using System.Windows.Forms;
using PS.Infrastructure;
using PS.Infrastructure.PropertyAccess;
//using PS.Component.PropertyGrid;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace PeraForm.Test01
{
    public partial class PropertyGridTest : PeraUserControl
    {
        //WCPropertyGrid pg;
        public PropertyGridTest()
        {
            InitializeComponent();
            

            InitForService();
        }

        private void InitForService()
        {
            
        }

        
    }
}
