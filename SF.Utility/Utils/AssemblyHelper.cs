using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SF.Utility.Utils
{
    /// <summary>
    /// 动态加载程序集的帮助类
    /// </summary>
    public sealed class AssemblyHelper
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AssemblyHelper));

        /// <summary>
        /// 程序集缓存
        /// </summary>
        private static Dictionary<string, Assembly> _assembly_cache = new Dictionary<string, Assembly>();
        /// <summary>
        /// 对象实例缓存
        /// </summary>
        private static Dictionary<string, object> _object_instance_cache = new Dictionary<string, object>();

        public static Type GetTypeFromAssembly(string assemblyName, string typeName)
        {
            if (!assemblyName.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase)
                && !assemblyName.StartsWith(".exe", StringComparison.CurrentCultureIgnoreCase))
            {
                logger.Error("程序集" + assemblyName + "不是dll或者exe文件");
                return null;
            }

            string assemblyFullPath = Directory.GetCurrentDirectory() + "/Plugins/" + assemblyName;

            if (!File.Exists(assemblyFullPath))
            {
                logger.Error("程序集" + assemblyName + "不存在");
                return null;
            }

            try
            {
                    Assembly asm = null;
                    if (_assembly_cache.ContainsKey(assemblyName))
                    {
                        asm = _assembly_cache[assemblyName];
                    }
                    else
                    {

                        asm = Assembly.Load(AssemblyName.GetAssemblyName(assemblyFullPath));
                        _assembly_cache.Add(assemblyName, asm);
                    }

                    return asm.GetType(typeName);
            }
            catch (Exception ex)
            {
                logger.Error("程序集" + assemblyName + "加载失败，其原因如下：" + ex.Message, ex);
            }

            return null;
        }

        /// <summary>
        /// 从程序集生成对象实例
        /// </summary>
        /// <param name="assemblyName">程序集</param>
        /// <param name="typeName">程序集的类型，dll还是exe</param>
        /// <param name="isShareInstance">是否要共享实例对象</param>
        /// <returns></returns>
        public static object GetInstanceFromAssembly(string assemblyName, string typeName, bool isShareInstance=true)
        {
            if (!assemblyName.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase)
                && !assemblyName.StartsWith(".exe", StringComparison.CurrentCultureIgnoreCase))
            {
                logger.Error("程序集" + assemblyName + "不是dll或者exe文件");
                return null;
            }

            string assemblyFullPath = Directory.GetCurrentDirectory() + "/Plugins/" + assemblyName;

            //增加DLL加载路径
            AppDomain.CurrentDomain.AppendPrivatePath(Directory.GetCurrentDirectory() + "/Plugins/");

            if (!File.Exists(assemblyFullPath))
            {

                logger.Error("程序集" + assemblyName + "不存在" + ":" + assemblyFullPath);
                return null;
            }

            try
            {
                if (isShareInstance && _object_instance_cache.ContainsKey(typeName))
                {
                    return _object_instance_cache[typeName];
                }
                else
                {
                    Assembly asm = null;
                    if (_assembly_cache.ContainsKey(assemblyName))
                    {
                        asm = _assembly_cache[assemblyName];
                    }
                    else
                    {
                        asm = Assembly.Load(AssemblyName.GetAssemblyName(assemblyFullPath));
                        _assembly_cache.Add(assemblyName, asm);
                    }

                    var instance = asm.CreateInstance(typeName);
                    if (isShareInstance) _object_instance_cache.Add(typeName, instance);
                    return instance;
                }
            }
            catch (Exception ex)
            {
                logger.Error("程序集" + assemblyName + "加载失败，其原因如下：" + ex.Message, ex);
            }

            return null;
        }

        public static MethodInfo GetMethodInfo(object instance, string methodName)
        {
            if (instance == null)
            {
                logger.Error("类[" + instance.GetType() + "]的实例不存在");
                return null;
            }

            var method = instance.GetType().GetMethod(methodName);
            //if (method != null)
            //{
            //    ParameterInfo[] paramsInfo = method.GetParameters();
                
            //}
            if (method == null)
            {
                logger.Error("类[" + instance.GetType() + "]的方法[" + methodName + "不存在");
            }

            return method;
        }

        /// <summary>
        /// 给控件注册事件
        /// </summary>
        /// <param name="ctrl">要注册事件的控件</param>
        /// <param name="eventName">要注册事件的控件的事件名称</param>
        /// <param name="instance"></param>
        /// <param name="method">事件方法</param>
        public static void RegisterControlEvent(Component ctrl, string eventName, object instance, MethodInfo method)
        {
            var click = ctrl.GetType().GetEvents().FirstOrDefault(ei => ei.Name.Equals(eventName, StringComparison.CurrentCultureIgnoreCase));
            if (click != null)
            {
                var handler = Delegate.CreateDelegate(click.EventHandlerType, instance, method);
                click.AddEventHandler(ctrl, handler);
            }
        }


    }
}
