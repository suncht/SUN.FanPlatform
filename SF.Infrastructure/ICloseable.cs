using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.Infrastructure
{
    /// <summary>
    /// 关闭接口
    /// </summary>
    public interface ICloseable
    {
        void Close();
    }
}
