using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ReportControls
{
    public class PanelFilter : Panel
    {
        private int _maxIndex = -1;
        private int marginLeft = 10, marginTop = 3;
        [Category("V6")]
        [Description("Khoảng cách từ cạnh trái đến các filterLine tính bằng pixel.")]
        [DefaultValue(10)]
        public int LeftMargin
        {
            get { return marginLeft; }
            set
            {
                marginLeft = value;
                ResetLeft();
            }//viết sự kiện
        }
        [Category("V6")]
        [Description("Khoảng cách từ cạnh trên đến filterLine đầu tiên tính bằng pixel.")]
        [DefaultValue(3)]
        public int TopMargin
        {
            get { return marginTop; }
            set
            {
                var a = value - marginTop;
                marginTop = value; 
                ResetTop(a);
            }//viết sự kiện
        }

        private void ResetLeft()
        {
            foreach (FilterLineBase control in Controls)
            {
                control.Left = marginLeft;
            }
        }

        private void ResetTop(int a)
        {
            foreach (FilterLineBase control in Controls)
            {
                control.Top += a;
            }
        }

        public void AddFilterLineControl(V6TableStruct structTable, string fieldName, string vvar, string filter)
        {
            try
            {
                var NAME = fieldName.Trim().ToUpper();
                FilterLineDynamic lineControl = new FilterLineDynamic();
                lineControl.FieldName = NAME;
                lineControl.FieldCaption = CorpLan2.GetFieldHeader(NAME);
                if (structTable.ContainsKey(NAME))
                {
                    if (",nchar,nvarchar,ntext,char,varchar,text,xml,"
                        .Contains("," + structTable[NAME].sql_data_type_string + ","))
                    {
                        //lineControl.AddTextBox();
                        lineControl.AddVvarTextBox(vvar, filter);
                    }
                    else if (",date,smalldatetime,datetime,"
                        .Contains("," + structTable[NAME].sql_data_type_string + ","))
                    {
                        lineControl.AddDateTimePick();
                    }
                    else
                    {
                        lineControl.AddNumberTextBox();
                    }
                }
                _maxIndex++;
                lineControl.Location = new Point(marginLeft, marginTop + 25 * _maxIndex);
                lineControl.Width = Width - marginLeft;
                lineControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                Controls.Add(lineControl);
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage(fieldName + ": " + ex.Message, ex.Source);
            }
        }
        
        //public void AddMultiFilterLine(V6TableStruct structTable, string[] fields)
        //{
        //    foreach (string field in fields)
        //    {
        //        string err = "";
        //        try
        //        {
        //            if(!string.IsNullOrEmpty(field))
        //                AddFilterLineControl(structTable, field, "", "");
        //        }
        //        catch (Exception ex)
        //        {
        //            err += "\n" + ex.Message;
        //        }
        //        if (err.Length > 0)
        //        {
        //            V6ControlFormHelper.ShowErrorMessage("AddMultiFilterLine error!" + err, "PanelFilter");
        //        }
        //    }
        //}

        /// <summary>
        /// Thêm vào các ô nhập filterLine tự động
        /// </summary>
        /// <param name="structTable"></param>
        /// <param name="adv">Field:vvar;Field2:vvar2:Field2 like '%'</param>
        public void AddMultiFilterLine(V6TableStruct structTable, string adv)
        {
            var spliter = ObjectAndString.SplitString(adv);
            foreach (string s in spliter)
            {
                string err = "";
                try
                {
                    var sss = s.Split(new[] {':'}, 3);
                    var key = sss[0];
                    var vvar = "";
                    string filter = null;
                    if (sss.Length >= 2)
                    {
                        vvar = sss[1];
                    }
                    if (sss.Length == 3)
                    {
                        filter = sss[2].Replace("''", "'");
                    }
                    AddFilterLineControl(structTable, key, vvar, filter);
                }
                catch (Exception ex)
                {
                    err += "\n" + ex.Message;
                }
                if (err.Length > 0)
                {
                    V6ControlFormHelper.ShowErrorMessage("AddMultiFilterLine error!" + err, "PanelFilter");
                }
            }
        }

        /// <summary>
        /// Kiểu And
        /// </summary>
        public string QueryString
        {
            get
            {
                string result = "";
                foreach (FilterLineDynamic c in Controls)
                {
                    if (c.IsSelected)
                        result += " And " + c.Query;
                }
                if (result.Length > 4) result = result.Substring(4);
                return result;
            }
        }

        public string GetQueryString(V6TableStruct tableStruct, string tableLable=null, bool and = true)
        {
            var and_or = and ? " AND " : " OR ";
            string result = "";
            foreach (FilterLineDynamic c in Controls)
            {
                if(tableStruct.ContainsKey(c.FieldName))
                if (c.IsSelected)
                    result += and_or + c.GetQuery(tableLable);
            }
            if (result.Length > 4) result = result.Substring(4);
            return result;
        }

        public SortedDictionary<string, object> GetQueryKeys()
        {
            var result = new SortedDictionary<string, object>();
            foreach (FilterLineDynamic c in Controls)
            {
                if (c.IsSelected)
                    result.Add(c.FieldName, c.ObjectValue);
            }
            return result;
        } 

    }
}
