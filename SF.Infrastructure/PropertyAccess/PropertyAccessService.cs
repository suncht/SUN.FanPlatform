using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SF.Infrastructure.PropertyAccess
{
    /// <summary>
    ///  数据访问服务实现类
    /// </summary>
    public class PropertyAccessService:IPropertyAccess
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PropertyAccessService));
        private Dictionary<string, NamePropertyPair> valueDic = new Dictionary<string, NamePropertyPair>();

        /// <summary>
        /// 获取插件指定名称的属性值
        /// </summary>
        /// <param name="key">注册的属性索引值</param>
        /// <returns>属性值</returns>
        public object GetValue(string key)
        {
            if (valueDic.ContainsKey(key))
            {
                return valueDic[key].PInfo.GetValue(valueDic[key].AccessObject,null);
            }

            throw new ApplicationException("找不到属性【" + key + "】，无法获取该属性值");
        }

        public object GetValue(string typeName, string key)
        {
            string real_key = typeName + "_" + key;
            return GetValue(real_key);
        }

        public object GetValue(Type type, string key)
        {
            string real_key = type.FullName + "_" + key;
            return GetValue(real_key);
        }

        /// <summary>
        /// 设置插件指定名称的属性值
        /// </summary>
        /// <param name="key">注册的属性索引值</param>
        /// <param name="value">要设置的值</param>
        public void SetValue(string key, object value)
        {
            if (valueDic.ContainsKey(key))
            {
                valueDic[key].PInfo.SetValue(valueDic[key].AccessObject, value, null);
            }
            else
            {
                throw new ApplicationException("找不到属性【" + key + "】，无法为该属性赋值");
            }
        }

        public void SetValue(Type type, string key, object value)
        {
            string real_key = type.FullName + "_" + key;
            SetValue(real_key, value);
        }

        public void SetValue(string typeName, string key, object value)
        {
            string real_key = typeName + "_" + key;
            SetValue(real_key, value);
        }

        /// <summary>
        /// 向服务注册对象,注册后即可访问指定对象的指定属性
        /// </summary>
        /// <param name="sda">实现指定接口的对象</param>
        public void register(ISupportPropertyAccess sda)
        {
            if (sda != null)
            {
                if (sda.AllowAccessPropertiesDic != null)
                {
                    foreach(KeyValuePair<string,string> pair in sda.AllowAccessPropertiesDic)
                    {
                        string key = sda.AccessName + "_" + pair.Key;
                        if (valueDic.ContainsKey(key))
                        {
                            logger.Error("已经加载名为["+sda.AccessName+"]对象的["+pair.Value+"]属性");
                            continue;
                        }
                        PropertyInfo property=sda.GetType().GetProperty(pair.Value);
                        if (property == null)
                        {
                            logger.Error("名为[" + sda.AccessName + "]对象的[" + pair.Value + "]属性不存在");
                            continue;
                        }
                        valueDic.Add(key, new NamePropertyPair()
                        {
                            AccessObject = sda,
                            PInfo = property
                        }
                        );
                    }
                }
            }
        }

        public void registerObject(Object obj)
        {
            if (obj == null) return;

            Type type = obj.GetType();
            foreach (PropertyInfo property in type.GetProperties())
            {
                foreach (Attribute attr in property.GetCustomAttributes(typeof(PropertyAccessAttribute), false))
                {
                    if (attr is PropertyAccessAttribute)
                    {
                        string key = type.Namespace + "." + type.Name + "_" + property.Name;
                        valueDic.Add(key, new NamePropertyPair()
                        {
                            AccessObject = obj,
                            PInfo = property
                        }
                        );
                    }
                }
            }
        }

        /// <summary>
        /// 向服务反注册对象,反注册后指定对象的指定属性不可访问
        /// </summary>
        /// <param name="sda">实现指定接口的对象</param>
        public void unregister(ISupportPropertyAccess spa)
        {
            if (spa != null)
            {
                if (spa.AllowAccessPropertiesDic != null)
                {
                    foreach (KeyValuePair<string, string> pair in spa.AllowAccessPropertiesDic)
                    {
                        string key = spa.AccessName + "_" + pair.Key;
                        if (valueDic.ContainsKey(key))
                        {
                            valueDic.Remove(key);
                            continue;
                        }
                    }
                }
            }
        }
    }

    class NamePropertyPair 
    {
        public object AccessObject { get; set; }

        public PropertyInfo PInfo { get; set; }
    }
}
