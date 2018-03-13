using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.IO;
using Microsoft.Practices.Unity;
using PS.Infrastructure;
using PS.Infrastructure.PropertyAccess;
using System.Collections;
using System.Text;

namespace Pera.Form.Test
{
    public partial class TestControl01 : PeraUserControl
    {

        public TestControl01()
        {
            InitializeComponent();
            this.Load += TestControl01_Load;
            
        }

        void TestControl01_Load(object sender, EventArgs e)
        {

            
        }

        
    }
}
