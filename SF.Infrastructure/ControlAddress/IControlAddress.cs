using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Infrastructure.ControlAddress
{
    public interface IControlAddress
    {
        /// <summary>
        /// 注册控件
        /// </summary>
        void Register(string controlName, Component component, ControlTypeEnum controlType);
        /// <summary>
        /// 取消注册控件
        /// </summary>
        void Unregister(string controlName, ControlTypeEnum controlType);
        /// <summary>
        /// 取消所有注册控件
        /// </summary>
        void Unregister();
        /// <summary>
        /// 获取Ribbon/Toolbar上的控件
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        Component GetBarItem(string itemName);
        /// <summary>
        /// 获取指定面板
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        Component GetPanel(string panelName);
        /// <summary>
        /// 获取指定面板中的控件
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        Component GetUserControlInPanel(string panelName);

        string GetTypeNameByName(string panelName, string defaultName);

        string GetNameByTypeName(string typeName, string defaultName);
    }

    /// <summary>
    /// 控件类型
    /// </summary>
    public enum ControlTypeEnum
    {
        BarItem,
        Panel,
        UserControl
    }
}
