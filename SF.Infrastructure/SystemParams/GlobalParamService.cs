
using SF.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
namespace SF.Infrastructure.SystemParams
{
    /// <summary>
    /// 系统全局参数服务（属于系统级别服务）
    /// </summary>
    public static class GlobalParamService
    {
        public const string config_ui_path = "Config/UI/";

        public static SystemSetting SystemSettingParam = new SystemSetting();

        #region 从app.config中读取配置信息
        public static void LoadFromConfig()
        {
            string applicationName = AppConfigHelper.GetAppConfig("application_name");
            string applicationIcon = AppConfigHelper.GetAppConfig("application_icon");
            string currentCulture = AppConfigHelper.GetAppConfig("current_culture");
            string companyName = AppConfigHelper.GetAppConfig("company_name");
            string productVersion = AppConfigHelper.GetAppConfig("product_version");
            string productName = AppConfigHelper.GetAppConfig("product_name");
            string copyRight = AppConfigHelper.GetAppConfig("copy_right");

            GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationName = applicationName;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationIcon = applicationIcon;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.CurrentCulture = currentCulture;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.CompanyName = companyName;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.ProductVersion = productVersion;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.ProductName = productName;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.CopyRight = copyRight;

            string layoutName = AppConfigHelper.GetAppConfig("layout_name");
            string enableSave = AppConfigHelper.GetAppConfig("layout_enable_save");
            string skinStyle = AppConfigHelper.GetAppConfig("layout_skin_style");
            GlobalParamService.SystemSettingParam.LayoutSettingParam.LayoutName = layoutName;
            GlobalParamService.SystemSettingParam.LayoutSettingParam.EnabeSave = ("true" == enableSave);
            GlobalParamService.SystemSettingParam.LayoutSettingParam.SkinStyle = skinStyle;
        }
        /// <summary>
        /// 写入AppConfig文件中
        /// </summary>
        public static void SaveToConfig()
        {
            AppConfigHelper.UpdateAppConfig("application_name", GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationName);
            AppConfigHelper.UpdateAppConfig("application_icon", GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationIcon);
            AppConfigHelper.UpdateAppConfig("current_culture", GlobalParamService.SystemSettingParam.ApplicationSettingParam.CurrentCulture);
            AppConfigHelper.UpdateAppConfig("company_name", GlobalParamService.SystemSettingParam.ApplicationSettingParam.CompanyName);
            AppConfigHelper.UpdateAppConfig("product_version", GlobalParamService.SystemSettingParam.ApplicationSettingParam.ProductVersion);
            AppConfigHelper.UpdateAppConfig("product_name", GlobalParamService.SystemSettingParam.ApplicationSettingParam.ProductName);

            AppConfigHelper.UpdateAppConfig("layout_name", GlobalParamService.SystemSettingParam.LayoutSettingParam.LayoutName);
            AppConfigHelper.UpdateAppConfig("layout_enable_save", GlobalParamService.SystemSettingParam.LayoutSettingParam.EnabeSave ? "true" : "false");
            AppConfigHelper.UpdateAppConfig("layout_skin_style", GlobalParamService.SystemSettingParam.LayoutSettingParam.SkinStyle);
        }

        #endregion

        #region  自定义图片集
        /// <summary>
        /// 加载自定义图片集
        /// </summary>
        public static void LoadCustomizeImageCollection(int size)
        {
            string images_filepath = Directory.GetCurrentDirectory() + "\\Images\\" + size + "x" + size + "\\";
            DirectoryInfo dir = new DirectoryInfo(images_filepath);
            if (!dir.Exists)
            {
                return;
            }

            Collection<Tuple<string, int, Image>> imageCollection = new Collection<Tuple<string, int, Image>>();
            var index = 0;
            foreach (var file in dir.GetFiles())
            {
                if (file.Extension == ".jpg" || file.Extension == ".gif" || file.Extension == ".png")
                {
                    Image image = Image.FromFile(images_filepath + file.Name);
                    imageCollection.Add(new Tuple<string, int, Image>(file.Name, index++, image));
                }
            }

            if (size == 16)
            {
                GlobalParamService.SystemSettingParam.Image16CollectionList = imageCollection;
            }
            else if (size == 32)
            {
                GlobalParamService.SystemSettingParam.Image32CollectionList = imageCollection;
            }
        }

