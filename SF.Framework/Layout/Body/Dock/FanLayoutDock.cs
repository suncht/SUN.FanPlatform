using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Framework.Layout.Body.Dock
{
    /// <summary>
    /// Dock方式的主体布局
    /// </summary>
    public class FanLayoutDock: FanLayoutBody
    {
        public Collection<FanLayoutPanel> Panels = new Collection<FanLayoutPanel>();

        public FanLayoutDocumentContainer DocumentContainer = new FanLayoutDocumentContainer();
    }
}
