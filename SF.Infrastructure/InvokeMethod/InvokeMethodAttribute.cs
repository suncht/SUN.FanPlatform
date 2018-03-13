using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.Infrastructure.InvokeMethod
{
    /// <summary>
    /// 方法可执行的特性
    /// </summary>
    public class InvokeMethodAttribute : Attribute
    {
        public InvokeMethodAttribute() { }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private bool isPlugin = false;

        public bool IsPlugin
        {
            get { return isPlugin; }
            set { isPlugin = value; }
        }
        private bool isCached = true;

        public bool IsCached
        {
            get { return isCached; }
            set { isCached = value; }
        }


    }
}
