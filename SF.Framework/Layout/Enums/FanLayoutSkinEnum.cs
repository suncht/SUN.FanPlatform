
namespace SF.Framework.Layout.Enums
{
    /// <summary>
    /// 布局皮肤
    /// </summary>
    public enum FanLayoutSkinEnum
    {
        /// <summary>
        /// 银色
        /// </summary>
        SILVER = 1,
        /// <summary>
        /// 黑色
        /// </summary>
        BLACK = 2,
        /// <summary>
        /// 红色
        /// </summary>
        RED = 3
    }

    public class FanLayoutSkinEnumHelper
    {
        public static FanLayoutSkinEnum Parse(string str)
        {
            if (str == "silver")
            {
                return FanLayoutSkinEnum.SILVER;
            }
            else if (str == "black")
            {
                return FanLayoutSkinEnum.BLACK;
            }
            else if (str == "red")
            {
                return FanLayoutSkinEnum.RED;
            }
            else
            {
                return FanLayoutSkinEnum.SILVER;
            }
        }
    }
}
