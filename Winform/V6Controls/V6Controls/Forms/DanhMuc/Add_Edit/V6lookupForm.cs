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
            if (TxtFcolumn.Text.Trim() == "")
                errors +=V6Text.CheckInfor+ "\r\n";
            if (TxtVMA_FILE.Text.Trim() == "")
                errors += V6Text.CheckInfor + "!\r\n";
           
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "VVAR",
                 TxtFcolumn.Text.Trim(), DataOld["VVAR"].ToString());
                if (!b)
                    throw new Exception(V6Text.EditDenied
                                                    + "VVAR = " + TxtFcolumn.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "VVAR",
                 TxtFcolumn.Text.Trim(), TxtFcolumn.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.AddDenied
                                                    + "VVAR = " + TxtFcolumn.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
        public override void V6F3Execute()
        {
            
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
