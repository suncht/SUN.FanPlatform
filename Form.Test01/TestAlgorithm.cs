using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Practices.Unity;
using SF.Infrastructure;

namespace Form.Test01
{
    public class TestAlgorithm
    {
        public int Add(int a, int b)
        {
            MessageBox.Show((a + b).ToString());
            return a + b;
        }

        public int Minus(int a, int b)
        {
            MessageBox.Show((a- b).ToString());
            return a - b;
        }


    }
}
