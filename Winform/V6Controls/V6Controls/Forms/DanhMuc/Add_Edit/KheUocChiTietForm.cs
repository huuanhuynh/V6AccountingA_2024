using System;
using System.Collections.Generic;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class KheUocChiTietForm : AddEditControlVirtual
    {
        public KheUocChiTietForm()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, EventArgs e)
        {
            
        }

        public override void DoBeforeEdit()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("KU DisableWhenEdit " + ex.Message);
            }
        }

        public override SortedDictionary<string, object> GetData()
        {
            var data = base.GetData();
            data["UID_CT"] = ParentData["UID"];
            return data;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaKu.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            
            if (Mode == V6Mode.Edit)
            {
                
            }
            else if (Mode == V6Mode.Add)
            {
                
            }

            if(errors.Length>0) throw new Exception(errors);
        }

    }
}
