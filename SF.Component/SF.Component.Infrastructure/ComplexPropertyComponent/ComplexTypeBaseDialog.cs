using System;
using System.Windows.Forms;

namespace SF.Component.Infrastructure.ComplexPropertyComponent
{
    /// <summary>
    /// 属性面板复杂类型编辑对话框，用户编辑复杂对象应继承
    /// </summary>
    public partial class ComplexTypeBaseDialog : Form,IGetSingle<object>
    {
       
        public ComplexTypeBaseDialog()
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
