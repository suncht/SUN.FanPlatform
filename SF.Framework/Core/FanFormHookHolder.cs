using SF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SF.Framework.Core
{
    /// <summary>
    /// Form控件句柄Holder
    /// by changtan.sun
    /// </summary>
    public class FanFormHookHolder
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(FanFormHookHolder));


        /// <summary>
        /// 控件句柄持有者
        /// </summary>
        private Dictionary<string, Control> controlHookHolder = new Dictionary<string, Control>();
        /// <summary>
        /// UI控件句柄持有者
        /// </summary>
        private Dictionary<string, FanUserControl> uiViewHookHolder = new Dictionary<string, FanUserControl>();


        public Dictionary<string, Control> GetControlHookHolder()
        {
            return this.controlHookHolder;
        }

        /// <summary>
        /// 添加控件句柄
        /// </summary>
        /// <param name="hookHolders"></param>
        public void AddControlHook(Dictionary<string, Control> hookHolders)
        {
            if (hookHolders == null) return;
            foreach (var item in hookHolders)
            {
                AddControlHook(item.Key, item.Value);
            }
        }

        public void AddControlHook(string hookName, Control hookControl)
        {
            if (hookName == null || hookControl == null) return;
            if (!controlHookHolder.ContainsKey(hookName))
            {
                this.controlHookHolder.Add(hookName, hookControl);
            }
            else
            {
                logger.Error("控件句柄[" + hookName + "]已存在，不能添加到控件句柄列表");
            }
        }


        public Dictionary<string, FanUserControl> GetUiViewHookHolder()
        {
            return this.uiViewHookHolder;
        }

        public void AddUiViewHook(string hookName, FanUserControl uiView)
        {
            if (hookName == null || uiView == null) return;
            if (!uiViewHookHolder.ContainsKey(hookName))
            {
                this.uiViewHookHolder.Add(hookName, uiView);
            }
            else
            {
                logger.Error("UI句柄[" + hookName + "]已存在，不能添加到UI句柄列表");
            }
        }

        public void AddUiViewHook(FanUserControl uiView)
        {
            AddUiViewHook(uiView.UiViewName() + "." + uiView.Name, uiView);
        }

        public void AddUiVIewHook(Dictionary<string, FanUserControl> hookHolders)
        {
            if (hookHolders == null) return;
            foreach (var item in hookHolders)
            {
                AddUiViewHook(item.Key, item.Value);
            }
        }

        public void Dispose()
        {

            var controlList = this.GetControlHookHolder().Values;
            Control[] controlArray=new Control[controlList.Count];
            controlList.CopyTo(controlArray, 0);
            for (int i=0;i<controlArray.Length;i++)
            {
                try
                {
                    var item = controlArray[i];
                    if (item != null && item is Control)
                    {
                        ((Control)item).Dispose();
                    }
                    else if (item != null && item is Form)
                    {
                        ((Control)item).Dispose();
                    }
                }
                catch { }
            }
            controlArray = null;
            var uiViewHook = this.GetUiViewHookHolder().Values;
            FanUserControl[] uiAyyay = new FanUserControl[uiViewHook.Count];
            uiViewHook.CopyTo(uiAyyay, 0);
            for (int i = 0; i < uiAyyay.Length;i++ )
            {
                try
                {
                    var item = uiAyyay[i];
                    if (item != null && item is FanUserControl)
                    {
                        ((FanUserControl)item).Dispose();
                    }

                }
                catch { }
            }
            uiAyyay = null;
            this.GetControlHookHolder().Clear();
            this.GetUiViewHookHolder().Clear();

            controlHookHolder = null;
            uiViewHookHolder = null;
        }
    }
}
