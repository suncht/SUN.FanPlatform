using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace SF.Component.Infrastructure.ComplexPropertyComponent
{
    /// <summary>
    /// 复杂类型调用对象
    /// </summary>
    public class ComplexPropertyEditor : UITypeEditor
    {
        private ComplexTypeBaseDialog dialog;
        public ComplexPropertyEditor()
        { }

        public ComplexPropertyEditor(ComplexTypeBaseDialog dialog)
        {
            this.dialog = dialog;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {

            return UITypeEditorEditStyle.Modal;

        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {

            IWindowsFormsEditorService edSvc =
                (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc != null)
            {
                string content = (value as ISupportJsonSerialization).ToJsonString();
                object tempObject = (value as ISupportJsonSerialization).ParseJsonObject(content);
                dialog.SetSingle(tempObject);
                System.Windows.Forms.DialogResult result= dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    return dialog.GetSingle();
                }

            }

            return value;

        }

        public ComplexTypeBaseDialog GetDialog()
        {
            return dialog;
        }

    }
}
