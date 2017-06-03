﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOVVTH3 : FilterBase
    {
        public ACOVVTH3()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = false;

            // "COVVTH3XL";
            
            txtNgay_ct1.Value = V6Setting.M_ngay_ct1;
            txtNgay_ct2.Value = V6Setting.M_ngay_ct2;
            


            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            SetHideFields("V");
            
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                _hideFields = new SortedDictionary<string, string> {{"TAG", "TAG"}};
            }
            else
            {
                
            }
            
        }
       

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
 

            int luyke =chk_Luy_ke.Checked ? 1 : 0;

        var result = new List<SqlParameter>
            {
                 
                    //@tk	varchar(16),
                    //@ngay_ct1 char(8),
                    //@ngay_ct2 char(8),
                    //@cach_tinh int,
                    //@condition	nvarchar(max),
                    //@advCondition nvarchar(max)

                 
                new SqlParameter("@Ngay_ct1", txtNgay_ct1.Value.ToString("yyyyMMdd")),
                new SqlParameter("@Ngay_ct2", txtNgay_ct2.Value.ToString("yyyyMMdd")),
                new SqlParameter("@Luyke", luyke),
                new SqlParameter("@Advance", "")
                


                
            };
            
            return result;
        }

        
    }
}