        /// <summary>
        /// 获取自定义图片的数据（文件名称、索引号、Image对象）
        /// </summary>
        /// <returns></returns>
        public static Tuple<string, int, Image> GetCustomizeImageData(string imageName, int imageSize)
        {
            Collection<Tuple<string, int, Image>> imageCollectionList = null;
            if (imageSize == 16)
            {
                imageCollectionList = GlobalParamService.SystemSettingParam.Image16CollectionList;
            }
            else if (imageSize == 32)
            {
                imageCollectionList = GlobalParamService.SystemSettingParam.Image32CollectionList;
            }

            if (imageCollectionList == null || imageCollectionList.Count == 0)
            {
                return null;
            }

            Tuple<string, int, Image> tuple = imageCollectionList.FirstOrDefault((t) => { return t.Item1 == imageName; });
            return tuple;
        }
        #endregion

        #region 从setting.ini中读取配置信息
        /// <summary>
        /// 加载全局系统配置文件setting.ini
        /// </summary>
        public static void LoadFromIni()
        {
            string fileName = Directory.GetCurrentDirectory() + "\\Config\\setting.ini";
            IniFileHelper iniFileHelper = new IniFileHelper(fileName);

            //加载布局设置
            LoadLayoutSection(iniFileHelper);

            LoadSystemSection(iniFileHelper);
        }

        /// <summary>
        /// 系统程序设置
        /// </summary>
        /// <param name="iniFileHelper"></param>
        private static void LoadSystemSection(IniFileHelper iniFileHelper)
        {
            string applicationName = iniFileHelper.GetIniString("System", "ApplicationName", "快速开发框架");
            string applicationIcon = iniFileHelper.GetIniString("System", "ApplicationIcon", "");
            string currentCulture = iniFileHelper.GetIniString("System", "CurrentCulture", "cn-zh");
            string companyName = iniFileHelper.GetIniString("System", "CompanyName", "");
            string productVersion = iniFileHelper.GetIniString("System", "ProductVersion", "1.0");
            string productName = iniFileHelper.GetIniString("System", "ProductName", "快速开发框架");

            GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationName = applicationName;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationIcon = applicationIcon;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.CurrentCulture = currentCulture;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.CompanyName = companyName;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.ProductVersion = productVersion;
            GlobalParamService.SystemSettingParam.ApplicationSettingParam.ProductName = productName;
        }

        /// <summary>
        /// 布局设置
        /// </summary>
        /// <param name="iniFileHelper"></param>
        private static void LoadLayoutSection(IniFileHelper iniFileHelper)
        {
            string layoutName = iniFileHelper.GetIniString("Layout", "Name", "ribbon");
            string enableSave = iniFileHelper.GetIniString("Layout", "EnableSave", "false");
            GlobalParamService.SystemSettingParam.LayoutSettingParam.LayoutName = layoutName;
            GlobalParamService.SystemSettingParam.LayoutSettingParam.EnabeSave = ("true" == enableSave);

        }
        /// <summary>
        /// 写入数据到INI文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void WriteToIni(string section, string key, string value)
        {
            string fileName = Directory.GetCurrentDirectory() + "\\Config\\setting.ini";
            IniFileHelper iniFileHelper = new IniFileHelper(fileName);
            iniFileHelper.WriteIniString(section, key, value);
        }

        /// <summary>
        /// 将全局数据写入INI文件中
        /// </summary>
        public static void WriteToIni()
        {
            string fileName = Directory.GetCurrentDirectory() + "\\Config\\setting.ini";
            IniFileHelper iniFileHelper = new IniFileHelper(fileName);
            iniFileHelper.WriteIniString("Layout", "Name", GlobalParamService.SystemSettingParam.LayoutSettingParam.LayoutName);
            iniFileHelper.WriteIniBool("Layout", "EnableSave", GlobalParamService.SystemSettingParam.LayoutSettingParam.EnabeSave);
        }

        #endregion

        public static void Clear()
        {
            GlobalParamService.SystemSettingParam.ApplicationSettingParam = null;
            GlobalParamService.SystemSettingParam.LayoutSettingParam = null;
            GlobalParamService.SystemSettingParam.Image16CollectionList.Clear();
            GlobalParamService.SystemSettingParam.Image32CollectionList.Clear();
            GlobalParamService.SystemSettingParam.Image16CollectionList = null;
            GlobalParamService.SystemSettingParam.Image32CollectionList = null;
        }
    }

}
