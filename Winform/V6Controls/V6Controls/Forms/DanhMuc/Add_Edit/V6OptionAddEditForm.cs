using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using V6Init;
using V6Tools.V6Convert;

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
                string[] param_font = ObjectAndString.SplitString(txtval.Text);
                string font_name = param_font[0];	
                float font_size =	Font.Size;
                FontStyle font_style = 0;
                if (param_font.Length > 1) font_size = Single.Parse(param_font[1]);
                if (param_font.Length > 2) font_style = (FontStyle)Int32.Parse(param_font[2]);
                Font m_rfontname = new Font(font_name, font_size, font_style);

                FontDialog f = new FontDialog();
                f.Font = m_rfontname;

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    txtval.Text = f.Font.Name + ";" + f.Font.Size + ";" + (int)f.Font.Style;
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
                
                var result = Categories.Update(_MA_DM, DataDic, _keys);
                var name = DataDic["NAME"].ToString();
                var value = DataDic["VAL"].ToString();
                V6Options.SetValue(name, value);

                return result;
            }

            return 0;
        }
    }
}
