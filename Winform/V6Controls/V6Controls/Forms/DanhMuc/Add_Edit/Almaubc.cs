using System;
using System.Data.SqlClient;
using System.Reflection;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class Almaubc : AddEditControlVirtual
    {
        public Almaubc()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
            txtMaMauBc.Enabled = false;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaMauBc.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaMauBC.Text;
            if (txtFileMauBc.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblFileMauBC.Text;
            if (txtTenMauBc.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenMauBC.Text;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "FILE_MAUBC", txtFileMauBc.Text.Trim(), DataOld["FILE_MAUBC"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblFileMauBC.Text + "=" + txtFileMauBc.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "FILE_MAUBC",
                 txtFileMauBc.Text.Trim(), txtFileMauBc.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblFileMauBC.Text + "=" + txtFileMauBc.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        public override void AfterInsert()
        {
            try
            {
                if (Mode != V6Mode.Add) return;
                if (DataOld == null || DataOld.Count == 0 || !DataOld.ContainsKey("FILE_MAUBC"))
                {
                    this.ShowInfoMessage(V6Text.NoData);
                    return;
                }

                var ma_new = DataDic["FILE_MAUBC"].ToString().Trim();
                var ma_old = DataOld["FILE_MAUBC"].ToString().Trim();

                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_AFTERINSERT_ALMAUBC",
                    new SqlParameter("@cFILE_MAUBC_old", ma_old),
                    new SqlParameter("@cFILE_MAUBC_new", ma_new));
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
