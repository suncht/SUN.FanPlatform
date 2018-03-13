using DevExpress.XtraBars.Docking;

namespace SF.Framework.Layout.Enums
{
    /// <summary>
    /// Dock布局的面板停靠方式
    /// by changtan.sun
    /// </summary>
    public enum FanLayoutDockPositionEnum
    {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM,
        FILL,
        FLOAT
    }

    public class FanLayoutDockPositionEnumHelper
    {
        public static FanLayoutDockPositionEnum Parse(string str)
        {
            if (str == "left")
            {
                return FanLayoutDockPositionEnum.LEFT;
            }
            else if (str == "right")
            {
                return FanLayoutDockPositionEnum.RIGHT;
            }
            else if (str == "top")
            {
                return FanLayoutDockPositionEnum.TOP;
            }
            else if (str == "bottom")
            {
                return FanLayoutDockPositionEnum.BOTTOM;
            }
            else if (str == "fill")
            {
                return FanLayoutDockPositionEnum.FILL;
            }
            else if (str == "float")
            {
                return FanLayoutDockPositionEnum.FLOAT;
            } 
            else {
                return FanLayoutDockPositionEnum.LEFT;
            }
        }

        public static DockingStyle toDockingStyle(FanLayoutDockPositionEnum dockPosition)
        {
            if (dockPosition == FanLayoutDockPositionEnum.LEFT)
            {
                return DockingStyle.Left;
            }
            else if (dockPosition == FanLayoutDockPositionEnum.RIGHT)
            {
                return DockingStyle.Right;
            } 
            else if (dockPosition == FanLayoutDockPositionEnum.BOTTOM)
            {
                return DockingStyle.Bottom;
            }
            else if (dockPosition == FanLayoutDockPositionEnum.TOP)
            {
                return DockingStyle.Top;
            }
            else if (dockPosition == FanLayoutDockPositionEnum.FILL)
            {
                return DockingStyle.Fill;
            }
            else if (dockPosition == FanLayoutDockPositionEnum.FLOAT)
            {
                return DockingStyle.Float;
            }
            else
            {
                return DockingStyle.Left;
            }
        }
    }
}
