using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SF.Infrastructure;

namespace SF.Framework.UI.DX.RibbonItems
{
    /// <summary>
    /// 按钮控件
    /// by changtan.sun
    /// </summary>
    public class FanBarButtonItem : BarButtonItem
    {
        public string ItemClickMethodName { get; set; }
        public string MouseClickMethodName { get; set; }
        public string ChangeMethodName { get; set; }

        public void button_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(!string.IsNullOrEmpty(this.ItemClickMethodName))
            {
                IInvokeMethod service = ServiceContainer.CreateInstance().Resolve<IInvokeMethod>("InvokeMethodService");
                service.Invoke(this.ItemClickMethodName, new object[1] { sender });
            }
        
        }
    }
}
