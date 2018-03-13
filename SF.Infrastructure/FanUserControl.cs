using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using SF.Infrastructure.PropertyAccess;
using Microsoft.Practices.Unity;
using SF.Infrastructure.ControlAddress;
using System.ComponentModel;

namespace SF.Infrastructure
{
    public class FanUserControl : UserControl, IFanUiViewPlugin, ICloseable
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(FanUserControl));
        /// <summary>  
        /// 是否处于设计器模式  
        /// </summary>  
        private bool isDesignMode = false;  

        /// <summary>
        /// 控件所在的面板
        /// </summary>
        private object ownerPanel = null;

        /// <summary>
        /// 控件句柄持有者
        /// </summary>
        //private Dictionary<string, Control> controlHookHolder = new Dictionary<string, Control>();
        /// <summary>
        /// 面板中控件句柄持有者
        /// </summary>
        //private Dictionary<string, FanUserControl> uiViewHookHolder = new Dictionary<string, FanUserControl>();


        //#region UI控件句柄
        ///// <summary>
        ///// UI控件句柄持有者
        ///// </summary>
        //public Dictionary<string, FanUserControl> UiViewHookHolder
        //{
        //    get { return uiViewHookHolder; }
        //    //set { uiViewHookHolder = value; }
        //}

        //public FanUserControl GetUiViewHook(string hookName)
        //{
        //    if (uiViewHookHolder.ContainsKey(hookName))
        //    {
        //        return uiViewHookHolder[hookName];
        //    }
        //    else
        //    {
        //        logger.Error("找不到UI句柄[" + hookName + "]");
        //    }

        //    return null;
        //}

        //public void AddUiViewHook(string hookName, FanUserControl uiView)
        //{
        //    if (hookName == null || uiView == null) return;
        //    if (!uiViewHookHolder.ContainsKey(hookName))
        //    {
        //        this.uiViewHookHolder.Add(hookName, uiView);
        //    }
        //    else
        //    {
        //        logger.Error("UI句柄[" + hookName + "]已存在，不能添加到UI句柄列表");
        //    }
        //}

        //public void AddUiViewHook(FanUserControl uiView)
        //{
        //    string hookName = this.UiViewName() + "." + uiView.Name;
        //    //this.AddControlHook(hookName, uiView);
        //}

        //public void AddUiViewHook(Dictionary<string, FanUserControl> uiViewHookHolderRange)
        //{
        //    if (uiViewHookHolderRange == null) return;
        //    foreach (var item in uiViewHookHolderRange)
        //    {
        //        AddUiViewHook(item.Key, item.Value);
        //    }
        //}
        //#endregion

        //#region 控件句柄
        ///// <summary>
        ///// 控件句柄持有者
        ///// </summary>
        ////public Dictionary<string, Control> ControlHookHolder
        ////{
        ////    get { return controlHookHolder; }
        ////    //set { controlHookHolder = value; }
        ////}
        ////public Control GetControlHook(string hookName)
        ////{
        ////    if (controlHookHolder.ContainsKey(hookName))
        ////    {
        ////        return controlHookHolder[hookName];
        ////    }
        ////    else
        ////    {
        ////        logger.Error("找不到控件句柄[" + hookName + "]");
        ////    }

        ////    return null;
        ////}

        ////public void AddControlHook(string hookName, Control controlHook)
        ////{
        ////    if (hookName==null || controlHook == null) return;
        ////    if (!controlHookHolder.ContainsKey(hookName))
        ////    {
        ////        this.controlHookHolder.Add(hookName, controlHook);
        ////    }
        ////    else
        ////    {
        ////        logger.Error("控件句柄[" + hookName + "]已存在，不能添加到句柄列表");
        ////    }
        ////}

        ////public void AddControlHook(Control controlHook)
        ////{
        ////    string hookName = this.UiViewName() + "." + controlHook.Name;
        ////    this.AddControlHook(hookName, controlHook);
        ////}

        ////public void AddControlHook(Dictionary<string, Control> controlHookHolderRange)
        ////{
        ////    if (controlHookHolderRange == null) return;
        ////    foreach (var item in controlHookHolderRange)
        ////    {
        ////        AddControlHook(item.Key, item.Value);
        ////    }
        ////}

        //#endregion

        public FanUserControl()
        {
            this.isDesignMode = this.GetIsDesignMode();

            if (!this.isDesignMode)
            {
                ExposeAccessableProperty(this);
            }
        }
        /// <summary>
        /// 向外部暴露可访问的属性
        /// </summary>
        protected void ExposeAccessableProperty(Object instance)
        {
            if (this.DesignMode) return;
            ServiceContainer.CreateInstance().Resolve<IPropertyAccess>("PropertyAccessService").registerObject(instance);
        }

        /// <summary>
        /// 获取“属性访问”服务
        /// </summary>
        /// <returns></returns>
        protected IPropertyAccess PropertyAccessService
        {
            get {
                if (this.DesignMode) return null;
                return ServiceContainer.CreateInstance().Resolve<IPropertyAccess>("PropertyAccessService");
            }
        }

        /// <summary>
        /// 获取“直接访问其他控件对象”服务
        /// </summary>
        protected IControlAddress ControlAddressService
        {
            get
            {
                if (this.DesignMode) return null;
                return  ServiceContainer.CreateInstance().Resolve<IControlAddress>("ControlAddressService");
            }
        }

        protected IInvokeMethod InvokeMethodService
        {
            get
            {
                if (this.DesignMode) return null;
                return ServiceContainer.CreateInstance().Resolve<IInvokeMethod>("InvokeMethodService");
            }
        }

        /// <summary>
        /// 控件所在的面板
        /// </summary>
        public object OwnerPanel
        {
            get
            {
                return this.ownerPanel;
            }
            set
            {
                if (this.ownerPanel == null)
                {
                    this.ownerPanel = value;
                }
            }
        }

        #region 图标集的操作
        private ImageList image16List = new ImageList();
        private ImageList image32List = new ImageList();

        private Collection<Tuple<string, int, Image>> image16CollectionList = null;
        private Collection<Tuple<string, int, Image>> image32CollectionList = null;

        
        public void LoadImage16Collection(Collection<Tuple<string, int, Image>> images)
        {
            this.image16CollectionList = images;
            foreach (var item in image16CollectionList)
            {
                image16List.Images.Add(item.Item3);
            }
        }

        public void LoadImage32Collection(Collection<Tuple<string, int, Image>> images)
        {
            this.image32CollectionList = images;
            foreach (var item in image32CollectionList)
            {
                image32List.Images.Add(item.Item3);
            }
        }

        /// <summary>
        /// Image（16*16）列表
        /// </summary>
        public ImageList Image16List
        {
            get
            {
                return this.image16List;
            }
        }
        /// <summary>
        /// Image（32*32）列表
        /// </summary>
        public ImageList Image32List
        {
            get
            {
                return this.image32List;
            }
        }

        /// <summary>
        /// 获取Image（16*16）的索引号
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public int GetImage16Index(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName)) return -1;
            if (image16CollectionList == null) return -1;
            Tuple<string, int, Image> tuple =  image16CollectionList.FirstOrDefault((t) => { return t.Item1 == imageName; });
            if (tuple == null) return -1;
            return tuple.Item2;
        }

        /// <summary>
        /// 获取Image（32*32）的索引号
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public int GetImage32Index(string imageName)
        {
            if (image32CollectionList == null) return -1;
            Tuple<string, int, Image> tuple = image32CollectionList.FirstOrDefault((t) => { return t.Item1 == imageName; });
            if (tuple == null) return -1;
            return tuple.Item2;
        }

        /// <summary>
        /// 获取Image（16*16）的文件名
        /// </summary>
        /// <param name="imageIndex"></param>
        /// <returns></returns>
        public string GetImage16Name(int  imageIndex)
        {
            if (image16CollectionList == null) return string.Empty;
            Tuple<string, int, Image> tuple = image16CollectionList.FirstOrDefault((t) => { return t.Item2 == imageIndex; });
            if (tuple == null) return string.Empty;
            return tuple.Item1;
        }

        /// <summary>
        /// 获取Image（32*32）的文件名
        /// </summary>
        /// <param name="imageIndex"></param>
        /// <returns></returns>
        public string GetImage32Name(int imageIndex)
        {
            if (image32CollectionList == null) return string.Empty;
            Tuple<string, int, Image> tuple = image32CollectionList.FirstOrDefault((t) => { return t.Item2 == imageIndex; });
            if (tuple == null) return string.Empty;
            return tuple.Item1;
        }

        #endregion

        //public virtual Dictionary<string, Control> CollectControlHooks()
        //{
        //    return controlHookHolder;
        //}

        public virtual string UiViewName()
        {
            return this.Name;
        }

        public virtual void InvokeCallback(string callbackName, object data)
        {
            throw new NotImplementedException("未实现回调触发");
        }


        public virtual void InvokeCallback(Action<object> func)
        {
            throw new NotImplementedException();
        }

        public virtual object InvokeCallback(Func<object, object> func)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            //if (controlHookHolder != null)
            //{
            //    foreach (var item in controlHookHolder)
            //    {
            //        if (item.Value != null && item.Value is Control)
            //        {
            //            ((Control)item.Value).Dispose();
            //        }
            //        else if (item.Value != null && item.Value is Form)
            //        {
            //            ((Control)item.Value).Dispose();
            //        }
            //        //controlHookHolder[item.Key] = null;
            //    }

            //    controlHookHolder.Clear();

            //}
            
            base.Dispose(true);
        }


        public virtual void Close()
        {
            this.Dispose(true);
        }

        /// <summary>  
        /// 获取当前是否处于设计器模式  
        /// </summary>  
        /// <remarks>  
        /// 在程序初始化时获取一次比较准确，若需要时获取可能由于布局嵌套导致获取不正确，如GridControl-GridView组合。  
        /// </remarks>  
        /// <returns>是否为设计器模式</returns>  
        private bool GetIsDesignMode()
        {
            return (this.GetService(typeof(System.ComponentModel.Design.IDesignerHost)) != null
                || LicenseManager.UsageMode == LicenseUsageMode.Designtime);
        } 

    }
}
