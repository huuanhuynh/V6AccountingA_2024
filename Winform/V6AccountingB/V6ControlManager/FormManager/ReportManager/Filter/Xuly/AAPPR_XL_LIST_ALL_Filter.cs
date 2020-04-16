using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AAPPR_XL_LIST_ALL_Filter : FilterBase
    {
        public AAPPR_XL_LIST_ALL_Filter()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                F3 = true;
                F4 = true;
                F5 = false;
                F9 = true;
                txtMaDMProc.SetInitFilter("APPR_YN='1'");
                txtMaDMProc.Upper();
                
                SqlParameter[] plist =
                {
                    new SqlParameter("@Ma_dm", ""),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@Advance", "APPR_YN='1'"),
                };
                var data = V6BusinessHelper.ExecuteProcedure("VPA_GET_ALCTCT_DM_DEFAULT", plist).Tables[0];
                if (data.Rows.Count > 0)
                {
                    txtMaDMProc.Text = data.Rows[0]["MA_DM"].ToString().Trim();
                }
                else
                {
                    txtMaDMProc.Text = "";
                }

                LoadComboboxSource(txtMaDMProc.Text);

                Ready();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void AAPPR_XL_LIST_ALL_Filter_Load(object sender, EventArgs e)
        {
            txtMa_dm_V6LostFocus(null);
        }

        private void LoadComboboxSource(string maCt)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@Ma_dm", txtMaDMProc.Text),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@Advance", ""),
                };
                var data = V6BusinessHelper.ExecuteProcedure("VPA_GET_ALXULY_DM", plist).Tables[0];

                cboMa_xuly.ValueMember = "MA_XULY2";
                cboMa_xuly.DisplayMember = V6Setting.IsVietnamese ? "Ten_xuly2" : "Ten_xuly22";
                cboMa_xuly.DataSource = data;
                cboMa_xuly.ValueMember = "MA_XULY2";
                cboMa_xuly.DisplayMember = V6Setting.IsVietnamese ? "Ten_xuly2" : "Ten_xuly22";

                var viewXuly = new DataView(data);
                viewXuly.RowFilter = "Ma_dm='"+ maCt+ "' and Status='1' And SL_TD2=1";
                if (viewXuly.Count == 1)
                {
                    string selectValue = viewXuly.ToTable().Rows[0]["MA_XULY2"].ToString().Trim();
                    if (!string.IsNullOrEmpty(selectValue)) cboMa_xuly.SelectedValue = selectValue;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadComboboxSource", ex);
            }
        }

        public override string Kieu_post
        {
            get
            {
                return cboMa_xuly.SelectedValue.ToString().Trim();
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            String1 = txtMaDMProc.Text.Trim();
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@ma_dm", txtMaDMProc.Text.Trim()));
            result.Add(new SqlParameter("@user_id", V6Login.UserId));

            var and = radAnd.Checked;
            
            var cKey = "";
            

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_XULY2"
            }, and);
            
            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey = string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "1=1";
            }

            switch (TxtXtag.Text.Trim())
            {
                case "0":
                    cKey = cKey + " and ( Status1=' ' or Status1 IS NULL) and ( Status2=' ' or Status2 IS NULL)";
                    break;
                case "1":
                    cKey = cKey + " and ( Status1='1')";
                    break;
                case "2":
                    cKey = cKey + " and ( Status2='1')";
                    break;
            }
            
            result.Add(new SqlParameter("@advance", cKey));
          
            return result;
        }
        
        public override void LoadDataFinish(DataSet ds)
        {
            base.LoadDataFinish(ds);
            GenButtons(ds.Tables[1]);
            chkView_all.Checked = true;
        }

        private XuLyBase _xulyBase;
        private V6ColorDataGridView _gridView1;
        private V6ColorDataGridView _gridView2;
        private List<Button> bs = new List<Button>(); 
        private void GenButtons(DataTable dataTable)
        {
            try
            {
                RemoveOldButtons();
                _xulyBase = FindParent<XuLyBase>() as XuLyBase;
                if (_xulyBase != null)
                {
                    _gridView1 = _xulyBase.dataGridView1;
                    _gridView2 = _xulyBase.dataGridView2;
                }

                int x_start = groupBox1.Left;
                int y_start = groupBox1.Bottom + 10;
                foreach (DataRow row in dataTable.Rows)
                {
                    Button b = new Button();
                    bs.Add(b);
                    b.UseVisualStyleBackColor = true;
                    b.Text = string.Format("{0} ({1})", row["ten_xuly" + (V6Setting.IsVietnamese?"":"2")], ObjectAndString.ObjectToInt(row["so_luong"]));

                    var file0 = Path.Combine("Pictures\\", row["PICTURE"].ToString().Trim());
                    var file = file0 + ".png";
                    if (File.Exists(file))
                    {
                        b.Image = V6ControlFormHelper.LoadCopyImage(file);
                    }
                    else
                    {
                        file = file0 + ".jpg";
                        if (!File.Exists(file))
                        {
                            file = file0 + ".gif";
                            if (!File.Exists(file)) file = file0 + ".bmp";
                        }
                        if (File.Exists(file)) b.Image = V6ControlFormHelper.LoadCopyImage(file);
                    }
                    b.ImageAlign = ContentAlignment.BottomLeft;
                    //b.TextAlign = ContentAlignment.MiddleLeft;    
                    
                    int y = y_start;
                    y_start += 38;
                    b.Location = new Point(x_start, y);
                    b.Width = groupBox1.Width;
                    b.Height = 38;
                    Controls.Add(b);
                    Height = b.Bottom + 5;
                    b.Tag = row;
                    b.Click += b_Click;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GenButtons", ex);
            }
        }

        private void RemoveOldButtons()
        {
            while (bs.Count>0)
            {
                Controls.Remove(bs[0]);
                bs.RemoveAt(0);
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            try
            {
                chkView_all.Checked = false;
                Button b = (Button) sender;
                DataRow row = (DataRow) b.Tag;

                _gridView1.Filter("ma_xuly2", "=", row["ma_xuly2"], "value2", false, false);
                _xulyBase.FormatGridViewExtern();
                _xulyBase.UpdateGridView2(_gridView1.CurrentRow);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".b_Click", ex);
            }
        }

        private void txtMa_dm_V6LostFocus(object sender)
        {
            try
            {
                LoadComboboxSource(txtMaDMProc.Text);
                lineMa_xuly.VvarTextBox.SetInitFilter(string.Format("Ma_dm='{0}'", txtMaDMProc.Text.Trim()));
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".txtMa_dm_V6LostFocus", ex);
            }
        }

        private void chkView_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (chkView_all.Checked)
                {
                    _gridView1.Filter("ma_dm", "=",txtMaDMProc.Text.Trim(), "value2", false, false);
                    _xulyBase.FormatGridViewExtern();
                    _xulyBase.UpdateGridView2(_gridView1.CurrentRow);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".chkView_all", ex);
            }
        }
        
    }
}
