
namespace SF.Framework.UI
{
    /// <summary>
    /// UI生成通用接口
    /// author by suncht
    /// </summary>
    public interface IUIGenerator
    {
        /// <summary>
        /// 生成布局界面前的处理
        /// </summary>
        void BeforeGenerate();

        /// <summary>
        /// 生成布局界面
        /// </summary>
        /// <param name="layout"></param>
        void GenerateLayout();
        /// <summary>
        /// 生成后的处理
        /// </summary>
        void AfterGenerate();
    }
}
