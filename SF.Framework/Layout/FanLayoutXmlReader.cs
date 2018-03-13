using SF.Framework.Layout.Body.Default;
using SF.Framework.Layout.Body.Dock;
using SF.Framework.Layout.Body.Fixed;
using SF.Framework.Layout.Enums;
using SF.Framework.Layout.Header.Ribbon;
using SF.Framework.Layout.Header.Toolbar;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using SF.Utility.Utils;

namespace SF.Framework.Layout
{
    /// <summary>
    /// 布局文件XML读取器
    /// </summary>
    public class FanLayoutXmlReader
    {
        private FanLayout layoutObj;

        private string title;
        private string desc;
        /// <summary>
        /// 从xml中读取布局对象
        /// </summary>
        /// <returns></returns>
        public FanLayout LoadLayout(string layoutTemlateName)
        {
            string xml = Directory.GetCurrentDirectory() + "\\Config\\layout\\" + layoutTemlateName + ".xml";
            XmlDocument doc = XmlLoader.GetXmlDocByFilePath(xml);
            LoadLayoutFromXml(doc);
            return layoutObj;
        }

        /// <summary>
        /// 从Xml中读取并加载布局
        /// </summary>
        /// <param name="doc"></param>
        private void LoadLayoutFromXml(XmlDocument doc)
        {
            ParseNodeLayout(doc);
            //XmlNode headerNode = ParseNodeHeader(layoutNode);
            //XmlNode pagesNode = ParseNodePages(headerNode);
            //XmlNodeList pageList = ParseNodePage(pagesNode);

        }

        /// <summary>
        /// 解析布局
        /// </summary>
        /// <param name="node"></param>
        private void ParseNodeLayout(XmlNode node)
        {
            XmlNode layoutNode = XmlLoader.GetFirstChildNodeByName(node, "layout");
            if (layoutNode == null) return;
            //string skin = XmlLoader.ReadAttrValue(layoutNode, "skin");
            //title = XmlLoader.ReadAttrValue(layoutNode, "title");
            desc = XmlLoader.ReadAttrValue(layoutNode, "description");
            //解析Ribbon部分
            ParseNodeHeader(layoutNode);
            //解析主体Body部分
            ParseNodeBody(layoutNode);
        }

        #region 解析Header部分
        private void ParseNodeHeader(XmlNode node)
        {
            XmlNode headerNode = XmlLoader.GetFirstChildNodeByName(node, "header");
            if (headerNode == null) return;
            string style = XmlLoader.ReadAttrValue(headerNode, "style");
            if (style == "ribbon")
            {
                layoutObj = new FanLayoutRibbon();
                layoutObj.Description = XmlLoader.ReadAttrValue(headerNode, "description");
                //layoutObj.Title = title;
                layoutObj.Description = desc;
                layoutObj.Header = new FanLayoutHeader();
                layoutObj.Header.Style = Enums.FanLayoutHeaderStyleEnum.RIBBON;
                layoutObj.Header.ShowApplicationButton = XmlLoader.ReadAttrBoolValue(headerNode, "showApplicationButton");
                //解析Ribbon Page部分
                ParseNodeRibbonPages(headerNode);
            }
            else if (style == "toolbar")
            {
                layoutObj = new FanLayoutToolbar();
                layoutObj.Description = XmlLoader.ReadAttrValue(headerNode, "description");
                layoutObj.Header = new FanLayoutHeader();
                layoutObj.Header.Style = Enums.FanLayoutHeaderStyleEnum.TOOLBAR;
                //layoutObj.Skin = FanLayoutSkinEnumHelper.Parse(skin);
            }
        }

        #region 加载layout_ribbon.xml
        /// <summary>
        /// 解析Ribbon Page部分
        /// </summary>
        /// <param name="node"></param>
        private void ParseNodeRibbonPages(XmlNode node)
        {
            XmlNode pagesNode = XmlLoader.GetFirstChildNodeByName(node, "pages");
            if (pagesNode == null) return;

            ParseNodeRibbonPage(pagesNode);
        }
        /// <summary>
        /// 解析每个Ribbon Page
        /// </summary>
        /// <param name="node"></param>
        private void ParseNodeRibbonPage(XmlNode node)
        {
            XmlNodeList pageNodeList = XmlLoader.GetChildNodesByName(node, "page");
            if (pageNodeList == null || pageNodeList.Count == 0) return;
            if (layoutObj is FanLayoutRibbon)
            {
                var layoutRibbonObj = (FanLayoutRibbon) layoutObj;
                foreach (XmlNode pageNode in pageNodeList)
                {
                    string title = XmlLoader.ReadAttrValue(pageNode, "title"); //Group的标题
                    FanLayoutRibbonPage page = new FanLayoutRibbonPage()
                    {
                        Title = title
                    };

                    ParseNodeRibbonPageGroup(pageNode, page);

                    layoutRibbonObj.Pages.Add(page);
                }
            }
            else if (layoutObj is FanLayoutToolbar)
            {
                foreach (var page in pageNodeList)
                {

                }
            }
            
        }

