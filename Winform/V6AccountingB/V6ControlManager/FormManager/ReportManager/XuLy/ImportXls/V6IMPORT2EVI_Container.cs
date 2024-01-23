using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class V6IMPORT2EIV_Container : XuLyBase0
    {
        private V6ComboBox cboDanhMuc;
        private Panel panelView;
        private V6Label v6Label1;

        private void InitializeComponent()
        {
            this.cboDanhMuc = new V6Controls.V6ComboBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.panelView = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelView);
            this.panel1.Controls.Add(this.cboDanhMuc);
            this.panel1.Controls.Add(this.v6Label1);
            // 
            // cboDanhMuc
            // 
            this.cboDanhMuc.AccessibleName = "kieu_post";
            this.cboDanhMuc.BackColor = System.Drawing.SystemColors.Window;
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDanhMuc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboDanhMuc.FormattingEnabled = true;
            this.cboDanhMuc.Location = new System.Drawing.Point(71, 3);
            this.cboDanhMuc.Name = "cboDanhMuc";
            this.cboDanhMuc.Size = new System.Drawing.Size(209, 21);
            this.cboDanhMuc.TabIndex = 5;
            this.cboDanhMuc.SelectedIndexChanged += new System.EventHandler(this.cboDanhMuc_SelectedIndexChanged);
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00126";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(6, 3);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(50, 13);
            this.v6Label1.TabIndex = 4;
            this.v6Label1.Text = "Chứng từ";
            // 
            // panelView
            // 
            this.panelView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelView.Location = new System.Drawing.Point(3, 30);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(554, 316);
            this.panelView.TabIndex = 6;
            // 
            // V6IMPORT2EIV_Container
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "V6IMPORT2EIV_Container";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        public V6IMPORT2EIV_Container(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                LoadListALIMXLS();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = "V6IMPORT2EIV_Container.";
            }

            V6ControlFormHelper.SetStatusText2(text, id);
        }

        public override void DoHotKey(Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                if (CurrentControl != null)
                {
                    CurrentControl.DoHotKey(keyData);
                }
            }
            base.DoHotKey(keyData);
        }

        protected override void Nhan()
        {
            if (this.ShowConfirmMessage(V6Text.ExecuteConfirm) != DialogResult.Yes)
            {
                return;
            }
            //base.Nhan();
            if(CurrentControl != null) CurrentControl.DoHotKey(Keys.F9);
        }

        


        private DataRow SelectedRow
        {
            get
            {
                if (cboDanhMuc.DataSource != null && cboDanhMuc.SelectedItem is DataRowView && cboDanhMuc.SelectedIndex >= 0)
                {
                    return ((DataRowView)cboDanhMuc.SelectedItem).Row;
                }
                return null;
            }
        }
        private DataTable ALIM2XLS_DATA;
        private void LoadListALIMXLS()
        {
            //ALIM2XLS_DATA = V6BusinessHelper.Select("ALIM2XLS", "*", "IMPORT_YN='1'", "", "stt").Data;

            SqlParameter[] plist =
            {
                new SqlParameter("@isAdmin", V6Login.IsAdmin),
                new SqlParameter("@mrights", V6Login.UserRight.Mrights),
                new SqlParameter("@r_add", V6Login.UserInfo["r_add"].ToString().Trim()),
                new SqlParameter("@moduleID", V6Login.SelectedModule),
            };
            ALIM2XLS_DATA = V6BusinessHelper.Select("ALIM2XLS", "*", "KT_LONG='1'"
                + " AND Rtrim(MO_TA) in (Select Itemid from V6menu Where (((1=@isAdmin or (dbo.VFA_Inlist_MEMO([Itemid], @mrights)=1 and dbo.VFA_Inlist_MEMO([Itemid], @r_add)=1)))"
                + " AND hide_yn<>1 AND Module_id=@moduleID))",
                "", "STT", plist).Data;

            cboDanhMuc.ValueMember = "MA_CT";
            cboDanhMuc.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
            cboDanhMuc.DataSource = ALIM2XLS_DATA;
            cboDanhMuc.ValueMember = "MA_CT";
            cboDanhMuc.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
        }

        private void cboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeControl();
        }

        Dictionary<string, XuLyBase> ControlsDictionary = new Dictionary<string, XuLyBase>();
        private XuLyBase CurrentControl = null;
        private void ChangeControl()
        {
            try
            {
                string ma_ct = SelectedRow["MA_CT"].ToString().Trim();
                string itemid = SelectedRow["mo_ta"].ToString().Trim();
                DataRow menuRow = V6Menu.GetRow(itemid);
                if (menuRow == null)
                {
                    this.ShowWarningMessage(V6Text.NoDefine + " " + itemid);
                    return;
                }
                string item_id = menuRow["itemid"].ToString().Trim();
                string program = menuRow["program"].ToString().Trim();
                program = program.Replace("XLS", "EIV");
                string reportCaption = menuRow["title"].ToString().Trim();
                string reportCaption2 = menuRow["title2"].ToString().Trim();

                foreach (var control in ControlsDictionary)
                {
                    if (control.Key != ma_ct) control.Value.Visible = false;
                    //else control.Value.Visible = true;
                }

                if (ControlsDictionary.ContainsKey(ma_ct) && !ControlsDictionary[ma_ct].IsDisposed)
                {
                    if (!panelView.Contains(ControlsDictionary[ma_ct]))
                    {
                        panelView.Controls.Add(ControlsDictionary[ma_ct]);
                    }
                    ControlsDictionary[ma_ct].Visible = true;
                    ControlsDictionary[ma_ct].Focus();
                    CurrentControl = ControlsDictionary[ma_ct];
                }
                else
                {
                    XuLyBase c = null;
                    switch (ma_ct)
                    {
                        case "AP1":
                            c = new XLSAP1_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "AR1":
                            c = new XLSAR1_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "GL1":
                            c = new XLSGL1_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "POA":
                            c = new EIVPOA_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "POB":
                            c = new XLSPOB_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "IND":
                            c = new XLSIND_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "IXA":
                            c = new XLSIXA_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "SOA":
                            c = new EIVSOA_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "SOB":
                            c = new XLSSOB_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "SOH":
                            c = new XLSSOH_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "TA1":
                            c = new XLSTA1_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        case "CA1":
                            c = new XLSCA1_Control(item_id, program, program, program, reportCaption, reportCaption2);
                            break;
                        //default:
                        //    c = new XuLyBase(item_id);
                        //    break;
                    }

                    if (c != null)
                    {
                        CurrentControl = c;
                        c.ALIM2XLS_Config = new ALIM2XLS_CONFIG(SelectedRow.ToDataDictionaryUpper());
                        c.Name = ma_ct;
                        c.Dock = DockStyle.Fill;
                        var cName = c.Name;
                        c.Disposed += delegate(object sender1, EventArgs e1)
                        {
                            try
                            {
                                ControlsDictionary.Remove(cName);
                            }
                            catch (Exception ex)
                            {
                                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, ma_ct), ex);
                            }
                        };
                        ControlsDictionary[ma_ct] = c;
                        panelView.Controls.Add(c);
                        c.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChangeControl", ex);
            }
        }
    }
}
