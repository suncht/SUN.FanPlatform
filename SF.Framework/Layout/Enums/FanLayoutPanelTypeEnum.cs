
namespace SF.Framework.Layout.Enums
{
    /// <summary>
    /// 面板类型
    /// by changtan.sun
    /// </summary>
    public enum FanLayoutPanelTypeEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        DEFAULT,
        /// <summary>
        /// 自定义
        /// </summary>
        CUSTOM
    }

    public class FanLayoutPanelTypeEnumHelper
    {
        public static FanLayoutPanelTypeEnum Parse(string str)
        {
            if (str == "default")
            {
                return FanLayoutPanelTypeEnum.DEFAULT;
            }
            else if (str == "custom")
            {
                return FanLayoutPanelTypeEnum.CUSTOM;
            }

            return FanLayoutPanelTypeEnum.DEFAULT;
        }
    }
}
