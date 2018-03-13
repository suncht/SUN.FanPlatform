using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraSplashScreen;
using SF.Framework.Core;
using SF.Framework.Layout;
using SF.Framework.UI;
using SF.Framework.UI.DX;
using SF.Infrastructure;
using SF.Infrastructure.ControlAddress;
using SF.Infrastructure.SystemParams;
using SF.Utility.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Practices.Unity;

namespace SF.Framework
{
    public partial class FanRibbonFormContainer : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(FanRibbonFormContainer));

        public FanFormHookHolder HookHolder = new FanFormHookHolder();


        public FanRibbonFormContainer()
        {
            InitializeComponent();
            //显示Splash等待闪屏
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(SplashWindow), true, true, false);

            this.Text = GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationName + " - " +GlobalParamService.SystemSettingParam.ApplicationSettingParam.ProductVersion;
            SetSplashInfo("初始化系统配置", 0, 10, 50);
            //this.Icon = new System.Drawing.Icon();

            LoadCustomizeImageCollection();
            SetSplashInfo("加载自定义图标", 11, 60, 10);

            LoadMainLayout();
            SetSplashInfo("加载界面布局", 61, 100, 10);
        }

        /// <summary>
        /// 设置Splash的文本信息
        /// </summary>
        /// <param name="labeltext"></param>
        /// <param name="formpos"></param>
        /// <param name="topos"></param>
        /// <param name="sleeptime"></param>
        private void SetSplashInfo(string labeltext, int formpos, int topos, int sleeptime)
        {
            for (int i = formpos; i < topos; i++)
            {
                SplashScreenManager.Default.SendCommand(SplashWindow.SplashScreenCommand.LOAD_SETTING,
                    new Info() { LabelText = labeltext + "   " + (i+1)+ "%", Pos = i });
                Thread.Sleep(sleeptime);
            }
        }

        /// <summary>
        /// 加载自定义图片集， 图片集来自于Images文件夹中
        /// </summary>
        private void LoadCustomizeImageCollection()
        {
            //加载自定义图片集
            GlobalParamService.LoadCustomizeImageCollection(16);
            GlobalParamService.LoadCustomizeImageCollection(32);

            //读取16*16的图片集，并且加载到Winform中
            if (GlobalParamService.SystemSettingParam.Image16CollectionList.Count > 0)
            {
                //this.image16Collection.BeginInit();
                this.image16Collection.Clear();
                foreach (var item in GlobalParamService.SystemSettingParam.Image16CollectionList)
                {
                    this.image16Collection.AddImage(item.Item3, item.Item1);
                }
                //this.image16Collection.EndInit();
            }

            //读取32*32的图片集，并且加载到Winform中
            if (GlobalParamService.SystemSettingParam.Image32CollectionList.Count > 0)
            {

                //this.image32Collection.BeginInit();
                this.image32Collection.Clear();
                foreach (var item in GlobalParamService.SystemSettingParam.Image32CollectionList)
                {
                    this.image32Collection.AddImage(item.Item3, item.Item1);
                }
               // this.image32Collection.EndInit();
            }
        }

        public DockManager GetDockManager()
        {
            return this.dockManager1;
        }

        public System.ComponentModel.IContainer GetComponents()
        {
            return this.components;
        }

        public DocumentManager GetDocumentManager()
        {
            return this.documentManager1;
        }

        public TabbedView GetTabbedView()
        {
            return this.tabbedView1;
        }

        
        private void FormContainer_Load(object sender, EventArgs e)
        {
            SplashScreenManager.CloseForm(false);
        }

        //加载布局
        private void LoadMainLayout()
        {
            //生成布局
            UIGeneratorBase layoutGenerator = UIFactory.GetInstance().GetGenerator(GlobalParamService.SystemSettingParam.LayoutSettingParam.LayoutName);
            layoutGenerator.TargetContainer = this;
            layoutGenerator.HeaderHostControl = this.ribbonControl1;
            layoutGenerator.GenerateLayout();

            //恢复上次保存的界面布局
            RestoreLayoutFromXml();

            LoadMainIcon();
        }

        private void LoadMainIcon()
        {
            if(!string.IsNullOrEmpty(GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationIcon))
            {
                if(File.Exists(GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationIcon))
                {
                    Icon icon=new Icon(GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationIcon);
                    this.Icon = icon;
                }
            }
        }

        private void FanRibbonFormContainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            //string xml = Directory.GetCurrentDirectory() + "\\Config\\layout.xml";
            //this.dockManager1.SaveLayoutToXml(xml);

            HookHolder.Dispose();

            IControlAddress controlAddressService = ServiceContainer.CreateInstance().Resolve<IControlAddress>("ControlAddressService");
            if (controlAddressService != null)
            {
                controlAddressService.Unregister();
            }

            this.image16Collection.Images.Clear();
            this.image32Collection.Images.Clear();

            GlobalParamService.Clear();
        }

        private void FanRibbonFormContainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = string.Format("是否确定退出{0}?", GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationName);
            DialogResult dr = SF.Utility.Dialogs.FanMessageBox.ShowMsgBox(message, "系统提示", Utility.Dialogs.MsgType.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                //保存当前布局
                this.SaveLayoutToXml();

                this.Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        #region 布局保存和恢复
        /// <summary>
        /// 保存当前布局到文件中
        /// </summary>
        private void SaveLayoutToXml()
        {
            //保存到AppConfig
            if (GlobalParamService.SystemSettingParam.LayoutSettingParam.EnabeSave)
            {
                //保存皮肤Skin
                AppConfigHelper.UpdateAppConfig("layout_skin_style", GlobalParamService.SystemSettingParam.LayoutSettingParam.SkinStyle);
                //保存布局
                string xml = Directory.GetCurrentDirectory() + "\\Config\\layout\\" + GlobalParamService.SystemSettingParam.LayoutSettingParam.LayoutName + "-restore.xml";
                this.dockManager1.SaveLayoutToXml(xml);
            }
        }
        /// <summary>
        ///  恢复上次保存的界面布局
        /// </summary>
        private void RestoreLayoutFromXml()
        {
            if (GlobalParamService.SystemSettingParam.LayoutSettingParam.EnabeSave)
            {
                //恢复上次保存的界面布局
                string xml = Directory.GetCurrentDirectory() + "\\Config\\layout\\" + GlobalParamService.SystemSettingParam.LayoutSettingParam.LayoutName + "-restore.xml";
                if (File.Exists(xml))
                {
                    this.dockManager1.RestoreLayoutFromXml(xml);
                }
            }
        }

        #endregion
    }
}
