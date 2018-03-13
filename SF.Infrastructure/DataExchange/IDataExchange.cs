using System;
namespace SF.Infrastructure.DataExchange
{
    /// <summary>
    /// 全局变量管理接口
    /// </summary>
    public interface IDataExchange
    {
        /// <summary>
        /// 从指定文件载入全局变量
        /// </summary>
        /// <param name="filepath">文件全路径</param>
        void LoadFromFile(string filepath);
        /// <summary>
        /// 从当前全局变量序列化到文件中
        /// </summary>
        /// <param name="filepath">文件路径</param>
        void SaveToFile(string filepath);
        /// <summary>
        /// 获取或设置全局变量
        /// </summary>
        /// <param name="name">全局变量字符串索引</param>
        /// <returns></returns>
        string this[string name] { get; set; }

        /// <summary>
        /// 删除指定的全局变量
        /// </summary>
        /// <param name="name">变量名</param>
        void Remove(string name);

        /// <summary>
        /// 判断缓存的全局数据是否包含指定的键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainsKey(string key);
    }
}
