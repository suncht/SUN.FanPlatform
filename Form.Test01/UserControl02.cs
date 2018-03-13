using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SF.Infrastructure;
using SF.Infrastructure.ControlAddress;
using SF.Infrastructure.PropertyAccess;
using Microsoft.Practices.Unity;
using SF.Utility.Dialogs;

namespace Form.Test01
{
    public partial class UserControl02 : FanUserControl
    {
        public UserControl02()
        {
            InitializeComponent();

            this.Resize += UserControl02_Resize;
        }

        void UserControl02_Resize(object sender, EventArgs e)
        {
            this.textEdit1.Width = this.Width - this.textEdit1.Left - 20;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //IInvokeMethod service = ServiceContainer.CreateInstance().Resolve<IInvokeMethod>("InvokeMethodService");
            //service.Invoke("Form.Test01.UserControl01", "SelectedItemText", "SelectedItemText");

            //string value = this.PropertyAccessService.GetValue(typeof(UserControl01), "SelectedItemText").ToString();
            string value = this.PropertyAccessService.GetValue("Form.Test01.UserControl01", "SelectedItemText1").ToString();
            FanMessageBox.Show(value);
        }

        private void testButton2_Click(object sender, EventArgs e)
        {
            this.InvokeMethodService.Invoke("Form.Test01.UserControl01", "CallMethod01", new object[] { "aaaaa" });
        }
    }
}
