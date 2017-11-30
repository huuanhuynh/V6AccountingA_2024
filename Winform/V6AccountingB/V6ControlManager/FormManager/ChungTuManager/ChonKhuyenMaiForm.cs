using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class ChonKhuyenMaiForm : V6Form
    {
        public ChonKhuyenMaiForm()
        {
            InitializeComponent();
        }

        private DataSet _ds;
        private DataView vctkm1, vctck1, vctkm1th, vctck1th;
        public ChonKhuyenMaiForm(DataSet ds)
        {
            _ds = ds;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            vctkm1 = new DataView(_ds.Tables[0]);
            vctck1 = new DataView(_ds.Tables[1]);
            vctkm1th = new DataView(_ds.Tables[2]);
            vctck1th = new DataView(_ds.Tables[3]);

            dgvctkm.DataSource = vctkm1th;
            dgvctkmct.DataSource = vctkm1;
            dgvctck.DataSource = vctck1th;
            dgvctckct.DataSource = vctck1;

        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.DoHotKey0(keyData);
        }

        private void XuLy()
        {
            try
            {
                XuLyKhuyenMai();
                XuLyChietKhau();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLy", ex);
            }
        }

        private void XuLyKhuyenMai()
        {
            try
            {
                List<string> key_list = new List<string>();
                foreach (DataRow row in vctkm1th.Table.Rows)
                {
                    var tag = (row["tag"] ?? "").ToString().Trim();
                    if (tag == "") continue;
                    if (tag.ToUpper() == "X")
                    {
                        key_list.Add(row["MA_KM"].ToString().Trim());
                    }
                }

                foreach (DataRow row in vctkm1.Table.Rows)
                {
                    row["Tag"] = "";
                    string ma_km = row["MA_KM"].ToString().Trim();
                    foreach (string key in key_list)
                    {
                        if (ma_km == key)
                        {
                            row["Tag"] = "x";
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyKhuyenMai", ex);
            }
        }

        private void XuLyChietKhau()
        {
            try
            {
                List<string> key_list = new List<string>();
                foreach (DataRow row in vctck1th.Table.Rows)
                {
                    var tag = (row["tag"] ?? "").ToString().Trim();
                    if (tag == "") continue;
                    if (tag.ToUpper() == "X")
                    {
                        key_list.Add(row["MA_KM"].ToString().Trim());
                    }
                }

                foreach (DataRow row in vctck1.Table.Rows)
                {
                    row["Tag"] = "";
                    string ma_km = row["MA_KM"].ToString().Trim();
                    foreach (string key in key_list)
                    {
                        if (ma_km == key)
                        {
                            row["Tag"] = "x";
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyChietKhau", ex);
            }
        }

        private void ChonKhuyenMaiForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            XuLy();
        }

        private void dgvctkm_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvctkm.CurrentRow != null)
                vctkm1.RowFilter = string.Format("MA_KM='{0}'", dgvctkm.CurrentRow.Cells["MA_KM"].Value);
            else vctkm1.RowFilter = "1=0";
        }
        
        private void dgvctck_SelectionChanged(object sender, EventArgs eventArgs)
        {
            if (dgvctck.CurrentRow != null)
                vctck1.RowFilter = string.Format("MA_KM='{0}'", dgvctck.CurrentRow.Cells["MA_KM"].Value);
            else vctck1.RowFilter = "1=0";
        }
    }
}
