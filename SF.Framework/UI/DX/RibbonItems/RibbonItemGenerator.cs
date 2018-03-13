using DevExpress.XtraBars;
using SF.Framework.Layout.Header.Ribbon;
using SF.Infrastructure.SystemParams;
using SF.Utility.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace SF.Framework.UI.DX.RibbonItems
{
    /// <summary>
    /// Ribbon控件项生成器
    /// by changtan.sun
    /// </summary>
    public class RibbonItemGenerator
    {

        public RibbonItemGenerator()
        {
        }

        /// <summary>
        /// 生成Ribbon控件项
        /// </summary>
        /// <param name="layoutItem"></param>
        /// <returns></returns>
        public BarItem GenerateItem(FanLayoutRibbonItem layoutItem)
        {
            BarItem barItem = null;
            string itemType = layoutItem.Type;
            if (itemType == "button") //按钮
            {
                barItem = GenerateBarButton(layoutItem);
            }
            else if (itemType == "checkbox") //复选框
            {
                barItem = GenerateBarCheckbox(layoutItem);
            }
            else if (itemType == "radio") //单选
            {
                barItem = GenerateBarRadio(layoutItem);
            }
            else if (itemType == "toggleSwitch") //开关按钮
            {
                barItem = GenerateBarToggleSwitch(layoutItem);
            }
            else if (itemType == "skin") //皮肤控件
            {
                barItem = GenerateBarSkinRibbonGallery(layoutItem);
            }

            return barItem;
        }

        /// <summary>
        /// 生成按钮
        /// </summary>
        /// <param name="layoutItem"></param>
        /// <param name="button"></param>
        private BarItem GenerateBarButton(FanLayoutRibbonItem layoutItem)
        {
            FanBarButtonItem button = new FanBarButtonItem();
            button.Caption = layoutItem.Title;
            AddImageToBarItem(button, layoutItem);
            button.Name = layoutItem.Name;
            button.Enabled = layoutItem.Enabled;
            if (!string.IsNullOrWhiteSpace(layoutItem.Tip))
            {
                DevExpress.Utils.SuperToolTip toolTip = new DevExpress.Utils.SuperToolTip();
                DevExpress.Utils.ToolTipItem toolTipItem = new DevExpress.Utils.ToolTipItem();
                toolTipItem.Text = layoutItem.Tip;
                toolTip.Items.Add(toolTipItem);
                button.SuperTip = toolTip;
            }
            if (!string.IsNullOrEmpty(layoutItem.Event_OnClick))
            {
                button.ItemClickMethodName = layoutItem.Event_OnClick;
                button.ItemClick += button.button_ItemClick;
            }

            //绑定事件
            //BarItemBindEvent(barItem, button);

            return button;
        }

        /// <summary>
        /// 生成复选框
        /// </summary>
        /// <param name="barItem"></param>
        /// <param name="checkbox"></param>
        private BarItem GenerateBarCheckbox(FanLayoutRibbonItem layoutItem)
        {
            FanBarCheckItem checkbox = new FanBarCheckItem();
            checkbox.Caption = layoutItem.Title;
            AddImageToBarItem(checkbox, layoutItem);
            checkbox.Name = layoutItem.Name;
            checkbox.CheckStyle = BarCheckStyles.Standard;
            checkbox.CheckBoxVisibility = CheckBoxVisibility.BeforeText;
            checkbox.Checked = layoutItem.Checked;
            checkbox.Enabled = layoutItem.Enabled;
            if (!string.IsNullOrWhiteSpace(layoutItem.Tip))
            {
                DevExpress.Utils.SuperToolTip toolTip = new DevExpress.Utils.SuperToolTip();
                DevExpress.Utils.ToolTipItem toolTipItem = new DevExpress.Utils.ToolTipItem();
                toolTipItem.Text = layoutItem.Tip;
                toolTip.Items.Add(toolTipItem);
                checkbox.SuperTip = toolTip;
            }
            if (!string.IsNullOrEmpty(layoutItem.Event_OnChange))
            {
                checkbox.ChangeMethodName = layoutItem.Event_OnChange;
                checkbox.CheckedChanged += checkbox.EventDefinedBarCheckItem_CheckedChanged;
            }
            //绑定事件
            //BarItemBindEvent(barItem, checkbox);

            return checkbox;
        }

        /// <summary>
        /// 生成单选框
        /// </summary>
        /// <param name="barItem"></param>
        /// <param name="radio"></param>
        private BarItem GenerateBarRadio(FanLayoutRibbonItem layoutItem)
        {
            FanBarCheckItem radio = new FanBarCheckItem();
            radio.Caption = layoutItem.Title;
            AddImageToBarItem(radio, layoutItem);
            radio.Name = layoutItem.Name;
            radio.CheckStyle = BarCheckStyles.Radio;
            radio.CheckBoxVisibility = CheckBoxVisibility.BeforeText;
            radio.Checked = layoutItem.Checked;
            radio.Enabled = layoutItem.Enabled;
            if (!string.IsNullOrWhiteSpace(layoutItem.Tip))
            {
                DevExpress.Utils.SuperToolTip toolTip = new DevExpress.Utils.SuperToolTip();
                DevExpress.Utils.ToolTipItem toolTipItem = new DevExpress.Utils.ToolTipItem();
                toolTipItem.Text = layoutItem.Tip;
                toolTip.Items.Add(toolTipItem);
                radio.SuperTip = toolTip;
            }
            if (!string.IsNullOrEmpty(layoutItem.Event_OnChange))
            {
                radio.ChangeMethodName = layoutItem.Event_OnChange;
                radio.CheckedChanged += radio.EventDefinedBarCheckItem_CheckedChanged;
            }
            //绑定事件
            //BarItemBindEvent(barItem, radio);
            return radio;
        }

        /// <summary>
        /// 生成开关按钮
        /// </summary>
        /// <param name="barItem"></param>
        /// <param name="toggleSwitch"></param>
        private BarItem GenerateBarToggleSwitch(FanLayoutRibbonItem layoutItem)
        {
            FanBarToggleSwitchItem toggleSwitch = new FanBarToggleSwitchItem();
            toggleSwitch.Caption = layoutItem.Title;
            AddImageToBarItem(toggleSwitch, layoutItem);
            toggleSwitch.Name = layoutItem.Name;
            toggleSwitch.Checked = layoutItem.Checked;
            toggleSwitch.Enabled = layoutItem.Enabled;
            if (!string.IsNullOrWhiteSpace(layoutItem.Tip))
            {
                DevExpress.Utils.SuperToolTip toolTip = new DevExpress.Utils.SuperToolTip();
                DevExpress.Utils.ToolTipItem toolTipItem = new DevExpress.Utils.ToolTipItem();
                toolTipItem.Text = layoutItem.Tip;
                toolTip.Items.Add(toolTipItem);
                toggleSwitch.SuperTip = toolTip;
            }
            if (!string.IsNullOrEmpty(layoutItem.Event_OnChange))
            {
                toggleSwitch.ChangeMethodName = layoutItem.Event_OnChange;
                toggleSwitch.CheckedChanged += toggleSwitch.EventDefinedBarToggleSwitchItem_CheckedChanged;
            }
            //绑定事件
            //BarItemBindEvent(layoutItem, toggleSwitch);
            return toggleSwitch;
        }

        /// <summary>
        /// 生成皮肤控件
        /// </summary>
        /// <param name="layoutItem"></param>
        /// <returns></returns>
        private BarItem GenerateBarSkinRibbonGallery(FanLayoutRibbonItem layoutItem)
        {
            FanSkinRibbonGalleryBarItem skinGallery = new FanSkinRibbonGalleryBarItem();
            skinGallery.Caption = layoutItem.Title;
            skinGallery.Name = layoutItem.Name;
            skinGallery.Id = new Random().Next(100);
            skinGallery.Enabled = layoutItem.Enabled;
            if (!string.IsNullOrWhiteSpace(layoutItem.Tip))
            {
                DevExpress.Utils.SuperToolTip toolTip = new DevExpress.Utils.SuperToolTip();
                DevExpress.Utils.ToolTipItem toolTipItem = new DevExpress.Utils.ToolTipItem();
                toolTipItem.Text = layoutItem.Tip;
                toolTip.Items.Add(toolTipItem);
                skinGallery.SuperTip = toolTip;
            }
            skinGallery.GalleryItemClick += (sender, e) =>
            {
                var skinStyle = e.Item.Tag.ToString();
                GlobalParamService.SystemSettingParam.LayoutSettingParam.SkinStyle = skinStyle;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = skinStyle;
            };
            if (!string.IsNullOrEmpty(layoutItem.Event_OnClick))
            {
                skinGallery.ItemClickMethodName = layoutItem.Event_OnMouseClick;
                skinGallery.ItemClick += skinGallery.EventDefinedSkinRibbonGalleryBarItem_ItemClick;
            }
            //绑定事件
            //BarItemBindEvent(barItem, skinGallery);

            return skinGallery;
        }

        #region 帮助类
        /// <summary>
        /// 给Ribbon的控件Item增加图标
        /// </summary>
        /// <param name="item"></param>
        /// <param name="barItem"></param>
        private void AddImageToBarItem(BarItem item, FanLayoutRibbonItem barItem)
        {
            if (!string.IsNullOrWhiteSpace(barItem.Image32))
            {
                Tuple<string, int, Image> data = GlobalParamService.GetCustomizeImageData(barItem.Image32, 32);
                //item.LargeImageIndex = data.Item2;
                item.LargeGlyph = data.Item3;
                item.Glyph = data.Item3;
            }
            else if (!string.IsNullOrWhiteSpace(barItem.Image16))
            {
                Tuple<string, int, Image> data = GlobalParamService.GetCustomizeImageData(barItem.Image16, 16);
                item.ImageIndex = data.Item2;
            }
            else if (!string.IsNullOrWhiteSpace(barItem.ImageIndex))
            {
                item.ImageUri.Uri = barItem.ImageIndex;
            }
        }

        /// <summary>
        /// Ribbon控件绑定事件
        /// </summary>
        /// <param name="barItem"></param>
        /// <param name="component"></param>
        private void BarItemBindEvent(FanLayoutRibbonItem barItem, Component component)
        {
            if (!string.IsNullOrWhiteSpace(barItem.Assembly))
            {
                string[] temp = barItem.Assembly.Split(',');
                if (temp.Length >= 2)
                {
                    string assembleName = temp[1] + "." + barItem.AssemblyType;
                    string typeName = temp[0];
                    object obj = AssemblyHelper.GetInstanceFromAssembly(assembleName, typeName);
                    if (obj == null) return;
                    if (!string.IsNullOrWhiteSpace(barItem.Event_OnClick))//click事件
                    {
                        MethodInfo eventMethod = AssemblyHelper.GetMethodInfo(obj, barItem.Event_OnClick);
                        if (eventMethod != null)
                        {
                            AssemblyHelper.RegisterControlEvent(component, "ItemClick", obj, eventMethod);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(barItem.Event_OnMouseClick))//mouseClick事件
                    {
                        MethodInfo eventMethod = AssemblyHelper.GetMethodInfo(obj, barItem.Event_OnMouseClick);
                        if (eventMethod != null)
                        {
                            AssemblyHelper.RegisterControlEvent(component, "MouseClick", obj, eventMethod);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(barItem.Event_OnChange))//mouseClick事件
                    {
                        MethodInfo eventMethod = AssemblyHelper.GetMethodInfo(obj, barItem.Event_OnChange);
                        if (eventMethod != null)
                        {
                            AssemblyHelper.RegisterControlEvent(component, "CheckedChanged", obj, eventMethod);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