        private void ParseNodeRibbonPageGroup(XmlNode node, FanLayoutRibbonPage page)
        {
            XmlNodeList pageGroupNodeList = XmlLoader.GetChildNodesByName(node, "pageGroup");
            if (pageGroupNodeList == null || pageGroupNodeList.Count == 0) return;
            foreach (XmlNode pageGroupNode in pageGroupNodeList)
            {
                string title = XmlLoader.ReadAttrValue(pageGroupNode, "title");
                FanLayoutRibbonPageGroup pageGroup = new FanLayoutRibbonPageGroup();
                pageGroup.Title = title;
                //解析每个Group中的控件
                ParseNodeRibbonItem(pageGroupNode, pageGroup);

                page.PageGroups.Add(pageGroup);
            }
        }
        /// <summary>
        /// 解析每个Ribbon的控件
        /// </summary>
        /// <param name="node"></param>
        /// <param name="pageGroup"></param>
        private void ParseNodeRibbonItem(XmlNode node, FanLayoutRibbonPageGroup pageGroup)
        {
            XmlNodeList itemNodeList = XmlLoader.GetChildNodesByName(node, "item");
            if (itemNodeList == null || itemNodeList.Count == 0) return;
            foreach (XmlNode barItemNode in itemNodeList)
            {
                pageGroup.Items.Add(new FanLayoutRibbonItem()
                {
                    Title = XmlLoader.ReadAttrValue(barItemNode, "title"),
                    Type = XmlLoader.ReadAttrValue(barItemNode, "type"),
                    Name = XmlLoader.ReadAttrValue(barItemNode, "name"),
                    Tip = XmlLoader.ReadAttrValue(barItemNode, "tip"),
                    ImageIndex = XmlLoader.ReadAttrValue(barItemNode, "image-index"),
                    Image16 = XmlLoader.ReadAttrValue(barItemNode, "image16"),
                    Image32 = XmlLoader.ReadAttrValue(barItemNode, "image32"),
                    Assembly = XmlLoader.ReadAttrValue(barItemNode, "assembly"),
                    AssemblyType = XmlLoader.ReadAttrValue(barItemNode, "assembly-type"),
                    Event_OnClick = XmlLoader.ReadAttrValue(barItemNode, "onClick"),
                    Event_OnMouseClick = XmlLoader.ReadAttrValue(barItemNode, "onMouseClick"),
                    Event_OnChange = XmlLoader.ReadAttrValue(barItemNode, "onChange"),
                    Checked = ("true"==XmlLoader.ReadAttrValue(barItemNode, "checked")),
                    Enabled = !("false" == XmlLoader.ReadAttrValue(barItemNode, "enabled")),
                });
            }

        }

        #endregion

        #endregion

        #region 解析Body部分
        private void ParseNodeBody(XmlNode node)
        {
            XmlNode bodyNode = XmlLoader.GetFirstChildNodeByName(node, "body");
            if (bodyNode == null) return;

            string style = XmlLoader.ReadAttrValue(bodyNode, "style");

            if (style == "dock")
            {
                layoutObj.Body = new FanLayoutDock();
                layoutObj.Body.Style = FanLayoutBodyStyleEnum.DOCK;

                ParseNodeBodyPanel(bodyNode, layoutObj.Body);
                //ParseNodeBodyDocumentContainer(bodyNode, layoutObj.Body);
            }
            else if(style=="fixed")
            {
                layoutObj.Body = new FanLayoutFixed();
                layoutObj.Body.Style = FanLayoutBodyStyleEnum.FIXED;
            }
            else
            {
                layoutObj.Body = new FanLayoutDefault();
                layoutObj.Body.Style = FanLayoutBodyStyleEnum.DEFAULT;
            }
        }
        /// <summary>
        /// 解析Panel
        /// </summary>
        /// <param name="node"></param>
        /// <param name="layoutBody"></param>
        private void ParseNodeBodyPanel(XmlNode node, FanLayoutBody layoutBody)
        {
            XmlNodeList panelNodeList = XmlLoader.GetChildNodesByName(node, "panel");
            if (panelNodeList == null || panelNodeList.Count == 0) return;
            foreach (XmlNode panelNode in panelNodeList)
            {
                if (layoutBody is FanLayoutDock)
                {
                    FanLayoutDock bodyDock = (FanLayoutDock)layoutBody;

                    FanLayoutPanel panel = new FanLayoutPanel();
                    panel.Guid = XmlLoader.ReadAttrValue(panelNode, "guid");
                    panel.Title = XmlLoader.ReadAttrValue(panelNode, "title");
                    panel.Name = XmlLoader.ReadAttrValue(panelNode, "name");
                    panel.Width = XmlLoader.ReadAttrIntValue(panelNode, "width", 200);
                    panel.Height = XmlLoader.ReadAttrIntValue(panelNode, "height", 300);
                    panel.DockPosistion = FanLayoutDockPositionEnumHelper.Parse(XmlLoader.ReadAttrValue(panelNode, "position"));
                    panel.TabView = ("true" == XmlLoader.ReadAttrValue(panelNode, "tabView"));
                    panel.Type = FanLayoutPanelTypeEnumHelper.Parse(XmlLoader.ReadAttrValue(panelNode, "type"));
                    panel.Assembly = XmlLoader.ReadAttrValue(panelNode, "assembly");
                    panel.AssemblyType = XmlLoader.ReadAttrValue(panelNode, "assembly-type");

                    //子Panel
                    ParseNodeBodyChildPanel(panelNode, panel.ChildPanelItems);

                    bodyDock.Panels.Add(panel);

                    //读取uiView
                    //ParseNodeUiView(panelNode, ref panel.UiViewControl);
                }
            }
        }
        /// <summary>
        /// 解析子panel
        /// </summary>
        /// <param name="node"></param>
        /// <param name="childPanelItems"></param>
        private void ParseNodeBodyChildPanel(XmlNode node, Collection<FanLayoutPanel> childPanelItems)
        {
            XmlNodeList childPanelNodeList = XmlLoader.GetChildNodesByName(node, "subPanel");
            if (childPanelNodeList == null || childPanelNodeList.Count==0) return;
            foreach (XmlNode childPanelNode in childPanelNodeList)
            {
                FanLayoutPanel panel = new FanLayoutPanel();
                panel.Guid = XmlLoader.ReadAttrValue(childPanelNode, "guid");
                panel.Title = XmlLoader.ReadAttrValue(childPanelNode, "title");
                panel.Name = XmlLoader.ReadAttrValue(childPanelNode, "name");
                panel.Width = XmlLoader.ReadAttrIntValue(childPanelNode, "width", 200);
                panel.Height = XmlLoader.ReadAttrIntValue(childPanelNode, "height", 300);
                panel.DockPosistion = FanLayoutDockPositionEnumHelper.Parse(XmlLoader.ReadAttrValue(childPanelNode, "position"));
                panel.TabView = ("true" == XmlLoader.ReadAttrValue(childPanelNode, "tabView"));
                panel.Type = FanLayoutPanelTypeEnumHelper.Parse(XmlLoader.ReadAttrValue(childPanelNode, "type"));
                panel.Assembly = XmlLoader.ReadAttrValue(childPanelNode, "assembly");
                panel.AssemblyType = XmlLoader.ReadAttrValue(childPanelNode, "assembly-type");

                childPanelItems.Add(panel);

                //读取uiView
                //ParseNodeUiView(childPanelNode, ref panel.UiViewControl);
            }
        }

