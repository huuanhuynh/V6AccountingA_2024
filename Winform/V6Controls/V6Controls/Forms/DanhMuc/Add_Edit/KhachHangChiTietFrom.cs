using System;
using System.Collections.Generic;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class KhachHangChiTietFrom : AddEditControlVirtual
    {
        public KhachHangChiTietFrom()
        {
            InitializeComponent();
            if (Mode == V6Mode.Add)
            {
                if (V6Login.MadvcsCount == 1)
                {
                    TxtMa_dvcs.Text = V6Login.Madvcs;
                }
            }
            else if (Mode == V6Mode.Edit)
            {
                
            }
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
                Logger.WriteToLog("KH DisableWhenEdit " + ex.Message);
            }
        }

        public override SortedDictionary<string, object> GetData()
        {
            var data = base.GetData();
            data["UID_KH"] = ParentData["UID"];
            return data;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaKH.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaKH.Text;
            if (V6Login.MadvcsTotal > 0 && TxtMa_dvcs.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblDVCS.Text;

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
