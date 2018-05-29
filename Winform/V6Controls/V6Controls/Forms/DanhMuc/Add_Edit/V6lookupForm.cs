using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6lookupForm : AddEditControlVirtual
    {
        public V6lookupForm()
        {
            InitializeComponent();
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

    }
}
