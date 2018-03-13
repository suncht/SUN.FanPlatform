using System.ComponentModel;

namespace SF.Framework.Layout.Enums
{
    /// <summary>
    /// 布局工具栏的方式
    /// by changtan.sun
    /// </summary>
    public enum FanLayoutHeaderStyleEnum
    {
        /// <summary>
        /// Ribbon方式
        /// </summary>
        [Description("ribbon")]
        RIBBON = 1,
        /// <summary>
        /// 传统工具栏方式
        /// </summary>
        [Description("toolbar")]
        TOOLBAR = 2
    }

    public class FanLayoutHeaderStyleEnumHelper
    {
        public static FanLayoutHeaderStyleEnum Parse(string str)
        {
            if (str == "ribbon")
            {
                return FanLayoutHeaderStyleEnum.RIBBON;
            }
            else if (str == "toolbar")
            {
                return FanLayoutHeaderStyleEnum.TOOLBAR;
            }
            else
            {
                return FanLayoutHeaderStyleEnum.RIBBON;
            }
        }
    }
}
