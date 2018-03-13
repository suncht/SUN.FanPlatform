using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace SF.Infrastructure
{
    /// <summary>
    /// 服务容器
    /// </summary>
    public class ServiceContainer
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ServiceContainer));
        private volatile static UnityContainer _instance = null;
        private static readonly object lockHelper = new object();
        private ServiceContainer() { }

        /// <summary>
        /// 获取服务容器实例
        /// </summary>
        /// <returns></returns>
        public static UnityContainer CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                    {
                        _instance = new UnityContainer();
                        Init();
                    }
                }
            }
            return _instance;
        }

        private static void Init()
        {
            
            UnityConfigurationSection configuration = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            configuration.Configure(_instance, "serviceContainer");
           
            
        }
    }
}
