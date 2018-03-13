using SF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SF.Infrastructure.DataExchange;
using System.Windows.Forms;
namespace Demo01
{
    public class Demo01: FanUserControl
    {
        private Button button1;
    
        public void testDataExchange()
        {
            IDataExchange gvService = ServiceContainer.CreateInstance().Resolve<IDataExchange>("DataExchangeService");
            //获取值
            string projectPath = gvService["ProjectPath"];
            //设置值

            //保存到文件
            MessageBox.Show(projectPath);
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                gvService["ProjectPath"] = fbd.SelectedPath;
                gvService.SaveToFile(null);
            }
 
        }

        public Demo01()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Demo01
            // 
            this.Controls.Add(this.button1);
            this.Name = "Demo01";
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            testDataExchange();
        }
    }
}
