using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit.Albc;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter.NhanSu
{
    public partial class XLSHRPERSONAL : FilterBase
    {
        public XLSHRPERSONAL()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            F3 = false;
            F5 = false;
            F9 = true;
            txtDanhSachCot1.Text = "S11:GENDER=1,S12:GENDER=0";
            txtDanhSachCot2.Text = ReadDanhSachCot2();
            txtSoCotMaNhanSu.Value = 2;
            Ready();
        }

        private string ReadDanhSachCot2()
        {
            string file = "DanhSachCot2.xml";
            return V6Tools.V6Reader.TextFile.ToString(file) ?? "";
        }
        
        private void WriteDanhSachCot2()
        {
            try
            {
                var saveFile = "DanhSachCot2.xml";
                
                FileStream fs = new FileStream(saveFile, FileMode.Create);
                var ds = ObjectAndString.XmlStringToDataSet(txtDanhSachCot2.Text);
                ds.WriteXml(fs);
                fs.Close();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".WriteDanhSachCot2", ex);
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            return null;
        }

        public override void UpdateValues()
        {
            String1 = txtFile.Text;
            Number2 = txtDongBatDau.Value;
            Number1 = txtSoCotMaNhanSu.Value;
            ObjectDictionary["DSCOT1"] = txtDanhSachCot1.Text;
            ObjectDictionary["DSCOT2"] = txtDanhSachCot2.Text;

            Check1 = checkBox1.Checked;
            Check2 = checkBox2.Checked;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string file = V6ControlFormHelper.ChooseExcelFile(this);
                if (!string.IsNullOrEmpty(file))
                {
                    txtFile.Text = file;
                    String1 = file;
                    Number2 = txtDongBatDau.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Check1 = checkBox1.Checked;
            if (Check1)
            {
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String2 = comboBox1.SelectedItem.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            String3 = comboBox2.SelectedItem.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Check2 = checkBox2.Checked;
        }

        private void btnEditXml_Click(object sender, EventArgs e)
        {
            DoEditXml();
        }

        private void DoEditXml()
        {
            try
            {
                var file_xml = "DanhSachCot2.xml";
                new XmlEditorForm(txtDanhSachCot2, file_xml, "DanhSachCot2", "TableName,Tach3,First_name,Mid_name,Last_name,MapColumns,Data".Split(',')).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditXml", ex);
            }
        }

        private void txtDanhSachCot2_TextChanged(object sender, EventArgs e)
        {
            if(IsReady) WriteDanhSachCot2();
        }
    }
}
