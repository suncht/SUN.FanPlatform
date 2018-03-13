
namespace SF.Component.Infrastructure.ComplexPropertyComponent
{
    /// <summary>
    /// 属性面板复杂类型支持接口，用于和json字符串互转，用户自定义复杂类型必须实现此接口
    /// </summary>
    public interface ISupportJsonSerialization
    {
        /// <summary>
        /// 字符串转化为对象
        /// </summary>
        /// <param name="jsonObject">对象的json字符串</param>
        /// <returns>对象</returns>
        object ParseJsonObject(string jsonObject);

        /// <summary>
        /// 将当前对象转化为json字符串
        /// </summary>
        /// <returns></returns>
        string ToJsonString();
    }
}
