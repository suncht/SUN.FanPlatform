using System.Reflection;

namespace SF.Infrastructure.InvokeMethod
{
    /// <summary>
    /// 回调Assembly,对应InvokeMethodService.xml,系统内部使用
    /// </summary>
    public class InvokeMethodAssembly
    {
        /// <summary>
        /// 配置的程序级
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 配置的插件面板名称或Dll中类的全名
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 配置的方法名
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 是否是插件
        /// </summary>
        public bool IsPlugin { get; set; }

        /// <summary>
        /// 反射对象是否缓存
        /// </summary>
        public bool IsCached { get; set; }
        /// <summary>
        /// 从Xml加载 还是从Plugins目录下加载
        /// </summary>
        public bool FromXml { get; set; }
    }
}
