using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace SF.Component.Infrastructure.ComplexPropertyComponent
{
    public class ComplexPropertyControlEditor : UITypeEditor
    {
        private ComplexTypeBaseControl control;
        public ComplexPropertyControlEditor()
        { }

        public ComplexPropertyControlEditor(ComplexTypeBaseControl control)
        {
            this.control = control;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {

            return UITypeEditorEditStyle.DropDown;

        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {

            IWindowsFormsEditorService edSvc =
                (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc != null)
            {
                string content = (value as ISupportJsonSerialization).ToJsonString();
                object tempObject = (value as ISupportJsonSerialization).ParseJsonObject(content);
                control.SetSingle(tempObject);
                edSvc.DropDownControl(control);
                return control.GetSingle();
            }

            return value;

        }

        public ComplexTypeBaseControl GetControl()
        {
            return control;
        } 
    }
}
