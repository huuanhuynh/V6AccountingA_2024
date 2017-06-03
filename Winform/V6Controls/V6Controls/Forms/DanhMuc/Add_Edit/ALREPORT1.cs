using System;
using V6AccountingBusiness;
using V6Structs;
namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class ALREPORT1 : AddEditControlVirtual
    {
        public ALREPORT1()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, System.EventArgs e)
        {
           // txtval.Focus();
        }
        public override void DoBeforeEdit()
        {
            if (Mode == V6Mode.Edit)
            {
               
            }

        }
  
        public override void V6F3Execute()
        {
            
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTMA_BC.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            
            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}