        //private void ParseNodeUiView(XmlNode node, ref FanLayoutUiView uiView)
        //{
        //    XmlNode uiViewNode = XmlLoader.GetFirstChildNodeByName(node, "uiView");
        //    if (uiViewNode != null)
        //    {
        //        uiView = new FanLayoutUiView();
        //        uiView.Title = XmlLoader.ReadAttrValue(uiViewNode, "title");
        //        uiView.Name = XmlLoader.ReadAttrValue(uiViewNode, "name");
        //        uiView.Type = FanLayoutUiViewTypeEnumHelper.Parse(XmlLoader.ReadAttrValue(uiViewNode, "type"));
        //        uiView.Assembly = XmlLoader.ReadAttrValue(uiViewNode, "assembly");
        //        uiView.AssemblyType = XmlLoader.ReadAttrValue(uiViewNode, "assembly-type");
        //    }
        //    else
        //    {
        //        uiView = null;
        //    }
        //}

        private void ParseNodeBodyDocumentContainer(XmlNode node, FanLayoutBody layoutBody)
        {
            XmlNode documentContainerNode = XmlLoader.GetFirstChildNodeByName(node, "documentContainer");
            if (documentContainerNode == null) return;

            string title = XmlLoader.ReadAttrValue(documentContainerNode, "title");
            string name = XmlLoader.ReadAttrValue(documentContainerNode, "name");
            if (layoutBody is FanLayoutDock)
            {
                FanLayoutDock bodyDock = (FanLayoutDock)layoutBody;
                bodyDock.DocumentContainer = new FanLayoutDocumentContainer();
                bodyDock.DocumentContainer.Title = title;
                bodyDock.DocumentContainer.Name = name;

                ParseNodeBodyTabView(documentContainerNode, bodyDock.DocumentContainer.TabViews);
            }
        }

        private void ParseNodeBodyTabView(XmlNode node, Collection<FanLayoutTabView> tabViews)
        {
            XmlNodeList tabViewNodeList = XmlLoader.GetChildNodesByName(node, "tabView");
            if (tabViewNodeList == null || tabViewNodeList.Count == 0) return;

            foreach (XmlNode tabViewNode in tabViewNodeList)
            {
                string title = XmlLoader.ReadAttrValue(tabViewNode, "title");
                string name = XmlLoader.ReadAttrValue(tabViewNode, "name");
                string index = XmlLoader.ReadAttrValue(tabViewNode, "index");
                FanLayoutTabView tabView = new FanLayoutTabView();
                tabView.Title = title;
                tabView.Name = name;
                
                int indexInt = 0;
                if(int.TryParse(index, out indexInt)) tabView.Index = indexInt;
            }
        }
        #endregion

        
    }
}
