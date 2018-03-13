using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Infrastructure.ControlAddress
{
    /// <summary>
    /// 控件寻址服务
    /// </summary>
    public class ControlAddressService : IControlAddress
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ControlAddressService));

        /// <summary>
        /// 面板句柄持有者
        /// </summary>
        private Dictionary<string, Component> panelHookHolder = new Dictionary<string, Component>();
        /// <summary>
        /// 面板上用户控件句柄持有者
        /// </summary>
        private Dictionary<string, Component> userControlHookHolder = new Dictionary<string, Component>();
        /// <summary>
        /// Ribbon/Toolbar/Menu上各个控件句柄持有者
        /// </summary>
        private Dictionary<string, Component> barItemHookHolder = new Dictionary<string, Component>();

        /// <summary>
        /// 类型名和Name的对应关系
        /// 比如UserControl01在布局XML中的name=panel01， 则对应关系是panel01《=》UserControl01
        /// 如果是一般的类，比如TestMath类， 则对应关系是TestMath《=》TestMath
        /// </summary>
        private static Dictionary<string, string> nameAndTypeNameMap = new Dictionary<string, string>();
        private static Dictionary<string, string> typeNameAndNameMap = new Dictionary<string, string>();
        /// <summary>
        /// 注册控件
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="controlType"></param>
        public void Register(string controlName, Component component, ControlTypeEnum controlType)
        {
            if (controlType == ControlTypeEnum.BarItem)
            {
                if (!barItemHookHolder.ContainsKey(controlName))
                {
                    barItemHookHolder.Add(controlName, component);
                }
                else
                {
                    logger.Error(controlName + "控件已存在");
                }
            }
            else if (controlType == ControlTypeEnum.Panel)
            {
                if (!panelHookHolder.ContainsKey(controlName))
                {
                    panelHookHolder.Add(controlName, component);
                }
                else
                {
                    logger.Error(controlName + "面板已存在");
                }
            }
            else if (controlType == ControlTypeEnum.UserControl)
            {
                if (!userControlHookHolder.ContainsKey(controlName))
                {
                    userControlHookHolder.Add(controlName, component);
                    nameAndTypeNameMap.Add(controlName, component.GetType().FullName);
                    typeNameAndNameMap.Add(component.GetType().FullName, controlName);
                }
                else
                {
                    logger.Error(controlName + "控件已存在");
                }
            }
            else
            {
                logger.Error(controlName + "控件不符合类型，不能注册");
            }
        }

        public void Unregister(string controlName, ControlTypeEnum controlType)
        {
            if (controlType == ControlTypeEnum.BarItem)
            {
                if (barItemHookHolder.ContainsKey(controlName))
                {
                    var barItem = barItemHookHolder[controlName];
                    barItemHookHolder.Remove(controlName);
                }
            }
            else if (controlType == ControlTypeEnum.Panel)
            {
                if (panelHookHolder.ContainsKey(controlName))
                {
                    panelHookHolder.Remove(controlName);
                }
            }
            else if (controlType == ControlTypeEnum.UserControl)
            {
                if (userControlHookHolder.ContainsKey(controlName))
                {
                    var userControl = userControlHookHolder[controlName];
                    if (userControl is FanUserControl)
                    {
                        userControl.Dispose();
                    }
                    userControlHookHolder.Remove(controlName);

                    string typeName = nameAndTypeNameMap[controlName];
                    typeNameAndNameMap.Remove(typeName);
                    nameAndTypeNameMap.Remove(controlName);
                }
            }
        }

        public void Unregister()
        {
            foreach (var item in userControlHookHolder)
            {
                var userControl = item.Value;
                if (userControl is ICloseable)
                {
                    (userControl as ICloseable).Close();
                }
            }

            userControlHookHolder.Clear();
            barItemHookHolder.Clear();
            panelHookHolder.Clear();
        }

        public Component GetBarItem(string itemName)
        {
            if (barItemHookHolder.ContainsKey(itemName))
            {
                return barItemHookHolder[itemName];
            }
            logger.Error(itemName + "控件不存在");
            return null;
        }

        public Component GetPanel(string panelName)
        {
            if (panelHookHolder.ContainsKey(panelName))
            {
                return panelHookHolder[panelName];
            }
            logger.Error(panelName + "面板不存在");
            return null;
        }

        public Component GetUserControlInPanel(string panelName)
        {
            //if (!panelName.EndsWith("_UserControl"))
            //{
            //    panelName = panelName + "_UserControl";
            //}

            if (userControlHookHolder.ContainsKey(panelName))
            {
                return userControlHookHolder[panelName];
            }
            logger.Error(panelName + "控件不存在");
            return null;
        }

        public string GetTypeNameByName(string panelName, string defaultName)
        {
            if (nameAndTypeNameMap.ContainsKey(panelName))
            {
                return nameAndTypeNameMap[panelName];
            }
            return defaultName;
        }

        public string GetNameByTypeName(string typeName, string defaultName)
        {
            if (typeNameAndNameMap.ContainsKey(typeName))
            {
                return typeNameAndNameMap[typeName];
            }
            return defaultName;
        }
    }
}
