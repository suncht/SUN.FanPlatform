
namespace SF.Infrastructure
{
    /// <summary>
    /// 方法调用服务接口
    /// </summary>
    public interface IInvokeMethod
    {
        /// <summary>
        /// 调用方法，方法配置在config\base\InvokeMethodService.xml中
        /// </summary>
        /// <param name="name">类型名称和方法名通过_拼接在一起的字符串</param>
        /// <param name="param">调用方法的输入参数</param>
        /// <returns>调用方法的返回值</returns>
        object Invoke(string name, object[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName">配置的type元素的Name属性</param>
        /// <param name="methodName">调用的方法名</param>
        /// <param name="param">调用方法的输入参数</param>
        /// <returns>调用方法的返回值</returns>
        object Invoke(string typeName, string methodName, object[] param);
    }
}
