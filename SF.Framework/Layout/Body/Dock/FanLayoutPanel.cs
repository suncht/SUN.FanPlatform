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
    /// 布局面板对象
    /// by changtan.sun
    /// </summary>
    public class FanLayoutPanel
    {
        /// <summary>
        /// 面板UUID，用于Dock恢复上次布局，如果没有，恢复布局会有问题
        /// </summary>
        public string Guid;
        /// <summary>
        /// 面板标题
        /// </summary>
        public string Title;
        /// <summary>
        /// 面板名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 面板宽度
        /// </summary>
        public int Width = 200;
        /// <summary>
        /// 面板高度
        /// </summary>
        public int Height = 300;
        /// <summary>
        /// 是否属于Document的Tab面板
        /// </summary>
        public bool TabView;
        /// <summary>
        /// 子面板
        /// </summary>
        public Collection<FanLayoutPanel> ChildPanelItems = new Collection<FanLayoutPanel>();
        /// <summary>
        /// 停靠位置
        /// </summary>
        public FanLayoutDockPositionEnum DockPosistion = FanLayoutDockPositionEnum.LEFT;
        /// <summary>
        /// 面板类型
        /// </summary>
        public FanLayoutPanelTypeEnum Type;
        /// <summary>
        /// 面板需要的程序集
        /// </summary>
        public string Assembly;

        private string assemblyType = "dll";
        /// <summary>
        /// 程序集类型：dll、exe等
        /// </summary>
        public string AssemblyType
        {
            get { return assemblyType; }
            set { if (!string.IsNullOrWhiteSpace(value))assemblyType = value; }
        }
    }
}
