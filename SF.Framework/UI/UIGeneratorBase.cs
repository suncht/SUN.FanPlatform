
using SF.Framework.Layout;
using SF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SF.Framework.UI
{
    /// <summary>
    /// UI界面生成类的基类
    /// author: 孙昌坦
    /// </summary>
    public abstract class UIGeneratorBase : IUIGenerator
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(FanRibbonFormContainer));

        /// <summary>
        /// Form目标容器
        /// </summary>
        public Form TargetContainer;
        /// <summary>
        /// Form界面的Header部分宿主控件
        /// </summary>
        public Control HeaderHostControl;
        /// <summary>
        /// Form界面的Body部分宿主控件
        /// </summary>
        public Control BodyHostControl;
        /// <summary>
        /// Form界面的Foot部分宿主控件
        /// </summary>
        public Control FootHostControl;

        protected Collection<FanUserControl> _uiViews_cache = new Collection<FanUserControl>();

        /// <summary>
        /// 布局配置信息对象
        /// </summary>
        public FanLayout Layout;

        /// <summary>
        /// 说明信息
        /// </summary>
        public string Description;


        public void GenerateLayout()
        {
            if (Layout == null)
            {
                throw new InvalidOperationException("布局配置信息不存在");
            }
            //TargetContainer.Text = Layout.Title;

            TargetContainer.SuspendLayout();
            //前处理（生成布局前的准备工作）
            BeforeGenerate();
            //生成Header部分（比如：Ribbon、工具栏）
            GenerateHeader();
            //生成主体部分（Form中心内容部分）
            GenerateBody();
            //生成Foot部分（比如：状态栏等）
            GenerateFoot();
            //后处理（生成完后的清理工作）
            AfterGenerate();

            TargetContainer.ResumeLayout(false);
            TargetContainer.PerformLayout();
        }
        /// <summary>
        /// 前处理
        /// </summary>
        public virtual void BeforeGenerate()
        {
        }

        /// <summary>
        /// 生成Header部分的布局界面
        /// </summary>
        public abstract void GenerateHeader();
        /// <summary>
        /// 生成Body部分的布局界面
        /// </summary>
        public abstract void GenerateBody();
        /// <summary>
        /// 生成Foot部分的布局界面
        /// </summary>
        public abstract void GenerateFoot();

        /// <summary>
        /// 后处理
        /// </summary>
        public virtual void AfterGenerate()
        {
        }
    }
}
