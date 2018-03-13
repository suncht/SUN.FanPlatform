using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace SF.Infrastructure.DataExchange
{
    /// <summary>
    /// 全局变量管理的默认实现
    /// </summary>
    public class DataExchangeService : IDataExchange
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DataExchangeService));
        Dictionary<string, string> dic = new Dictionary<string, string>();

        private string filePath = "Config/base/DataExchangeConfig.xml";

        /// <summary>
        /// 获取或设置全局变量
        /// </summary>
        /// <param name="name">全局变量字符串索引</param>
        /// <returns></returns>
        public string this[string name]
        {
            //实现索引器的get方法
            get
            {
                if (dic.ContainsKey(name))
                {
                    return dic[name];
                }
                return null;
            }

            //实现索引器的set方法
            set
            {
                dic[name] = value;
            }
        }

        /// <summary>
        /// 新建
        /// </summary>
        public DataExchangeService()
        {
            LoadFromFile(filePath);
        }

        /// <summary>
        /// 从指定文件载入全局变量
        /// </summary>
        /// <param name="filepath">文件全路径</param>
        public void LoadFromFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                try
                {
                    dic.Clear();
                    XElement root = XElement.Load(filepath);
                    foreach (XElement globalVariant in root.Elements())
                    {
                        dic.Add(globalVariant.Attribute("key").Value, globalVariant.Attribute("value").Value);
                    }
                }
                catch
                {
                    logger.Error("全局变量管理初始化载入文件[" + filepath + "]失败,请确认格式");
                }
            }
            else
            {
                logger.Error("全局变量管理未找到初始化文件[" + filepath + "]");
            }
        }

        /// <summary>
        /// 从当前全局变量序列化到文件中
        /// </summary>
        /// <param name="filepath">文件路径</param>
        public void SaveToFile(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                filepath = this.filePath;
            }

            XElement xe = new XElement("DataExchange",
                from pair in dic
                select new XElement("Variant"
                    , new XAttribute("key", pair.Key),
                    new XAttribute("value", pair.Value))
                );
            xe.Save(filepath);
        }

        /// <summary>
        /// 删除指定的全局变量
        /// </summary>
        /// <param name="name">变量名</param>
        public void Remove(string name)
        {
            if (dic.ContainsKey(name))
            {
                dic.Remove(name);
            }
        }

        public bool ContainsKey(string key)
        {
            return dic.ContainsKey(key);
        }

    }
}
