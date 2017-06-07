using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    public class AllChangeCodeForm : ChangeCodeFormBase
    {
        public AllChangeCodeForm(SortedDictionary<string, object> data, string codedm)
            : base(data)
        {

            string sql = "SELECT MA_DM,[VALUE],[F_NAME],[TABLE_NAME] FROM " + V6TableName.Aldm
               + "  Where ma_dm = @ma_dm";
            var listParameters = new SqlParameter("@ma_dm", codedm);
            tblaldm = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters)
                .Tables[0].Rows[0];

            codedm1 = codedm;
           

            InitializeComponent();
            
            AccessibleNameOldCode = tblaldm["VALUE"].ToString().Trim();
            AccessibleNameOldCodeName = tblaldm["F_NAME"].ToString().Trim();
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            ClientSize = new System.Drawing.Size(716, 123);
            Name = "AllChangeCodeForm";
            //Text = (V6Setting.IsVietnamese ? "Đổi mã " : "Change code: ");
            //Gọi lại hàm gán data.
            MyInit();
        }

        private static DataRow tblaldm;
        private static string codedm1;

        protected override void btnNhan_Click(object sender, EventArgs e)
        {
            if (_do_change_code_running)
            {
                this.ShowMessage(V6Text.Busy);
                return;
            }

            if (tblaldm["TABLE_NAME"].ToString().Trim() !="" &&  tblaldm["VALUE"].ToString().Trim() !="")
            {
                Nhan(tblaldm["TABLE_NAME"].ToString().Trim(), tblaldm["VALUE"].ToString().Trim());
            }

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AllChangeCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(716, 143);
            this.Name = "AllChangeCodeForm";
            this.ResumeLayout(false);

        }

        protected override void DoChangeCodeThread()
        {
            try
            {
                if (codedm1 != "" && _oldCode != "" && _newCode != "" && _oldCode != _newCode)
                {
                    V6BusinessHelper.ChangeAll_Id(codedm1, _oldCode, _newCode);
                }
            }
            catch (Exception ex)
            {
                _do_change_code_error = true;
                _message = ex.Message;
                this.WriteExLog(GetType() + ".DoChangeCodeThread", ex);
            }
            _do_change_code_finish = true;

        }

    }
}
