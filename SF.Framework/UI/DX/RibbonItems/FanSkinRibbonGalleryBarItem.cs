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
    /// 皮肤设置控件
    /// by changtan.sun
    /// </summary>
    public class FanSkinRibbonGalleryBarItem : SkinRibbonGalleryBarItem
    {
        public string ItemClickMethodName { get; set; }
        public string MouseClickMethodName { get; set; }
        public string ChangeMethodName { get; set; }

        public void EventDefinedSkinRibbonGalleryBarItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.MouseClickMethodName))
            {
                IInvokeMethod service = ServiceContainer.CreateInstance().Resolve<IInvokeMethod>("InvokeMethodService");
                service.Invoke(this.MouseClickMethodName, new object[1] { sender});
            }
        }
    }
}
