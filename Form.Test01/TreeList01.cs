using System;
using System.Data;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SF.Infrastructure;

namespace Form.Test01
{
    public partial class TreeList01 : FanUserControl
    {
        public TreeList01()
        {
            InitializeComponent();

            this.treeList1.TreeLineStyle = LineStyle.Wide;
            this.treeList1.OptionsView.ShowVertLines = true;
           // this.AddControlHook(this.treeList1);

        }

        private void treeList1_Load(object sender, EventArgs e)
        {

            LoadTree();
        }

        private void LoadTree()
        {
            //获取数据源 
            DataTable dt = MakeData();

            //设置字段 
            treeList1.KeyFieldName = "GroupCode";
            treeList1.ParentFieldName = "ParentGroupCode";
            treeList1.DataSource = dt;

            //递归设置图标 
            SetImageIndex(treeList1, null, 1, 0);

            treeList1.ExpandAll();
        }

        private DataTable MakeData()
        {
            DataTable dt = new DataTable("aa");
            dt.Columns.Add("GroupCode", Type.GetType("System.String"));
            dt.Columns.Add("ParentGroupCode", Type.GetType("System.String"));
            dt.Columns.Add("部门", Type.GetType("System.String"));

            DataRow newRow = dt.NewRow();
            newRow["GroupCode"] = "10";
            newRow["ParentGroupCode"] = "1";
            newRow["部门"] = "研发部";
            dt.Rows.Add(newRow);

            newRow = dt.NewRow();
            newRow["GroupCode"] = "11";
            newRow["ParentGroupCode"] = "1";
            newRow["部门"] = "测试部";
            dt.Rows.Add(newRow);

            newRow = dt.NewRow();
            newRow["GroupCode"] = "1101";
            newRow["ParentGroupCode"] = "11";
            newRow["部门"] = "测试一部";
            dt.Rows.Add(newRow);
            newRow = dt.NewRow();
            newRow["GroupCode"] = "1102";
            newRow["ParentGroupCode"] = "11";
            newRow["部门"] = "测试二部";
            dt.Rows.Add(newRow);
            newRow = dt.NewRow();
            newRow["GroupCode"] = "1103";
            newRow["ParentGroupCode"] = "11";
            newRow["部门"] = "测试三部";
            dt.Rows.Add(newRow);
            newRow = dt.NewRow();
            newRow["GroupCode"] = "1104";
            newRow["ParentGroupCode"] = "11";
            newRow["部门"] = "测试四部";
            dt.Rows.Add(newRow);

            newRow = dt.NewRow();
            newRow["GroupCode"] = "105";
            newRow["ParentGroupCode"] = "10";
            newRow["部门"] = "研发五部";
            dt.Rows.Add(newRow);

            newRow = dt.NewRow();
            newRow["GroupCode"] = "102";
            newRow["ParentGroupCode"] = "10";
            newRow["部门"] = "研发二部";
            dt.Rows.Add(newRow);
            return dt;
        }

        public void SetImageIndex(TreeList tl, TreeListNode node, int nodeIndex, int parentIndex)
        {
            if (node == null)
            {
                foreach (TreeListNode N in tl.Nodes)
                    SetImageIndex(tl, N, nodeIndex, parentIndex);
            }
            else
            {
                if (node.HasChildren || node.ParentNode == null)
                {
                    //node.SelectImageIndex = parentIndex; 
                    node.StateImageIndex = parentIndex;
                    node.ImageIndex = parentIndex;
                }
                else
                {
                    //node.SelectImageIndex = nodeIndex; 
                    node.StateImageIndex = nodeIndex;
                    node.ImageIndex = nodeIndex;
                }

                foreach (TreeListNode N in node.Nodes)
                {
                    SetImageIndex(tl, N, nodeIndex, parentIndex);
                }
            }
        }

        public override void InvokeCallback(string eventName, object data)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            
            treeList1.Dispose();
            treeList1 = null;

            base.Close();
        }
    }
}
