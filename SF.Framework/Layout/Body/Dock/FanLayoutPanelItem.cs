using SF.Framework.Layout.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Framework.Layout.Body.Dock
{
    /// <summary>
    /// 每个Panel对象
    /// by changtan.sun
    /// </summary>
    public class FanLayoutPanelItem
    {
        public string Title;
        public string Name;

        /// <summary>
        /// Panel的停靠方式
        /// </summary>
        public FanLayoutDockPositionEnum DockPosistion = FanLayoutDockPositionEnum.LEFT;
        /// <summary>
        /// Panel的父面板
        /// </summary>
        public FanLayoutUiView UiViewControl;
    }
}
