namespace Form.Test01
{
    partial class TreeList01
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeList01));
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeList1.ColumnsImageList = this.imageCollection1;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.BeginUnboundLoad();
            this.treeList1.AppendNode(new object[0], -1);
            this.treeList1.AppendNode(new object[0], -1);
            this.treeList1.AppendNode(new object[0], 1);
            this.treeList1.AppendNode(new object[0], 2);
            this.treeList1.AppendNode(new object[0], 3);
            this.treeList1.AppendNode(new object[0], -1);
            this.treeList1.AppendNode(new object[0], 5);
            this.treeList1.EndUnboundLoad();
            this.treeList1.OptionsView.ShowCaption = true;
            this.treeList1.OptionsView.ShowIndentAsRowStyle = true;
            this.treeList1.OptionsView.ShowPreview = true;
            this.treeList1.SelectImageList = this.imageCollection1;
            this.treeList1.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.treeList1.Size = new System.Drawing.Size(150, 150);
            this.treeList1.StateImageList = this.imageCollection1;
            this.treeList1.TabIndex = 0;
            this.treeList1.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.Solid;
            this.treeList1.Load += new System.EventHandler(this.treeList1_Load);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "ansys64.png");
            this.imageCollection1.Images.SetKeyName(1, "catia_entity64.png");
            this.imageCollection1.Images.SetKeyName(2, "catia_macro64.png");
            this.imageCollection1.Images.SetKeyName(3, "catia_script42.png");
            this.imageCollection1.Images.SetKeyName(4, "catia_script64.png");
            this.imageCollection1.Images.SetKeyName(5, "command64.png");
            this.imageCollection1.Images.SetKeyName(6, "data_parser64.png");
            this.imageCollection1.Images.SetKeyName(7, "default64.png");
            this.imageCollection1.Images.SetKeyName(8, "document64.png");
            this.imageCollection1.Images.SetKeyName(9, "excel64.png");
            this.imageCollection1.Images.SetKeyName(10, "fluent_script64.png");
            this.imageCollection1.Images.SetKeyName(11, "form64.png");
            this.imageCollection1.Images.SetKeyName(12, "formula64.png");
            this.imageCollection1.Images.SetKeyName(13, "hyperworks64.png");
            this.imageCollection1.Images.SetKeyName(14, "matlab_engine64.png");
            this.imageCollection1.Images.SetKeyName(15, "matlab_script64.png");
            this.imageCollection1.Images.SetKeyName(16, "maxsurf_script64.png");
            this.imageCollection1.Images.SetKeyName(17, "nastran64.png");
            this.imageCollection1.Images.SetKeyName(18, "oracle64.png");
            this.imageCollection1.Images.SetKeyName(19, "patran_function64.png");
            this.imageCollection1.Images.SetKeyName(20, "patran_macro64.png");
            this.imageCollection1.Images.SetKeyName(21, "proe_entity64.png");
            this.imageCollection1.Images.SetKeyName(22, "script_driver64.png");
            this.imageCollection1.Images.SetKeyName(23, "ug_entity64.png");
            this.imageCollection1.Images.SetKeyName(24, "ug_script64.png");
            this.imageCollection1.Images.SetKeyName(25, "word64.png");
            // 
            // TreeList01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeList1);
            this.Name = "TreeList01";
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
