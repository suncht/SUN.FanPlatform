using System;
using SF.Infrastructure;
using DevExpress.XtraBars.Docking;
using SF.Infrastructure.ControlAddress;
using Microsoft.Practices.Unity;
using SF.Infrastructure.PropertyAccess;
using SF.Utility.Dialogs;
using SF.Infrastructure.InvokeMethod;

namespace Form.Test01
{
    public partial class UserControl01 : FanUserControl
    {
        public UserControl01()
        {
            InitializeComponent();


            //this.AddControlHook(this.listBoxControl1);
            this.Resize += UserControl01_Resize;

        }

        void UserControl01_Resize(object sender, EventArgs e)
        {
            
        }


        public override void InvokeCallback(string callbackName, object data)
        {
            if ("add" == callbackName)
            {
                listBoxControl1.Items.Add(data.ToString());
            }
        }

        public override object InvokeCallback(Func<object, object> func)
        {
            var a = func("aaa");

            return a + "111";
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.OwnerPanel is DockPanel)
            {
                (this.OwnerPanel as DockPanel).Text = "测试" +　this.listBoxControl1.SelectedValue;
            }

            (this.ControlAddressService.GetPanel("panel3") as DockPanel).Text = "寻址服务测试" + this.listBoxControl1.SelectedValue;

        }

        [InvokeMethodAttribute]
        public void CallMethod01(string param)
        {
            FanMessageBox.Show("参数：" + param);
        }

        [PropertyAccessAttribute]
        public string SelectedItemText
        {
            get
            {
                return this.listBoxControl1.SelectedItem.ToString();
            }

            set
            {
                this.listBoxControl1.SelectedItem = value;
            }
        }
    }
}
