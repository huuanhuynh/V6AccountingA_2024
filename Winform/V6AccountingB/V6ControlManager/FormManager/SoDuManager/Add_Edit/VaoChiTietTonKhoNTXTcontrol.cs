using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Controls;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class VaoChiTietTonKhoNTXTcontrol : SoDuAddEditControlVirtual
    {
        public VaoChiTietTonKhoNTXTcontrol()
        {
            InitializeComponent();
            try
            {
                TxtMaVatTu.SetInitFilter("gia_ton=3");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        public override void DoBeforeAdd()
        {
            TxtNam.Value = V6Setting.M_Nam_bd;
            dateNgaySd.Value = V6Setting.M_Ngay_ky1;
        }

        /// <summary>
        /// Xử lý khi load form trường hợp sửa
        /// </summary>
        public override void DoBeforeEdit()
        {
                    

        }

        public override void ValidateData()
        {
            if ((txtSoCt.Text.Trim() == "") || (txtMaKho.Text.Trim() == "")
                || (TxtMaVatTu.Text.Trim() == "") || (txtMaKho.Text.Trim() == ""))
            {
                throw new Exception("Chưa nhập đủ thông tin!");
            }
            
            {
                // Check data 
                if (Mode == V6Mode.Edit)
                {
                    

                }
                else if (Mode == V6Mode.Add)
                {
                    
                }
            }
        }

        public override SortedDictionary<string, object> GetData()
        {
            var data = base.GetData();
            data["MA_CT"] = "S08";
            data["PN_CO_GIA"] = 1;
            data["NGAY"] = dateNgayCt.Value.Day;
            var ton_kho = txtton00.Value;
            var so_du = txtdu00.Value;
            var so_du_nt = txtdu_nt00.Value;
            for (int i = 1; i <= 13; i++)
            {
                var textNum = ("00" + i).Right(2);

                data["TON_KHO" + textNum] = ton_kho;
                data["SO_DU" + textNum] = so_du;
                data["SO_DU_NT" + textNum] = so_du_nt;
            }
            if (Mode == V6Mode.Add)
            {
                data["STT_REC_NT"] = V6BusinessHelper.GetNewLikeSttRec("S08", "STT_REC_NT", "M");
            }
            
            return data;
        }

        public override bool InsertNew()
        {
            var result = base.InsertNew();
            if (result)
            {
                try
                {
                    var plist = new SqlParameter("@nYear", TxtNam.Value);
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_ConvertItemBalanceFromFIFOBalance", plist);
                }
                catch (Exception ex)
                {
                    this.ShowWarningMessage(ex.Message);
                }
            }
            return result;
        }

        public override int UpdateData()
        {
            var result = base.UpdateData();
            if (result > 0)
            {
                try
                {
                    var plist = new SqlParameter("@nYear", TxtNam.Value);
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_ConvertItemBalanceFromFIFOBalance", plist);
                }
                catch (Exception ex)
                {
                    this.ShowWarningMessage(ex.Message);
                }
            }
            return result;
        }

    }
}
