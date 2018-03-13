using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SF.Component.Infrastructure.ComplexPropertyComponent
{
    public partial class ComplexTypeBaseControl : UserControl, IGetSingle<object>
    {
        public ComplexTypeBaseControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置对象值
        /// </summary>
        /// <param name="input">编辑对象</param>
        public virtual void SetSingle(object input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取对象值，在关闭对话框后调用
        /// </summary>
        /// <returns>在</returns>
        public virtual object GetSingle()
        {
            throw new NotImplementedException();
        }

    }
}
