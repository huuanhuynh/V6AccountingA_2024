﻿using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter
{
    public partial class PRINT_AMAD : InChungTuFilterBase
    {
        public PRINT_AMAD()
        {
            InitializeComponent();

            F3 = false;
            F5 = false;

            //TxtStt_rec = _report_stt_rec;
            SetFieldValueEvent += AAPCTAP1_SetFieldValueEvent;
        }

        void AAPCTAP1_SetFieldValueEvent(string sttrec)
        {
            TxtStt_rec.Text = sttrec;
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@stt_rec; varchar(50),
            
            var result = new List<SqlParameter>();
            
            
            result.Add(new SqlParameter("@stt_rec", TxtStt_rec.Text.Trim()));
            
            
            return result;
        }

        
    }
}
