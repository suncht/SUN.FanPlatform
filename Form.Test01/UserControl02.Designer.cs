namespace Form.Test01
{
    partial class UserControl02
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.addButton = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.testButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(32, 21);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "测试1";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(32, 63);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(347, 20);
            this.textEdit1.TabIndex = 1;
            // 
            // testButton2
            // 
            this.testButton2.Location = new System.Drawing.Point(130, 21);
            this.testButton2.Name = "testButton2";
            this.testButton2.Size = new System.Drawing.Size(75, 23);
            this.testButton2.TabIndex = 2;
            this.testButton2.Text = "测试2";
            this.testButton2.Click += new System.EventHandler(this.testButton2_Click);
            // 
            // UserControl02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.testButton2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.addButton);
            this.Name = "UserControl02";
            this.Size = new System.Drawing.Size(434, 198);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton addButton;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton testButton2;
    }
}
