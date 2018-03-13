
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
namespace SF.Infrastructure.SystemParams
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SystemSetting
    {
        /// <summary>
        /// 布局设置参数
        /// </summary>
        public LayoutSetting LayoutSettingParam = new LayoutSetting();
        /// <summary>
        /// 系统程序设置参数
        /// </summary>
        public ApplicationSetting ApplicationSettingParam = new ApplicationSetting();
        /// <summary>
        /// 图标集合16*16
        /// tuple第一个参数是：图标文件名
        /// tuple第二个参数是：序列号 从0开始
        /// </summary>
        public Collection<Tuple<string, int, Image>> Image16CollectionList = new Collection<Tuple<string, int, Image>>();
        /// <summary>
        /// 图标集合32*32
        /// tuple第一个参数是：图标文件名
        /// tuple第二个参数是：序列号 从0开始
        /// </summary>
        public Collection<Tuple<string, int, Image>> Image32CollectionList = new Collection<Tuple<string, int, Image>>();
    }

    /// <summary>
    /// 系统程序设置
    /// </summary>
    public class ApplicationSetting
    {
        public string ApplicationName = "仿真集成框架";
        public string ApplicationIcon = "";
        public string CurrentCulture = "cn-zh";
        public string CompanyName = "安世亚太";
        public string ProductVersion = "0.1";
        public string ProductName = "仿真集成框架";
        public string CopyRight = "版权2016-2017";
    }

    /// <summary>
    /// 布局设置
    /// </summary>
    public class LayoutSetting
    {
        /// <summary>
        /// 当前布局名称
        /// </summary>
        public string LayoutName = "ribbon";
        /// <summary>
        /// 是否启用自动保存布局（下次打开时，可恢复到上次布局）
        /// </summary>
        public bool EnabeSave = false;
        /// <summary>
        /// 皮肤样式
        /// </summary>
        public string SkinStyle = "DevExpress Style";
    }

}
