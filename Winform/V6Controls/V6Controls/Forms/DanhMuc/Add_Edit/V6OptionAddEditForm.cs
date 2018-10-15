using System.Collections.Generic;
using System.Windows.Forms;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6OptionAddEditForm : AddEditControlVirtual
    {
        public V6OptionAddEditForm()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, System.EventArgs e)
        {
            txtval.Focus();
        }
         public override void DoBeforeEdit()
        {
           
        }

        public override void ValidateData()
        {
           
        }

        private void btnFont_Click(object sender, System.EventArgs e)
        {
            if (new List<string>(){"M_RFONTNAME", "M_RSFONT", "M_RTFONT" }
                .Contains(txtName.Text.Trim().ToUpper()))
            {
                FontDialog f = new FontDialog();
                //f.AllowScriptChange = false;
                //f.AllowSimulations = false;
                //f.AllowVectorFonts = false;
                //f.AllowVerticalFonts = false;

                //f.ShowColor = false;

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    txtval.Text = f.Font.Name;
                }
            }
        }

        public override int UpdateData()
        {
            if (V6Login.GetDataMode == GetDataMode.Local)
            {
                ValidateData();
                DataDic = GetData();

                //Lấy thêm UID từ DataEditNếu có.
                if (DataOld.ContainsKey("UID"))
                {
                    _keys["UID"] = DataOld["UID"];
                }
                
                var result = Categories.Update(TableName, DataDic, _keys);
                var name = DataDic["NAME"].ToString();
                var value = DataDic["VAL"].ToString();
                V6Options.SetValue(name, value);

                return result;
            }

            return 0;
        }
    }
}
