using SF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Form.Test01
{
    public class MessageBoardControl01 : FanUserControl
    {
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;

        private List<string> messageList = new List<string>();

        public MessageBoardControl01()
        {
            InitializeComponent();
            MaxLineCount = 100;
        }

        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.memoEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(300, 100);
            this.panelControl1.TabIndex = 1;
            // 
            // memoEdit1
            // 
            this.memoEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEdit1.Location = new System.Drawing.Point(2, 2);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(296, 96);
            this.memoEdit1.TabIndex = 0;
            this.memoEdit1.ReadOnly = true;
            // 
            // MessageBoardControl01
            // 
            this.Controls.Add(this.panelControl1);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "StateTipControl";
            this.Size = new System.Drawing.Size(300, 100);

            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        public int MaxLineCount { get; set; }

        private int showLineCount = 0;
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            showLineCount++;
            messageList.Add(message);
            if (MaxLineCount != 0 && showLineCount >= MaxLineCount)
            {
                string oldString = memoEdit1.Text.Substring(0, memoEdit1.Text.LastIndexOf("\r\n"));
                memoEdit1.Text = message + "\r\n" + oldString;
            }
            else
            {
                memoEdit1.Text = message + "\r\n" + memoEdit1.Text;
            }

        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveToFile(string filePath)
        {
            System.IO.File.WriteAllLines(filePath, messageList.ToArray());
        }

        public void Clear()
        {
            messageList.Clear();
            memoEdit1.Text = string.Empty;
            showLineCount = 0;
        }
    }
}
