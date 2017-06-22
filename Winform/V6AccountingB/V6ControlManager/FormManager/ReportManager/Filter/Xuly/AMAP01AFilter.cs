using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using V6Controls;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter.Xuly
{
    public partial class AMAP01AFilter : FilterBase
    {
        public AMAP01AFilter()
        {
            InitializeComponent();

            if (V6Login.MadvcsCount <= 1)
            {
                //lineMaDvcs.Enabled = false;
            }
            
            Ready();
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                //_hideFields.Add("TAG", "TAG");
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
            var result = new List<SqlParameter>();
            var condition = "";
            //if (flag == "DAY")
            //{
            condition = string.Format("MA_VITRI like '{0}%'", lineMaVitri.StringValue.Replace("'", "''"));
                
            //}
            //else
            //{
            //    condition = string.Format("MA_KHO='{0}' and MA_VITRI='{1}'", lineMakho.StringValue.Replace("'", "''"),
            //        lineMaVitri.StringValue.Replace("'", "''"));
            //}

            result = new List<SqlParameter>
            {
                new SqlParameter("@MA_KHO", lineMakho.StringValue),
                new SqlParameter("@Advance", condition),
            };

            return result;
        }

        private string flag = "VITRI";
        public override void SetData(SortedDictionary<string, object> data)
        {
            try
            {
                if (data.ContainsKey("FLAG")) flag = data["FLAG"].ToString().Trim();

                if (data.ContainsKey("MA_KHO"))
                {
                    lineMakho.VvarTextBox.Text = data["MA_KHO"].ToString().Trim();
                }
                if (data.ContainsKey("MA_HINH"))
                {
                    lineMaVitri.VvarTextBox.Text = data["MA_HINH"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        
    }
}
