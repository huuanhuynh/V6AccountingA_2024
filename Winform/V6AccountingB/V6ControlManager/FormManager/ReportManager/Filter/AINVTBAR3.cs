﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6Controls;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINVTBAR3 : FilterBase
    {
        public AINVTBAR3()
        {
            InitializeComponent();
            //txtMalo.SetInitFilter("");
            txtSL_TD1.TextAlign = HorizontalAlignment.Left;
            txtSL_TD2.TextAlign = HorizontalAlignment.Left;
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            string cKey = string.Format("a.MA_LO = '{0}'", txtMalo.Text);
            return new List<SqlParameter>
            {
                new SqlParameter("@cTable", "ALLO"),
                new SqlParameter("@cOrder", ""),
                new SqlParameter("@cKey", cKey),
            };
        }

        /// <summary>
        /// Sau xử lý F9
        /// </summary>
        /// <param name="s"></param>
        public override void Call1(object s = null)
        {
            ClearAll();

        }

        /// <summary>
        /// Mã đã sử dụng
        /// </summary>
        /// <param name="s"></param>
        public override void Call2(object s = null)
        {
            txtMalo.Alert();
            //txtMalo.BackColor = Color.Red;
            lblStatus1.Visible = true;
            GetInForAlert();
        }

        /// <summary>
        /// F8 Xóa
        /// </summary>
        /// <param name="s"></param>
        public override void Call3(object s = null)
        {
            ClearAll();
            txtMalo.Focus();
        }

        private void txtMalo_V6LostFocus(object sender)
        {
            XuLyLayThongTin();
            XuLyQuetBarcode();
        }

        private void ClearAll()
        {
            lblStatus1.Visible = false;
            txtMalo.Clear();
            txtMalo.ExistRowInTable();
            XuLyLayThongTin();
        }

        private void XuLyLayThongTin()
        {
            try
            {

               
                if (txtMaKH.Data != null)
                {
                    txtTenKH.Text = txtMaKH.Data["TEN_KH"].ToString().Trim();
                }
                else
                {
                    txtTenKH.Clear();
                }

                if (txtMaVT.Data != null)
                {
                    txtTenVT.Text = txtMaVT.Data["TEN_VT"].ToString().Trim();
                    txtDVT.Text = txtMaVT.Data["DVT"].ToString().Trim();
                }
                else
                {
                    txtTenVT.Clear();
                    txtDVT.Clear();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyLayThongTin", ex);
            }
        }

        private void GetInForAlert()
        {

            if (txtSL_TD2.Value == 0)
            {
                lblStatus1.Text = "Hết hạn sử dụng (hoặc khóa không sử dụng)! ";
            }
            else
            {
                string infor_lo = "";



                if (txtMalo.Data != null)
                {
                    var ngay_td1 = ObjectAndString.ObjectToString(txtMalo.Data["NGAY_TD1"], "dd/MM/yyyy");

                    infor_lo = txtMalo.Data["GC_TD1"].ToString().Trim();
                    infor_lo = infor_lo.Replace(",", "---");
                    infor_lo += " ngày " + ngay_td1;
                }
                else
                {
                    infor_lo = "";
                }

                lblStatus1.Text = "Mã đã sử dụng! : " + infor_lo;
            }
        }

        private void XuLyQuetBarcode()
        {
            try
            {
                if (txtMalo.Data != null)
                {
                    XuLyBase0 base0 = FindParent<XuLyBase0>() as XuLyBase0;
                    if (base0 != null)
                    {
                        base0.btnNhan_Click(null, null);
                    }
                    lblStatus1.Visible = false;
                }
                else
                {
                    ShowMainMessage(V6Text.Wrong);
                    txtMalo.Alert();
                    //txtMalo.BackColor = Color.Red;
                    lblStatus1.Visible = true;
                    GetInForAlert();

                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyQuetBarcode", ex);
            }
        }

        private void txtMalo_Leave(object sender, EventArgs e)
        {
            if (!IsDisposed && txtMalo.Text != "") txtMalo_V6LostFocus(sender);
        }
    }
}