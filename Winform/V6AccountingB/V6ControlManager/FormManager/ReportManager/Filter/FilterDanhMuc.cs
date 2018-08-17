using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Init;
using V6ReportControls;
using V6Structs;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class FilterDanhMuc : FilterBase
    {
        public FilterDanhMuc()
        {
            InitializeComponent();
            SetHideFields(V6Setting.Language);
        }

        public FilterDanhMuc(string tableName)
        {
            InitializeComponent();
            _tableName = tableName;
            SetHideFields(V6Setting.Language);

            //date1.Value = V6Setting.M_ngay_ct1;
            //date2.Value = V6Setting.M_ngay_ct2;

            var fields_vvar_filter = V6Lookup.GetValueByTableName(tableName, "vLfScatter");//.GetReportFilterFields(tableName);
            MadeControls(tableName, fields_vvar_filter);
        }

        private string _tableName;
        private V6TableStruct _tStruct;

        private void FilterDanhMuc_Load(object sender, EventArgs e)
        {
            if (Controls.Count == 5)
            {
                groupBox1.Top = date2.Bottom + 5;
                //groupBox1.Height = Height = groupBox1.Top - 5;
            }
        }

        private void MadeControls(string tableName, string fields_vvar_filter)
        {
            string err = "";
            try
            {
                _tStruct = V6BusinessHelper.GetTableStruct(tableName);
                panel1.AddMultiFilterLine(_tStruct, fields_vvar_filter);
            }
            catch (Exception ex)
            {
                err += "\n" + ex.Message;
            }

            if (err.Length > 0)
            {
                this.WriteToLog(GetType() + ".MadeControls error!", err);
            }
        }

        private void MadeControl(int index, string fieldName)
        {
            var lineControl = new FilterLineDynamic
            {
                FieldName = fieldName.ToUpper(),
                Caption = CorpLan2.GetFieldHeader(fieldName)
            };
            if (_tStruct.ContainsKey(fieldName.Trim().ToUpper()))
            {
                if (",nchar,nvarchar,ntext,char,varchar,text,xml,"
                    .Contains("," + _tStruct[fieldName.ToUpper()].sql_data_type_string + ","))
                {
                    lineControl.AddTextBox();
                }
                else if (",date,smalldatetime,datetime,"
                    .Contains("," + _tStruct[fieldName.ToUpper()].sql_data_type_string + ","))
                {
                    lineControl.AddDateTimePick();
                }
                else
                {
                    lineControl.AddNumberTextBox();
                }
            }
            lineControl.Location = new Point(10, 10 + 30 * index);
            panel1.Controls.Add(lineControl);
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields.Add("TAG", "TAG");
                GridViewHideFields.Add("STT_REC", "STT_REC");
                GridViewHideFields.Add("STT_REC0", "STT_REC0");
                GridViewHideFields.Add("STT_REC_TT", "STT_REC_TT");
                GridViewHideFields.Add("MA_TT", "MA_TT");
                GridViewHideFields.Add("MA_GD", "MA_GD");
            }
            else
            {
                GridViewHideFields.Add("TAG", "TAG");
                GridViewHideFields.Add("STT_REC", "STT_REC");
                GridViewHideFields.Add("STT_REC0", "STT_REC0");
                GridViewHideFields.Add("STT_REC_TT", "STT_REC_TT");
                GridViewHideFields.Add("MA_TT", "MA_TT");
                GridViewHideFields.Add("MA_GD", "MA_GD");
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@cTable VARCHAR(40),
            //@cOrder VARCHAR(40),
            //@cKey VARCHAR(8000)

            var result = new List<SqlParameter>();
            //V6Setting.M_ngay_ct1 = date1.Value;
            //V6Setting.M_ngay_ct2 = date2.Value;

            result.Add(new SqlParameter("@cTable", _tableName));
            result.Add(new SqlParameter("@cOrder", V6TableHelper.GetDefaultSortField(_tableName)));

            
            string keyDate = "";
            if (date1.Value != null)
            {
                keyDate += string.Format(" And date0 >= '{0}'", ((DateTime)date1.Value).ToString("yyyyMMdd"));
            }
            if (date2.Value != null)
            {
                keyDate += string.Format(" And date0 <= '{0}'", ((DateTime)date2.Value).ToString("yyyyMMdd"));
            }
            if (keyDate.Length > 4)
            {
                keyDate = keyDate.Substring(4);
            }

            var keyDynamic = "";
            var and_or = radAnd.Checked ? " And " : " Or  ";
            foreach (Control c in panel1.Controls)
            {
                FilterLineDynamic cc = c as FilterLineDynamic;
                if (cc != null && cc.IsSelected)
                {
                    keyDynamic += and_or + cc.GetQuery(cc.TableLabel);
                }
            }
            if (keyDynamic.Length > 4) keyDynamic = keyDynamic.Substring(4);

            string cKey;
            if (keyDate.Length > 0)
            {
                if (keyDynamic.Length > 0)
                {
                    cKey = string.Format("{0} And ({1})", keyDate, keyDynamic);
                }
                else
                {
                    cKey = keyDate;
                }
            }
            else
            {
                if (keyDynamic.Length > 0)
                {
                    cKey = string.Format("({0})", keyDynamic);
                }
                else
                {
                    cKey = "1=1";
                }
            }
             
            result.Add(new SqlParameter("@cKey", cKey));
            
            return result;
        }

        
    }
}
