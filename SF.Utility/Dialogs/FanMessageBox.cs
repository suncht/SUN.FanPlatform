using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SF.Utility.Dialogs
{
    #region 枚举弹出类型
    public enum MsgType
    {
        /// <summary>  
        /// 提示  
        /// </summary>  
        OK = 0,
        /// <summary>  
        /// 警告  
        /// </summary>  
        Warning = 1,
        /// <summary>  
        /// 询问  
        /// </summary>  
        Question = 2,
        /// <summary>  
        /// 错误  
        /// </summary>  
        Exception = 3,
        /// <summary>  
        /// 是/否/取消  
        /// </summary>  
        YesNoCancel = 4,
        /// <summary>  
        /// 是/否  
        /// </summary>  
        YesNo = 5
    }
    #endregion  

    /// <summary>
    /// 对话框
    /// by changtan.sun
    /// </summary>
    public sealed class FanMessageBox
    {
        /// <summary>  
        /// 显示消息  
        /// </summary>  
        /// <param name="msg">消息</param>  
        /// <param name="ie">消息类型</param>  
        /// <returns>需要用户作出选择时，返回YES or NO,否则返回，ok，cancel等</returns>  
        public static System.Windows.Forms.DialogResult ShowMsg(string msg, MsgType msgType)  
        {  
            switch (msgType)  
            {  
                case MsgType.OK:  
                    return ShowMsg(msg, "信息", MsgType.OK);  
                case MsgType.Question:  
                    return ShowMsg(msg, "确认", MsgType.Question);  
                case MsgType.Warning:  
                    return ShowMsg(msg, "警告", MsgType.Warning);  
                case MsgType.Exception:  
                    Exception(msg);  
                    return System.Windows.Forms.DialogResult.OK;  
                case MsgType.YesNoCancel:  
                    return ShowMsg(msg, "请选择", MsgType.YesNoCancel);  
                case MsgType.YesNo:  
                    return ShowMsg(msg, "请选择", MsgType.YesNo);  
                default:  
                    return System.Windows.Forms.DialogResult.Cancel;  
            }  
        }  
  
        /// <summary>  
        /// 显示消息  
        /// </summary>  
        /// <param name="msg">消息</param>  
        /// <param name="caption">标题 系统会自动加上一些信息</param>  
        /// <param name="msgType">消息类型</param>  
        /// <returns>需要用户作出选择时，返回YES or NO,否则返回，ok，cancel等</returns>  
        public static System.Windows.Forms.DialogResult ShowMsg(string msg, string caption, MsgType msgType)  
        {  
            switch (msgType)  
            {  
                case MsgType.OK:  
                    return ShowMsgBox(msg, caption, msgType);  
                case MsgType.Question:  
                    return ShowMsgBox(msg, caption, msgType);  
                case MsgType.Warning:  
                    return ShowMsgBox(msg, caption, msgType);  
                case MsgType.Exception:  
                    Exception(msg);  
                    return System.Windows.Forms.DialogResult.OK;  
                case MsgType.YesNoCancel:  
                    return ShowMsgBox(msg, caption, msgType);  
                case MsgType.YesNo:  
                    return ShowMsgBox(msg, caption, msgType);  
                default:  
                    return System.Windows.Forms.DialogResult.Cancel;  
            }  
        }  
  
        /// <summary>  
        /// 显示消息  
        /// </summary>  
        /// <param name="msg">消息</param>  
        /// <param name="catpion">标题</param>  
        /// <param name="msgType">消息类型</param>  
        /// <returns>需要用户作出选择时，返回YES or NO,否则返回，ok，cancel等</returns>  
        public static System.Windows.Forms.DialogResult ShowMsgBox(string msg, string catpion, MsgType msgType)  
        {  
            switch (msgType)  
            {  
                case MsgType.OK:  
                    return  Show(msg, catpion, System.Windows.Forms.MessageBoxButtons.OK);  
                case MsgType.Question:  
                    return  Question(msg, catpion, System.Windows.Forms.MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2);  
                case MsgType.Warning:  
                    return  Warning(msg, catpion, System.Windows.Forms.MessageBoxButtons.OK);  
                case MsgType.Exception:  
                     Exception(msg);  
                    return System.Windows.Forms.DialogResult.OK;  
                case MsgType.YesNoCancel:  
                    return  Information(msg, catpion, System.Windows.Forms.MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3);  
                case MsgType.YesNo:  
                    return  Information(msg, catpion, System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);  
                default:  
                    return System.Windows.Forms.DialogResult.Cancel;  
            }  
        }  
  
        /// <summary>  
        /// 显示消息  
        /// </summary>  
        /// <param name="msg">消息</param>  
        /// <param name="ie">消息类型</param>  
        /// <returns>需要用户作出选择时，返回YES or NO,否则返回，ok，cancel等</returns>  
        public static System.Windows.Forms.DialogResult ShowMsg(IWin32Window owner, string msg, MsgType msgType)  
        {  
            switch (msgType)  
            {  
                case MsgType.OK:  
                    return Show(msg, "信息", System.Windows.Forms.MessageBoxButtons.OK);  
                case MsgType.Question:  
                    return Question(msg, "确认", System.Windows.Forms.MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2);  
                case MsgType.Warning:  
                    return Warning(msg, "警告", System.Windows.Forms.MessageBoxButtons.OK);  
                case MsgType.Exception:  
                        Exception(msg);  
                    return System.Windows.Forms.DialogResult.OK;  
                case MsgType.YesNoCancel:  
                    return  Information(msg, "请选择", System.Windows.Forms.MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3);  
                default:  
                    return System.Windows.Forms.DialogResult.Cancel;  
            }  
        }  
 
        #region Show  
        public static DialogResult Show(string text)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text);  
        }  
  
        public static DialogResult Show(string text, string caption)  
        {  
  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption);  
        }  
  
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, buttons, MessageBoxIcon.Information);  
        }  
  
  
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(owner, text, caption, buttons, MessageBoxIcon.Information);  
        }  
  
        public static DialogResult Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, MessageBoxIcon icon)  
        {  
  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, buttons, icon);  
        }  
  
        public static DialogResult Show(IWin32Window owner, string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, MessageBoxIcon icon)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(owner, text, caption, buttons, icon);  
        }  
        #endregion  
 
        #region Information  
  
        public static DialogResult Information(string message)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, " 消息", MessageBoxButtons.OK, MessageBoxIcon.Information);  
        }  
  
        public static DialogResult Information(string message, string caption)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);  
        }  
  
        public static DialogResult Information(string message, string caption, MessageBoxButtons buttons)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, caption, buttons, MessageBoxIcon.Information);  
        }  
        public static DialogResult Information(string message, string caption, MessageBoxButtons buttons, MessageBoxDefaultButton defButton)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, caption, buttons, MessageBoxIcon.Information, defButton);  
        }  
        #endregion  
 
        #region Question  
  
        public static DialogResult Question(string text)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, "疑问", MessageBoxButtons.OK, MessageBoxIcon.Question);  
        }  
        public static DialogResult Question(string text, string caption)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Question);  
        }  
        public static DialogResult Question(string text, string caption, MessageBoxButtons buttons)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, buttons, MessageBoxIcon.Question);  
        }  
  
        public static DialogResult Question(string text, string caption, MessageBoxButtons buttons, MessageBoxDefaultButton defButton)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, buttons, MessageBoxIcon.Question, defButton);  
        }  
 
        #endregion  
 
        #region Warning  
        public static DialogResult Warning(string text)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);  
        }  
        public static DialogResult Warning(string text, string caption)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);  
        }  
        public static DialogResult Warning(string text, string caption, MessageBoxButtons buttons)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, buttons, MessageBoxIcon.Warning);  
        }  
        #endregion  
 
        #region Exception  
        public static DialogResult Exception(string text)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);  
        }  
        public static DialogResult Exception(string text, string caption)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);  
        }  
        public static DialogResult Exception(string text, string caption, MessageBoxButtons buttons)  
        {  
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, buttons, MessageBoxIcon.Error);  
        }  
        #endregion  
         
 
    }

     
}
