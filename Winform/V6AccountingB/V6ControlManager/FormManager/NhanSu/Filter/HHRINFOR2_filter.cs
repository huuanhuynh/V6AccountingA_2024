﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6ControlManager.FormManager.NhanSu.View;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6Controls;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.NhanSu.Filter
{
    public partial class HHRINFOR2_filter : FilterBase
    {
        public HHRINFOR2_filter()
        {
            InitializeComponent();
            SetParentRowEvent += HRAPPFAMILY_filter_SetParentRowEvent;
            lineMaNS.VvarTextBox.V6LostFocus += VvarTextBox_V6LostFocus;
            lineMaNS.VvarTextBox.CheckOnLeave = true;
            lineMaNS.VvarTextBox.F2 = false;
        }

        void VvarTextBox_V6LostFocus(object sender)
        {
            NoGridControl parent = FindParent<NoGridControl>() as NoGridControl;
            if (parent != null)
            {
                parent.btnNhan.PerformClick();
            }
            else
            {
                OneGridControl parent1 = FindParent<OneGridControl>() as OneGridControl;
                if (parent1 != null)
                {
                    parent1.btnNhan.PerformClick();
                }
            }
        }

        void HRAPPFAMILY_filter_SetParentRowEvent(IDictionary<string, object> data)
        {
            if (data != null)
            {
                if (data.ContainsKey("STT_REC")) txtSttRec.Text = data["STT_REC"].ToString().Trim();
                if (data.ContainsKey("MA_NS")) lineMaNS.VvarTextBox.Text = data["MA_NS"].ToString().Trim();
            }
        }

        void Getinfo_ns()
        {
            try
            {
                // Get name from HRPERSONAL
                var infor = V6BusinessHelper.Select("HRPERSONAL", "*",
                        "stt_rec = '" + txtSttRec.Text.Trim() + "'");
                if (infor.TotalRows > 0)
                {

                    if (ObjectAndString.ObjectToString((infor.Data.Rows[0]["MID_NAME"])) != "")
                    {
                        Txtten_ns.Text =
                            (infor.Data.Rows[0]["LAST_NAME"].ToString().Trim() + " " +
                             infor.Data.Rows[0]["MID_NAME"].ToString().Trim() + " " +
                             infor.Data.Rows[0]["FIRST_NAME"].ToString().Trim()).ToUpper();

                        Txtten_ns.Text = Txtten_ns.Text + "\r\n" + " ( " + infor.Data.Rows[0]["EMP_ID"].ToString().Trim() + ")";

                    }
                    else
                    {
                        Txtten_ns.Text = (infor.Data.Rows[0]["LAST_NAME"].ToString().Trim() + " " +
                                          infor.Data.Rows[0]["FIRST_NAME"].ToString().Trim()).ToUpper();

                        Txtten_ns.Text = Txtten_ns.Text + "\r\n" + " ( " + infor.Data.Rows[0]["EMP_ID"].ToString().Trim() + ")";
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Getinfo_ns", ex);
            }
        }


        //public override void SetData(IDictionary<string, object> data)
        //{
        //    //base.SetData(data);
        //}

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        
        public override List<SqlParameter> GetFilterParameters()
        {
            //@stt_rec varchar(50),
            //@ma_ns varchar(50),
            //@Advance nvarchar(max),
            //@User_id int =1

            var result = new List<SqlParameter>();

            if (lineMaNS.VvarTextBox.Data == null) throw new Exception("Chọn ma_ns!");
            txtSttRec.Text = lineMaNS.VvarTextBox.Data["STT_REC"].ToString().Trim();

            Getinfo_ns();
          
            result.Add(new SqlParameter("@stt_rec", txtSttRec.Text.Trim()));
            result.Add(new SqlParameter("@ma_ns", lineMaNS.StringValue));
            var and = radAnd.Checked;

            var advance = GetFilterStringByFields(new List<string>()
            {
                "MA_NS",
            }, and);

            result.Add(new SqlParameter("@Advance", advance));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            
            return result;
        }

        private void lineMaNS_Leave(object sender, EventArgs e)
        {
            try
            {
                if (lineMaNS.VvarTextBox.Data != null)
                {
                    txtSttRec.Text = lineMaNS.VvarTextBox.Data["STT_REC"].ToString().Trim();
                    Getinfo_ns();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".lineMaNS_Leave", ex);
            }
        }

    }
}
