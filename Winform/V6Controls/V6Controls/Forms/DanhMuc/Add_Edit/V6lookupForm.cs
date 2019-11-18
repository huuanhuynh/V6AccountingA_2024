using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6lookupForm : AddEditControlVirtual
    {
        public V6lookupForm()
        {
            InitializeComponent();
            MyInit1();
        }

        private void MyInit1()
        {
            try
            {
                LoadColorNameList();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("AlbcAddEdit Init" + ex.Message, Application.ProductName);
            }
        }

        private void LoadColorNameList()
        {
            //List<string> theList = Enum.GetValues(typeof(KnownColor)).Cast<string>().ToList();
            List<string> colorList = new List<string>();
            foreach (object value in Enum.GetValues(typeof(KnownColor)))
            {
                colorList.Add(value.ToString());
            }
            cboColorList.DataSource = colorList;
            cboColorList.SelectedIndex = -1;
        }

        private void From_Load(object sender, System.EventArgs e)
        {
           // txtval.Focus();
        }
        public override void DoBeforeEdit()
        {
            if (Mode == V6Mode.Edit)
            {
               
            }

        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtVvar.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblVvar.Text;
            if (txtVma_file.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblVma_file.Text;
           
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "VVAR",
                 txtVvar.Text.Trim(), DataOld["VVAR"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblVvar.Text + "=" + txtVvar.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "VVAR",
                 txtVvar.Text.Trim(), txtVvar.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblVvar.Text + "=" + txtVvar.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        public override void V6F3Execute()
        {
            base.V6F3Execute();
        }

        private void cboColorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboColorList.SelectedIndex == -1)
            {
                lblTenMau.BackColor = Color.Transparent;
            }
            else
            {
                lblTenMau.BackColor = Color.FromName(cboColorList.Text);
            }
        }

    }
}
