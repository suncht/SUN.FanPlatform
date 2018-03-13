using SF.Framework.Exceptions;
using SF.Framework.Layout;
using SF.Framework.UI.DX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Framework.UI
{
    /// <summary>
    /// UI工厂类
    /// </summary>
    public class UIFactory
    {
        private static UIFactory instance;

        private UIFactory()
        {

        }

        public static UIFactory GetInstance()
        {
            if (instance == null)
            {
                lock(typeof(UIFactory)) {
                    if (instance == null)
                    {
                        instance = new UIFactory();
                    }
                }
            }

            return instance;
        }
        /// <summary>
        /// 获取UI生成者
        /// </summary>
        /// <param name="layoutName"></param>
        /// <returns></returns>
        public UIGeneratorBase GetGenerator(string layoutName)
        {
            FanLayoutXmlReader layoutXmlReader = new FanLayoutXmlReader();
            FanLayout layout = layoutXmlReader.LoadLayout(layoutName);
            UIGeneratorBase layoutGenerator = new FanLayoutDxrdUIGenerator();
            layoutGenerator.Layout = layout;
            return layoutGenerator;
        }
    }
}
