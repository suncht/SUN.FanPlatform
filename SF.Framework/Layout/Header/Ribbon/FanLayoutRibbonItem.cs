
namespace SF.Framework.Layout.Header.Ribbon
{
    /// <summary>
    /// Ribbon上的控件
    /// by changtan.sun
    /// </summary>
    public class FanLayoutRibbonItem
    {
        /// <summary>
        /// 控件类型，比如button等
        /// </summary>
        public string Type;
        /// <summary>
        /// 控件显示文本
        /// </summary>
        public string Title;
        /// <summary>
        /// 控件名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 控件图标Index(devexpress默认自带的图标)
        /// </summary>
        public string ImageIndex;
        /// <summary>
        /// 显示Tip文件
        /// </summary>
        public string Tip;
        /// <summary>
        /// 控件图标(自定义图片16*16)， 优先级比ImageIndex高
        /// </summary>
        public string Image16;

        /// <summary>
        /// 控件图标(自定义图片32*32)， 优先级比ImageIndex高
        /// </summary>
        public string Image32;

        public string Assembly;
        private string assemblyType = "dll";

        public string AssemblyType
        {
            get { return assemblyType; }
            set { if(!string.IsNullOrWhiteSpace(value))assemblyType = value; }
        }

        public bool Enabled = true;

        #region 定义事件
        /// <summary>
        /// 单击事件
        /// </summary>
        public string Event_OnClick;
        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        public string Event_OnMouseClick;
        /// <summary>
        /// Change事件
        /// </summary>
        public string Event_OnChange;
        #endregion

        public bool Checked = true;
    }
}
