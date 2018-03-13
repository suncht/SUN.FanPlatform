using System.ComponentModel;

namespace SF.Component.Infrastructure.ComplexPropertyComponent
{
    /// <summary>
    /// 下拉列表类型值提供器，属性面板中指来源不同时用户应基础
    /// </summary>
    public class DropDownListConverter:StringConverter
    {
        object[] m_Objects;
        public void SetValue(object[] objects)
        {
            m_Objects = objects;
        }

        public virtual void SetValue(string initValue)
        {
            string[] values=initValue.Split(new char[] { ',' });
            SetValue(values);
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;  
        }
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(m_Objects);
        }
    }
}
