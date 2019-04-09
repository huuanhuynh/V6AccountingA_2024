using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NgoaiTeAddEditForm : AddEditControlVirtual
    {
        public NgoaiTeAddEditForm()
        {
            InitializeComponent();
        }

        void DocSoTien()
        {
            lblDocSoTien.Text = V6Tools.DocSo.DOI_SO_CHU_NEW(number.Value, begin1.Text,
                end1.Text, only1.Text, point1.Text, endpoint1.Text) + "\r\n"
                + V6Tools.DocSo.NumWordsWrapper(number.Value, begin2.Text,
                end2.Text, only2.Text, point2.Text, endpoint2.Text);
        }

        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ARA00", "Ma_nt", TxtMa_nt.Text);
            TxtMa_nt.Enabled = !v;
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_nt.Text.Trim() == "" || TxtTen_nt.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_NT",
                    TxtMa_nt.Text.Trim(), DataOld["MA_NT"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.DataExist
                                        + "MA_NT = " + TxtMa_nt.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_NT",
                    TxtMa_nt.Text.Trim(), TxtMa_nt.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.DataExist
                                        + "MA_NT = " + TxtMa_nt.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void only2_TextChanged(object sender, EventArgs e)
        {
            DocSoTien();
        }
    }
}
