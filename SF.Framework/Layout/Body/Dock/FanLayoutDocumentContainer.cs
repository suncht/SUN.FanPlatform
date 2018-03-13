using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Framework.Layout.Body.Dock
{
    /// <summary>
    /// Tab方式Panel的容器
    /// 容器会有多个Tab的Panel加载
    /// by changtan.sun
    /// </summary>
    public class FanLayoutDocumentContainer
    {
        public string Title;
        public string Name;

        public Collection<FanLayoutTabView> TabViews = new Collection<FanLayoutTabView>();
    }
}
