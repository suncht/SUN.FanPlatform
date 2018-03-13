using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.Text;
using System.IO;
using SF.Infrastructure.SystemParams;

namespace SF.Framework
{
    public class Program
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main method point for the application.
        /// </summary>
        public void Execute()
        {
            try
            {
                InitApplicationExecptionBehavior();

                InitDevApplicationSkins();

                FanRibbonFormContainer form = new FanRibbonFormContainer();

                BeforeApplicationRun(form);

                Application.Run(form);
                
                BeforeApplicationEnd();
            }
            catch (Exception ex)
            {
                string str = GetExceptionMsg(ex, string.Empty);
                MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(str);
                try
                {
                    ApplicationException(ex);
                }
                catch(Exception exx) 
                {
                    string strr = GetExceptionMsg(ex, "系统崩溃调用处理方法时");
                    logger.Error(str);
                }
            }
            
        }



        /// <summary>
        /// 在窗体创建后执行
        /// </summary>
        public virtual void BeforeApplicationRun(FanRibbonFormContainer mainForm)
        {
            logger.Info("BeforeApplicationRun");
        }

        /// <summary>
        /// 窗体关闭后运行
        /// </summary>
        public virtual void BeforeApplicationEnd()
        {
            logger.Info("BeforeApplicationEnd");
        }

        public virtual void ApplicationException(Exception ex)
        {
            logger.Info("ApplicationException");
        }

        public void InitDevApplicationSkins()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            //DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(FanRibbonFormContainer).Assembly); //Register!
            //Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.Skins.SkinManager.EnableFormSkins();

            //加载系统全局参数
            GlobalParamService.LoadFromConfig();

            UserLookAndFeel.Default.SetSkinStyle(GlobalParamService.SystemSettingParam.LayoutSettingParam.SkinStyle);
        }

        /// <summary>
        /// 设置应用程序异常行为
        /// </summary>
        public virtual void InitApplicationExecptionBehavior()
        {
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

           
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            logger.Error(str);

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            logger.Error(str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}
