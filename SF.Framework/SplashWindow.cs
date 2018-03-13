using System;
using DevExpress.XtraSplashScreen;
using SF.Infrastructure.SystemParams;
using System.Threading;

namespace SF.Framework
{
    public partial class SplashWindow : SplashScreen
    {
        public SplashWindow()
        {
            InitializeComponent();

            this.labelControl1.Text = GlobalParamService.SystemSettingParam.ApplicationSettingParam.ApplicationName + " " +GlobalParamService.SystemSettingParam.ApplicationSettingParam.CopyRight;
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            //base.ProcessCommand(cmd, arg);

            SplashScreenCommand command = (SplashScreenCommand)cmd;
            if (command == SplashScreenCommand.LOAD_SETTING)
            {
                Info pos = (Info)arg;
                progressBarControl1.Position = pos.Pos;
                labelControl2.Text = pos.LabelText;

                //Thread.Sleep(100);
            }
        }

        #endregion

        public enum SplashScreenCommand
        {
            LOAD_SETTING
        }
    }

    public class Info
    {
        //滚动条的位置信息
        public int Pos
        {
            get;
            set;
        }
        //滚动条上对应的文字信息
        public string LabelText
        {
            get;
            set;
        }
    }
}


    
