
namespace SF.Framework.Layout.Enums
{
    /// <summary>
    /// 主体布局方式枚举
    /// by changtan.sun
    /// </summary>
    public enum FanLayoutBodyStyleEnum
    {
        /// <summary>
        /// 默认主体布局
        /// </summary>
        DEFAULT = 1,
        /// <summary>
        /// Dock主体布局
        /// </summary>
        DOCK = 2,
        /// <summary>
        /// 固定布局
        /// </summary>
        FIXED = 3
    }

    public class FanLayoutBodyStyleEnumHelper
    {
        public static FanLayoutBodyStyleEnum Parse(string str)
        {
            if (str == "dock")
            {
                return FanLayoutBodyStyleEnum.DOCK;
            }
            else if (str == "fixed")
            {
                return FanLayoutBodyStyleEnum.FIXED;
            }
            else
            {
                return FanLayoutBodyStyleEnum.DEFAULT;
            }
        }
    }
}
