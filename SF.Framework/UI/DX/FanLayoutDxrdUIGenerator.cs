using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Ribbon;
using SF.Framework.Layout;
using SF.Framework.Layout.Body.Dock;
using SF.Framework.Layout.Enums;
using SF.Framework.Layout.Header.Ribbon;
using SF.Framework.UI.DX.RibbonItems;
using SF.Infrastructure;
using SF.Infrastructure.ControlAddress;
using SF.Infrastructure.SystemParams;
using SF.Utility.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using SF.Utility.Dialogs;

namespace SF.Framework.UI.DX
{
    /// <summary>
    /// 生成 用DevExpress开发的的Ribbon + Dock风格的布局界面
    /// by changtan.sun
    /// </summary>
    public class FanLayoutDxrdUIGenerator : UIGeneratorBase
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(FanLayoutDxrdUIGenerator));

        public FanLayoutDxrdUIGenerator()
        {
            this.Description = "DevExpress风格的Ribbon + Dock的布局";
        } 

        /// <summary>
        /// UI所在的Form容器
        /// </summary>
        public FanRibbonFormContainer Container
        {
            get
            {
                return (FanRibbonFormContainer)TargetContainer;
            }
        }
        /// <summary>
        /// 生成UI前，需要处理的方法
        /// </summary>
        public override void BeforeGenerate()
        {
            var ribbon = (RibbonControl)HeaderHostControl;
            ribbon.BeginInit();
        }

        public override void GenerateHeader()
        {
            FanLayoutRibbon ribbonLayout = (FanLayoutRibbon)Layout;
            GenerateLayoutHeader(ribbonLayout);
            
        }

        public override void GenerateBody()
        {
            FanLayoutRibbon ribbonLayout = (FanLayoutRibbon)Layout;
            GenerateLayoutBody(ribbonLayout);
        }

        public override void GenerateFoot()
        {
            
        }

        public override void AfterGenerate()
        {
            //foreach (FanUserControl uiView in _uiViews_cache)
            //{
            //    uiView.AddUiViewHook(Container.HookHolder.GetUiViewHookHolder());
            //    uiView.AddControlHook(Container.HookHolder.GetControlHookHolder());
            //}

            var ribbon = (RibbonControl)HeaderHostControl;
            ribbon.EndInit();
        }

        private IControlAddress GetAddressService()
        {
            return ServiceContainer.CreateInstance().Resolve<IControlAddress>("ControlAddressService");
        }

        #region 生成Header
        #region 生成Ribbon布局
        private void GenerateLayoutHeader(FanLayoutRibbon ribbonLayout)
        {
            var ribbon =(RibbonControl)HeaderHostControl;
            if (ribbonLayout.Header.ShowApplicationButton)
            {
                ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.True;
            }

            //Ribbon控件生成器
            RibbonItemGenerator itemGenerator = new RibbonItemGenerator();

            foreach (FanLayoutRibbonPage page in ribbonLayout.Pages)
            {
                RibbonPage ribbonPage = new RibbonPage();
                ribbonPage.Text = page.Title;

                foreach (FanLayoutRibbonPageGroup pageGroup in page.PageGroups)
                {
                    RibbonPageGroup ribbonPageGroup = new RibbonPageGroup();
                    ribbonPageGroup.Text = pageGroup.Title;

                    foreach (FanLayoutRibbonItem barItem in pageGroup.Items)
                    {
                        BarItem item = itemGenerator.GenerateItem(barItem);
                        ribbonPageGroup.ItemLinks.Add(item);
                        ribbon.Items.Add(item);

                        //将Ribbon上控件句柄注册到寻址服务中
                        //只有指定名称的ribbon控件才能通过寻址服务访问
                        if(!string.IsNullOrEmpty(barItem.Name))
                        { 
                            this.GetAddressService().Register(barItem.Name, item, ControlTypeEnum.BarItem);
                        }
                        //if (barItem.Type == "button") //按钮
                        //{
                        //    FanBarButtonItem button = null;
                        //    GenerateBarButton(barItem, out button);
                        //    ribbonPageGroup.ItemLinks.Add(button);
                        //    ribbon.Items.Add(button);
                        //}
                        //else if (barItem.Type == "checkbox") //复选框
                        //{
                        //    FanBarCheckItem checkbox = null;
                        //    GenerateBarCheckbox(barItem, out checkbox);
                        //    ribbonPageGroup.ItemLinks.Add(checkbox);
                        //    ribbon.Items.Add(checkbox);
                        //}
                        //else if (barItem.Type == "radio") //单选
                        //{
                        //    FanBarCheckItem radio = null;
                        //    GenerateBarRadio(barItem, out radio);
                        //    ribbonPageGroup.ItemLinks.Add(radio);
                        //    ribbon.Items.Add(radio);
                        //}
                        //else if (barItem.Type == "toggleSwitch") //开关按钮
                        //{
                        //    FanBarToggleSwitchItem switchItem = null;
                        //    GenerateBarToggleSwitch(barItem, out switchItem);
                        //    ribbonPageGroup.ItemLinks.Add(switchItem);
                        //    ribbon.Items.Add(switchItem);
                        //}
                        //else if (barItem.Type == "skin") //皮肤控件
                        //{
                        //    FanSkinRibbonGalleryBarItem skinGallery = null;
                        //    GenerateBarSkinRibbonGallery(barItem, out skinGallery);
                        //    ribbonPageGroup.ItemLinks.Add(skinGallery);
                        //    ribbon.Items.Add(skinGallery);
                        //}
                    }

                    ribbonPage.Groups.Add(ribbonPageGroup);
                }

                ribbon.Pages.Add(ribbonPage);
                ribbon.ExpandCollapseItem.Id = 0;
            }
        }

        #endregion

        #region 生成Ribbon上的控件

        
        #endregion

        #endregion

        #region 生成Body
        private void GenerateLayoutBody(FanLayout layout)
        {
            if (layout.Body.Style == FanLayoutBodyStyleEnum.DOCK)
            {
                FanLayoutDock body = (FanLayoutDock)layout.Body;
                GenerateLayoutBodyDock(body);
            }
        }

        #region 生成Dock布局
        private void GenerateLayoutBodyDock(FanLayoutDock body)
        {
            Container.GetDockManager().BeginInit();
            DockPanel[] list = new DockPanel[body.Panels.Count];

            int index = 0;
            foreach (FanLayoutPanel panel in body.Panels)
            {
                DockPanel dockPanel = new DockPanel();
                dockPanel.Options.ShowCloseButton = true;
                dockPanel.Options.ShowAutoHideButton = true;
                dockPanel.Options.ShowMaximizeButton = true;
                dockPanel.ClosingPanel += dockPanel_ClosingPanel;
                if (panel.TabView) //是否属于TabView面板
                {
                    //生成DockPanel对象
                    dockPanel.DockedAsTabbedDocument = true;
                    dockPanel.ID = new System.Guid(panel.Guid);
                    dockPanel.Location = new System.Drawing.Point(0, 200);
                    dockPanel.Name = panel.Name;
                    dockPanel.Width = panel.Width;
                    dockPanel.Height = panel.Height;
                    dockPanel.Text = panel.Title;

                    ControlContainer container = new ControlContainer();
                    container.Location = new System.Drawing.Point(0, 0);
                    container.Dock = DockStyle.Fill;
                    container.Name = panel.Name + "_Container";
                    container.Size = new System.Drawing.Size(493, 242);
                    container.TabIndex = 0;

                    dockPanel.Controls.Add(container);

                    //加载UiView界面
                    LoadAssemblyToPanel(dockPanel, container, panel.Assembly, panel.AssemblyType);


                    //将Panel加载到TabbedView中
                    DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(Container.GetComponents());
                    document.Caption = panel.Title;
                    document.ControlName = panel.Name;
                    document.FloatLocation = new System.Drawing.Point(0, 0);
                    document.FloatSize = new System.Drawing.Size(200, 200);
                    document.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
                    document.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
                    document.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
                    //Container.GetTabbedView().AddDocument(dockPanel);
                    //Container.GetTabbedView().ActivateDocument(dockPanel);

                    Container.GetTabbedView().Documents.Add(document);
                }
                else
                {
                    //生成DockPanel对象
                    dockPanel.Dock = FanLayoutDockPositionEnumHelper.toDockingStyle(panel.DockPosistion);
                    dockPanel.ID = new System.Guid(panel.Guid);
                    dockPanel.Location = new System.Drawing.Point(0, 200);
                    dockPanel.Name = panel.Name;
                    dockPanel.OriginalSize = new System.Drawing.Size(panel.Width, panel.Height);
                    dockPanel.Width = panel.Width;
                    dockPanel.Height = panel.Height;
                    dockPanel.Text = panel.Title;

                    if (panel.ChildPanelItems.Count == 0) //没有子面板
                    {
                        ControlContainer container = new ControlContainer();
                        container.Location = new System.Drawing.Point(0, 0);
                        container.Dock = DockStyle.Fill;
                        container.Name = panel.Name + "_Container";
                        dockPanel.OriginalSize = new System.Drawing.Size(dockPanel.Width, dockPanel.Height);
                        dockPanel.Width = dockPanel.Width;
                        dockPanel.Height = dockPanel.Height;
                        container.TabIndex = 0;

                        dockPanel.Controls.Add(container);

                        //将Assembly加载到面板中,构建面板内容
                        LoadAssemblyToPanel(dockPanel, container, panel.Assembly, panel.AssemblyType);
                    }
                    else  //有子面板
                    {
                        //生成子Panel
                        GenerateLayoutChildPanels(panel.ChildPanelItems, dockPanel);
                    }
                }
                list[index++] = dockPanel;
                
                //将面板句柄注册到寻址服务中
                this.GetAddressService().Register(dockPanel.Name, dockPanel, ControlTypeEnum.Panel);
            }
            Container.GetDockManager().RootPanels.AddRange(list);
            Container.Controls.AddRange(list);

            Container.GetDockManager().EndInit();

            //GenerateLayoutDocumentContainer(body);
        }


        private void GenerateLayoutChildPanels(Collection<FanLayoutPanel> childPanelItems, DockPanel parentPanel)
        {
            foreach (FanLayoutPanel childPanel in childPanelItems)
            {
                DockPanel dockChildPanel = new DockPanel();
                dockChildPanel.Dock = DockingStyle.Fill;//;FanLayoutDockPositionEnumHelper.toDockingStyle(childPanel.DockPosistion);
                dockChildPanel.ID = new System.Guid(childPanel.Guid);
                dockChildPanel.Location = new System.Drawing.Point(0, 200);
                dockChildPanel.Name = childPanel.Name;
                dockChildPanel.OriginalSize = new System.Drawing.Size(200, 200);
                dockChildPanel.Size = new System.Drawing.Size(200, 271);
                dockChildPanel.Text = childPanel.Title;
                dockChildPanel.Options.ShowCloseButton = true;
                dockChildPanel.Options.ShowAutoHideButton = true;
                dockChildPanel.Options.ShowMaximizeButton = true;
                dockChildPanel.ClosingPanel += dockPanel_ClosingPanel;

                ControlContainer container = new ControlContainer();
                container.Location = new System.Drawing.Point(0, 0);
                container.Dock = DockStyle.Fill;
                container.Name = childPanel.Name + "_Container";
                container.Size = new System.Drawing.Size(493, 242);
                container.TabIndex = 0;

                dockChildPanel.Controls.Add(container);

                parentPanel.Controls.Add(dockChildPanel);
                
                //将Assembly加载到面板中,构建面板内容
                LoadAssemblyToPanel(dockChildPanel, container, childPanel.Assembly, childPanel.AssemblyType);

                //将面板注册到寻址服务中
                this.GetAddressService().Register(dockChildPanel.Name, dockChildPanel, ControlTypeEnum.Panel);
            }
        }
        /// <summary>
        /// 将Assembly加载到面板中,构建面板内容
        /// </summary>
        /// <param name="controlContainer"></param>
        /// <param name="uiView"></param>
        private void LoadAssemblyToPanel(DockPanel dockPanel, ControlContainer controlContainer, string assembly, string assemblyType)
        {
            if (string.IsNullOrWhiteSpace(assembly)) return;

            string[] temp = assembly.Split(',');
            if (temp.Length < 2) return;

            string assembleName = temp[1] + "." + assemblyType;
            string typeName = temp[0];
            object obj = AssemblyHelper.GetInstanceFromAssembly(assembleName, typeName);

            if (obj == null)
            {
                logger.Error("加载【" + typeName + "】插件中的【" + assembleName + "】失败，并忽略该插件");
                FanMessageBox.Warning("加载【" + typeName + "】插件中的【" + assembleName + "】失败，并忽略该插件", "系统消息");
                return;
            }

                
            if (obj is FanUserControl)
            {
                FanUserControl userControl = (FanUserControl)obj;
                userControl.Name = dockPanel.Name; // +"_UserControl";
                userControl.OwnerPanel = dockPanel;
                userControl.Dock = DockStyle.Fill;
                //将图标集转递给各个面板
                userControl.LoadImage16Collection(GlobalParamService.SystemSettingParam.Image16CollectionList);
                userControl.LoadImage32Collection(GlobalParamService.SystemSettingParam.Image32CollectionList);
                
                controlContainer.Controls.Add(userControl);

                //将面板中的用户控件注册到寻址服务中
                this.GetAddressService().Register(userControl.Name, userControl, ControlTypeEnum.UserControl);

                //this.Container.HookHolder.AddControlHook(userControl.ControlHookHolder);
                //this.Container.HookHolder.AddUiViewHook(userControl);
                //_uiViews_cache.Add(userControl);
            }
            else
            {
                logger.Error("加载【" + typeName + "】插件中的【" + assembleName + "】不是FanUserControl标准用户控件，并忽略该插件");
                FanMessageBox.Show("加载【" + typeName + "】插件中的【" + assembleName + "】不是FanUserControl标准用户控件，并忽略该插件", "系统消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //else if (obj is UserControl)
            //{
            //    UserControl userControl = (UserControl)obj;
            //    userControl.Dock = DockStyle.Fill;
            //    controlContainer.Controls.Add(userControl);
            //}
            
        }

        //private void GenerateLayoutDocumentContainer(FanLayoutDock body)
        //{
        //    DevExpress.XtraBars.Docking2010.DocumentManager documentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(Container.GetComponents());
        //    DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(Container.GetComponents());
        //    DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup documentGroup = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(Container.GetComponents());
        //    DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(Container.GetComponents());
        //    DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer dockingContainer = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer();

        //    ((System.ComponentModel.ISupportInitialize)(documentManager)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(tabbedView)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(documentGroup)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(document)).BeginInit();
        //    this.Container.SuspendLayout();

        //    //TabbedView tabbedView = Container.GetTabbedView();
        //    documentManager.ContainerControl = Container;
        //    documentManager.View = tabbedView;
        //    documentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {tabbedView});

        //    document.Caption = body.DocumentContainer.Title;
        //    document.ControlName = body.DocumentContainer.Name;
        //    document.FloatLocation = new System.Drawing.Point(0, 0);
        //    document.FloatSize = new System.Drawing.Size(200, 200);
        //    document.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
        //    document.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
        //    document.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;

        //    documentGroup.Items.Add(document);



        //    tabbedView.DocumentGrouSF.Add(documentGroup);
        //    tabbedView.Documents.Add(document);
        //    //tabbedView.Orientation = System.Windows.Forms.Orientation.Vertical;
        //    tabbedView.RootContainer.Element = null;
        //    dockingContainer.Element = documentGroup;
        //    tabbedView.RootContainer.Nodes.Add(dockingContainer);
        //    //tabbedView.RootContainer.Orientation = System.Windows.Forms.Orientation.Vertical;

        //    ((System.ComponentModel.ISupportInitialize)(documentManager)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(tabbedView)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(documentGroup)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(document)).EndInit();
        //    this.Container.ResumeLayout();
        //}

        #endregion
        #endregion

        void dockPanel_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            DialogResult dr = FanMessageBox.Question("是否要关闭该面板？如果要恢复该面板，请App.conf中layout_enable_save配置项改成false", "系统提示", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {

            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
