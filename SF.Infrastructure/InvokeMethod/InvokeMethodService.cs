using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using SF.Utility.Utils;
using Microsoft.Practices.Unity;
using SF.Infrastructure.ControlAddress;
using System.ComponentModel;
using System.Linq;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace SF.Infrastructure.InvokeMethod
{
    /// <summary>
    /// 方法调用服务实现类
    /// </summary>
    public  class InvokeMethodService : IInvokeMethod
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(InvokeMethodAssembly));

        /// <summary>
        /// 服务缓存
        /// </summary>
        private static Dictionary<string, InvokeMethodAssembly> callBackAssemblyCache = new Dictionary<string, InvokeMethodAssembly>();
        
        private ControlAddress.IControlAddress cAddressService;

        public InvokeMethodService()
        {
            //从XML文件中加载
            LoadFromXml();
            //从Dll反射加载
            LoadFromPlugins();
        }

        /// <summary>
        /// 从InvokeMethodService.xml中加载
        /// </summary>
        private void LoadFromXml()
        {
            string configPath = "config/base/InvokeMethodService.xml";
            if (!File.Exists(configPath))
            {
                logger.Error("找不到InvokeMethodk配置文件[" + configPath + "]");
            }
            XElement root = XElement.Load(configPath);
            foreach (XElement assemblyConfig in root.Elements())
            {
                try
                {
                    //string configName = itemConfig.Element("Name").Value;
                    string assemblyName = assemblyConfig.Attribute("Name").Value;
                    foreach (XElement typeConfig in assemblyConfig.Elements())
                    {
                        string typeName = typeConfig.Attribute("Name").Value;
                        bool isPlugin = false;
                        if (typeConfig.Attribute("IsPlugin") != null)
                            isPlugin = bool.Parse(typeConfig.Attribute("IsPlugin").Value);
                        bool isCached = true;
                        if (typeConfig.Attribute("IsCached") != null)
                            isCached = bool.Parse(typeConfig.Attribute("IsCached").Value);
                        foreach (XElement methodConfig in typeConfig.Elements())
                        {
                            string methodName = methodConfig.Value;
                            InvokeMethodAssembly stru = new InvokeMethodAssembly()
                            {
                                AssemblyName = assemblyName,
                                IsCached = isCached,
                                IsPlugin = isPlugin,
                                Method = methodName,
                                TypeName = typeName,
                                FromXml = true
                            };
                            string key = buildConfigName(stru);
                            if (!callBackAssemblyCache.ContainsKey(key))
                            {
                                callBackAssemblyCache.Add(key, stru);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("InvokeMethod配置项目[" + assemblyConfig.ToString() + "]配置失败,原因[" + ex.Message + "]");
                }

            }
        }

        private void LoadFromPlugins()
        {
            string pluginPath = Directory.GetCurrentDirectory() + "/Plugins/";
            var pluginFiles = Directory.GetFiles(pluginPath).Where((file) => file.EndsWith(".dll", true, null) || file.EndsWith(".exe", true, null));

            foreach (var pluginFile in pluginFiles)
            {
                Assembly assembly = Assembly.LoadFrom(pluginFile);
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    bool isPlugin = false;
                    string typeName = type.FullName;
                    if (typeof(UserControl).IsAssignableFrom(type))
                    {
                        isPlugin = true;
                        typeName = type.FullName;
                    }

                    foreach (MethodInfo method in type.GetMethods())
                    {
                        foreach (InvokeMethodAttribute attr in method.GetCustomAttributes(typeof(InvokeMethodAttribute), false))
                        {
                            string methodName = method.Name;
                            InvokeMethodAssembly stru = new InvokeMethodAssembly()
                            {
                                AssemblyName = assembly.FullName,
                                IsCached = attr.IsCached,
                                IsPlugin = isPlugin,
                                Method = methodName,
                                TypeName = typeName,
                                FromXml = false
                            };

                            string key = buildConfigName(stru);
                            if (!callBackAssemblyCache.ContainsKey(key))
                            {
                                callBackAssemblyCache.Add(key, stru);
                            }
                        }
                    }
                }
            }
        }

        private string buildConfigName(InvokeMethodAssembly stru)
        {
            string returnalue = string.Format("{0}_{1}", stru.TypeName, stru.Method);

            return returnalue;
        }

        /// <summary>
        /// 调用方法，方法配置在config\base\InvokeMethodService.xml中
        /// </summary>
        /// <param name="name">类型名称和方法名通过_拼接在一起的字符串</param>
        /// <param name="param">调用方法的输入参数</param>
        /// <returns>调用方法的返回值</returns>
        public object Invoke(string name,params object[] o) {
            InvokeMethodAssembly s = null;
            if (callBackAssemblyCache.ContainsKey(name))
            {
                //从xml加载
                s = callBackAssemblyCache[name];

                if (s.IsPlugin)
                {
                    return callControlMethod(s, o);
                }
                else
                {
                    return callNormalMethod(s, o);
                }
            }
            
            if (s == null)
            {
                throw new ApplicationException("找不到调用方法，方法调用失败");
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName">配置的type元素的Name属性</param>
        /// <param name="methodName">调用的方法名</param>
        /// <param name="param">调用方法的输入参数</param>
        /// <returns>调用方法的返回值</returns>
        public object Invoke(string typeName, string methodName, object[] param)
        {
            InvokeMethodAssembly s = null;
            string key = string.Format("{0}_{1}", typeName, methodName);
            if (callBackAssemblyCache.ContainsKey(key))
            {
                s = callBackAssemblyCache[key];
                if (s.FromXml)
                {
                    IControlAddress controlAddress = ServiceContainer.CreateInstance().Resolve<IControlAddress>("ControlAddressService");
                    string name = controlAddress.GetNameByTypeName(typeName, typeName);

                    s = callBackAssemblyCache[key];

                    if (s.IsPlugin)
                    {
                        return callControlMethod(s, param);
                    }
                    else
                    {
                        return callNormalMethod(s, param);
                    }
                }
                else
                {
                    if (s.IsPlugin)
                    {
                        return callControlMethod(s, param);
                    }
                    else
                    {
                        return callNormalMethod(s, param);
                    }
                }
            }

            if (s == null)
            {
                throw new ApplicationException("找不到调用方法，方法调用失败");
            }

            return null;
        }

        private object callNormalMethod(InvokeMethodAssembly s,params object[] o)
        {
            
            object instance = AssemblyHelper.GetInstanceFromAssembly(s.AssemblyName, s.TypeName, s.IsCached);
            MethodInfo minfo = instance.GetType().GetMethod(s.Method);
            return minfo.Invoke(instance, o);
            
        }

        private object callControlMethod(InvokeMethodAssembly s, params object[] o)
        {

            if (cAddressService == null)
            {
                cAddressService = ServiceContainer.CreateInstance().Resolve<IControlAddress>("ControlAddressService");
                
                
                //pmanager = ServiceContainer.CreateInstance().Resolve<Plugin.IPluginManager>("PluginManagerService");
            }

            Component c = null;
            string panelName = cAddressService.GetNameByTypeName(s.TypeName, null);
            if (panelName == null)
            {
                c = cAddressService.GetUserControlInPanel(s.TypeName);
            }
            else
            {
                c = cAddressService.GetUserControlInPanel(panelName);
            }
            
            MethodInfo minfo = c.GetType().GetMethod(s.Method);
            return minfo.Invoke(c, o);
        }

        /// <summary>
        /// 获取方法调用内存表示
        /// </summary>
        public Dictionary<string, InvokeMethodAssembly> GetCallBackAssemblyCache
        {
            get { return callBackAssemblyCache; }
        }
    }
}
