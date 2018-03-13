using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SF.Infrastructure
{
    /// <summary>
    /// 控件UI的通用接口
    /// </summary>
    [Obsolete("暂时不用")]
    public interface IFanUiViewPlugin
    {
        //Dictionary<string, Control> CollectControlHooks();

        //void InvokeCallback(Action<object> func);
        //object InvokeCallback(Func<object, object> func);
        //void InvokeCallback(string callbackName, object data);
        //string UiViewName();
    }
}
