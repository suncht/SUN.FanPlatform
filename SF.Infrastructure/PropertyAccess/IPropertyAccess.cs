using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Infrastructure.PropertyAccess
{
    /// <summary>
    /// 数据访问服务接口
    /// </summary>
    public interface IPropertyAccess
    {
        /// <summary>
        /// 获取插件指定名称的属性值
        /// </summary>
        /// <param name="key">注册的属性索引值</param>
        /// <returns>属性值</returns>
        object GetValue(string key);

        object GetValue(string typeaName, string key);

        object GetValue(Type type, string key);

        /// <summary>
        /// 设置插件指定名称的属性值
        /// </summary>
        /// <param name="key">注册的属性索引值</param>
        /// <param name="value">要设置的值</param>
        void SetValue(string key, object value);

        void SetValue(string typeName, string key, object value);

        void SetValue(Type type, string key, object value);

        /// <summary>
        /// 向服务注册对象,注册后即可访问指定对象的指定属性
        /// </summary>
        /// <param name="sda">实现指定接口的对象</param>
        void register(ISupportPropertyAccess sda);

        void registerObject(Object obj);

        /// <summary>
        /// 向服务反注册对象,反注册后指定对象的指定属性不可访问
        /// </summary>
        /// <param name="sda">实现指定接口的对象</param>
        void unregister(ISupportPropertyAccess sda);
    }
}
