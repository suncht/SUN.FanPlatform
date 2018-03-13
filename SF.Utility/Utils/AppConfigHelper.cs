using System.Collections.Generic;
using System.Configuration;

namespace SF.Utility.Utils
{
    public class AppConfigHelper
    {
        ///<summary> 
        ///依据连接串名字connectionName返回数据连接字符串  
        ///</summary> 
        ///<param name="connectionName"></param> 
        ///<returns></returns> 
        public static string GetConnectionStringsConfig(string connectionName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();
            return connectionString;
        }
        ///<summary> 
        ///更新连接字符串  
        ///</summary> 
        ///<param name="newName">连接字符串名称</param> 
        ///<param name="newConString">连接字符串内容</param> 
        ///<param name="newProviderName">数据提供程序名称</param> 
        public static void UpdateConnectionStringsConfig(string newName, string newConString, string newProviderName)
        {
            bool isModified = false; //记录该连接串是否已经存在  
            //如果要更改的连接串已经存在  
            if (ConfigurationManager.ConnectionStrings[newName] != null)
            {
                isModified = true;
            }
            //新建一个连接字符串实例  
            ConnectionStringSettings mySettings = new ConnectionStringSettings(newName, newConString, newProviderName);
            // 打开可执行的配置文件*.exe.config  
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // 如果连接串已存在，首先删除它  
            if (isModified)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(newName);
            }
            // 将新的连接串添加到配置文件中.  
            config.ConnectionStrings.ConnectionStrings.Add(mySettings);
            // 保存对配置文件所作的更改  
            config.Save(ConfigurationSaveMode.Modified);
            // 强制重新载入配置文件的ConnectionStrings配置节  
            ConfigurationManager.RefreshSection("ConnectionStrings");
        }

        public static List<string> GetAppConfigList(string ch)
        {
            List<string> listKeyStr = new List<string>();
            listKeyStr.Clear();
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key.Length > ch.Length)
                {
                    if (key.Substring(0, ch.Length).Equals(ch))
                        listKeyStr.Add(key + "：" + ConfigurationManager.AppSettings[key]);
                }
            }
            return listKeyStr;
        }

        ///<summary> 
        ///返回＊.exe.config文件中appSettings配置节的value项  
        ///</summary> 
        ///<param name="strKey"></param> 
        ///<returns></returns> 
        public static string GetAppConfig(string strKey)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == strKey)
                {
                    return ConfigurationManager.AppSettings[strKey];
                }
            }
            return null;
        }

        ///<summary>  
        ///在＊.exe.config文件中appSettings配置节增加一对键、值对  
        ///</summary>  
        ///<param name="newKey"></param>  
        ///<param name="newValue"></param>  
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }

            // 打开应用程序配置文件 
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //删除对象
            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            // 添加节点
            config.AppSettings.Settings.Add(newKey, newValue);
            // 保存app.config文件
            config.Save(ConfigurationSaveMode.Modified);
            //强制重新加载
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
