using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Framework.Exceptions
{
    /// <summary>
    /// 布局异常类
    /// by changtan.sun
    /// </summary>
    public class LayoutException : Exception
    {
        public LayoutException(string message): base(message)
        {
        }

        public LayoutException(string message, Exception ex)
            : base(message, ex)
        {
        }

    }
}
