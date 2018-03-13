using DevExpress.XtraBars;
using SF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace SF.Framework.UI.DX.RibbonItems
{
    /// <summary>
    /// 复选框控件
    /// by changtan.sun
    /// </summary>
    public class FanBarCheckItem : BarCheckItem
    {
        public string ItemClickMethodName { get; set; }
        public string MouseClickMethodName { get; set; }
        public string ChangeMethodName { get; set; }

        public void EventDefinedBarCheckItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ChangeMethodName))
            {
                IInvokeMethod service = ServiceContainer.CreateInstance().Resolve<IInvokeMethod>("InvokeMethodService");
                service.Invoke(this.ChangeMethodName, new object[1] { sender });
            }
        }
    }
}
