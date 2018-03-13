
namespace SF.Component.Infrastructure.ComplexPropertyComponent
{
    /// <summary>
    /// 用与返回一个字符串的
    /// </summary>
    public interface IGetSingle<out T>
    {
        T GetSingle();
    }
}
