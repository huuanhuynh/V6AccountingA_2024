using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace V6Controls
{
    public class V6DateTimeColor:V6ColorTextBox
    {  
        //============================================================================
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public V6DateTimeColor()
        {
            InitializeComponent();
            MyInit();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // V6DateTimeColor
            // 
            this.Enter += new System.EventHandler(this.V6ColorDateTime_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Date_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Date_KeyPress);
            this.Leave += new System.EventHandler(this.V6ColorDateTime_Leave);
            this.ResumeLayout(false);

        }

        private void MyInit()
        {
            Write();
        }

        protected string stringFormat = "__/__/____";
        protected char[] valueArray = {'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0'};

        [Category("V6")]
        public string StringValue
        {
            get
            {
                var result = "";
                for (int i = 0; i < valueArray.Length; i++)
                {
                    char c = valueArray[i];
                    if (c != '\0' && char.IsDigit(c))
                    {
                        result += c;
                    }
                    else
                    {
                        result += "_";
                    }

                    if (i == 1 || i == 3)
                    {
                        result += "/";
                    }
                }
                return result;
            }
            set
            {
                try
                {
                    var date = DateTime.ParseExact(value, "d/M/yyyy", null);
                    Value = date;
                }
                catch (Exception)
                {
                    Value = null;
                }
            }
        }

        [Category("V6")]
        [DefaultValue(null)]
        public DateTime? Value
        {
            get
            {
                try
                {
                    //var t = StringValue;
                    //t = t.Replace("_", "");
                    //var index1 = t.IndexOf('/');
                    //var index2 = t.LastIndexOf('/');
                    //var yearString = t.Substring(index2 + 1);
                    //var monthString = t.Substring(index1 + 1, index2 - index1);
                    //if (yearString.Length == 2)
                    //{
                    //    return DateTime.ParseExact(t, "d/M/yy", null);
                    //}
                    //else
                    //{
                    //    return DateTime.ParseExact(t, "d/M/yyyy", null);
                    //}

                    var day = int.Parse("" + valueArray[0] + valueArray[1]);
                    var month = int.Parse("" + valueArray[2] + valueArray[3]);
                    var year = int.Parse("" + valueArray[4] + valueArray[5] + valueArray[6] + valueArray[7]);
                    if (year <= 99)
                    {
                        year = DateTime.Now.Year/100*100 + year;
                    }
                    return new DateTime(year, month, day);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    string s = ((DateTime) value).ToString("ddMMyyyy");
                    valueArray = new char[8];
                    for (int i = 0; i < s.Length; i++)
                    {
                        valueArray[i] = s[i];
                    }
                }
                else
                {
                    valueArray = new [] {'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0'};
                }
                Write();
            }
        }
        [Category("V6")]
        public string ValueDescription
        {
            get
            {
                try
                {
                    if (Value == null)
                        return "null";
                    var date = (DateTime) Value;
                    return string.Format("Ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public override string Query
        {
            get { return GetQuery("=",Value == null? "null" : ((DateTime)Value).ToString("yyyyMMdd")); }
        }

        public override void UseCarry()
        {
            if (Carry)
            {
                try
                {
                    Value = DateTime.ParseExact(CarryString, "d/M/yyyy", null);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void Write()
        {
            //stringValue[0] = 'a';
            Text = StringValue;
        }

        protected override void WndProc(ref Message m)
        {
            // Trap WM_PASTE:
            if (m.Msg == 0x302 && Clipboard.ContainsText())
            {
                var clipboard = Clipboard.GetText().Replace("\n", "").Replace(" ", "");
                DateTime newValue = DateTime.Now;

                try
                {
                    newValue = DateTime.ParseExact(clipboard, "d/M/yyyy", null);
                }
                catch
                {
                    // ignored
                }

                Value = newValue;
                return;
            }
            base.WndProc(ref m);
        }

        private void Date_KeyDown(object sender, KeyEventArgs e)
        {
            //chặn sự kiện của phím để dùng xử lý riêng
            e.Handled = true;
            if (ReadOnly) return;
            //int sls = SelectionStart;
            if (e.KeyCode == Keys.Back)
            {
                DoBack();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DoDelete();
            }
            else if (e.KeyCode == Keys.Up)
            {
                DoUp();
            }
            else if (e.KeyCode == Keys.Down)
            {
                DoDown();
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (SelectionStart > 0)
                {
                    SelectionStart--;
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (SelectionStart < Text.Length)
                {
                    SelectionStart++;
                }
            }
            else if (e.KeyCode == Keys.Home)
            {
                SelectionStart = 0;
            }
            else if (e.KeyCode == Keys.End)
            {
                SelectionStart = Text.Length;
            }
        }

        private void Date_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (ReadOnly) return;
            if (Char.IsDigit(e.KeyChar))
            {   
                InsertStringValue(e.KeyChar);
            }
        }

        private void DoUp()
        {
            try
            {
                var sls = SelectionStart;
                
                switch (sls)
                {
                    case 0:
                    case 1:
                    case 2:
                        var day = "";
                        if (valueArray[0] != '\0') day += valueArray[0];
                        if (valueArray[1] != '\0') day += valueArray[1];
                        if (day.Length == 0) day = "0";
                        var dayNum = (Convert.ToInt32(day)+1);
                        if (dayNum > 31) dayNum = 31;
                        day = dayNum.ToString();
                        if (day.Length < 2) day = "0" + day;
                        valueArray[0] = day[0];
                        valueArray[1] = day[1];
                        break;

                    case 3:
                    case 4:
                    case 5:
                        var mounth = "";
                        if (valueArray[2] != '\0') mounth += valueArray[2];
                        if (valueArray[3] != '\0') mounth += valueArray[3];
                        if (mounth.Length == 0) mounth = "0";
                        var mounthNum = (Convert.ToInt32(mounth)+1);
                        if (mounthNum > 12) mounthNum = 12;
                        mounth = mounthNum.ToString();
                        if (mounth.Length < 2) mounth = "0" + mounth;
                        valueArray[2] = mounth[0];
                        valueArray[3] = mounth[1];
                        break;

                    case 6://..........[dd/MM/|yyyy   ]......
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        var year = "";
                        if (valueArray[4] != '\0') year += valueArray[4];
                        if (valueArray[5] != '\0') year += valueArray[5];
                        if (valueArray[6] != '\0') year += valueArray[6];
                        if (valueArray[7] != '\0') year += valueArray[7];
                        if (year.Length == 0) year = "2000";
                        var yearNum = (Convert.ToInt32(year)+1);
                        if (yearNum > 9999) yearNum = 9999;
                        year = yearNum.ToString();
                        while (year.Length < 4) year = "0" + year;
                        valueArray[4] = year[0];
                        valueArray[5] = year[1];
                        valueArray[6] = year[2];
                        valueArray[7] = year[3];
                        break;
                }
                Write();
                
                SelectionStart = sls;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void DoDown()
        {
            try
            {
                var sls = SelectionStart;

                switch (sls)
                {
                    case 0:
                    case 1:
                    case 2:
                        var day = "";
                        if (valueArray[0] != '\0') day += valueArray[0];
                        if (valueArray[1] != '\0') day += valueArray[1];
                        if (day.Length == 0) day = "0";
                        var dayNum = (Convert.ToInt32(day) - 1);
                        if (dayNum < 1) dayNum = 1;
                        day = dayNum.ToString();
                        if (day.Length < 2) day = "0" + day;
                        valueArray[0] = day[0];
                        valueArray[1] = day[1];
                        break;

                    case 3:
                    case 4:
                    case 5:
                        var mounth = "";
                        if (valueArray[2] != '\0') mounth += valueArray[2];
                        if (valueArray[3] != '\0') mounth += valueArray[3];
                        if (mounth.Length == 0) mounth = "0";
                        var mounthNum = (Convert.ToInt32(mounth) - 1);
                        if (mounthNum < 1) mounthNum = 1;
                        mounth = mounthNum.ToString();
                        if (mounth.Length < 2) mounth = "0" + mounth;
                        valueArray[2] = mounth[0];
                        valueArray[3] = mounth[1];
                        break;

                    case 6://..........[dd/MM/|yyyy   ]......
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        var year = "";
                        if (valueArray[4] != '\0') year += valueArray[4];
                        if (valueArray[5] != '\0') year += valueArray[5];
                        if (valueArray[6] != '\0') year += valueArray[6];
                        if (valueArray[7] != '\0') year += valueArray[7];
                        if (year.Length == 0) year = "2000";
                        var yearNum = (Convert.ToInt32(year) - 1);
                        if (yearNum < 1) yearNum = 1;
                        year = yearNum.ToString();
                        while (year.Length < 4) year = "0" + year;
                        valueArray[4] = year[0];
                        valueArray[5] = year[1];
                        valueArray[6] = year[2];
                        valueArray[7] = year[3];
                        break;
                }
                Write();

                SelectionStart = sls;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Xử lý nút Backspace
        /// </summary>
        private void DoBack()
        {
            try
            {
                var sls = SelectionStart;
                switch (sls)
                {
                    case 0:
                        //Do nothing
                        break;
                    case 1://..........[d|d/MM/yyyy   ]......
                        valueArray[0] = '\0';
                        break;
                    case 2:
                        valueArray[1] = '\0';
                        break;
                    case 3://..........[dd/|MM/yyyy   ]......
                        
                        break;
                    case 4:
                        valueArray[2] = '\0';
                        break;
                    case 5:
                        valueArray[3] = '\0';
                        break;
                    case 6://..........[dd/MM/|yyyy   ]......
                        
                        break;
                    case 7:
                        valueArray[4] = '\0';
                        break;
                    case 8:
                        valueArray[5] = '\0';
                        break;
                    case 9:
                        valueArray[6] = '\0';
                        break;
                    case 10:
                        valueArray[7] = '\0';
                        break;
                }
                Write();
                if (sls > 0) sls--;
                SelectionStart = sls;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Xử lý nút Delete
        /// </summary>
        private void DoDelete()//Cần xử lý kỹ sls
        {
            try
            {
                var sls = SelectionStart;
                switch (sls)
                {
                    case 0://..........[|dd/MM/yyyy   ]......
                        valueArray[0] = '\0';
                        break;
                    case 1:
                        valueArray[1] = '\0';
                        break;
                    case 2://..........[dd|/MM/yyyy   ]......

                        break;
                    case 3:
                        valueArray[2] = '\0';
                        break;
                    case 4:
                        valueArray[3] = '\0';
                        break;
                    case 5://..........[dd/MM|/yyyy   ]......

                        break;
                    case 6:
                        valueArray[4] = '\0';
                        break;
                    case 7:
                        valueArray[5] = '\0';
                        break;
                    case 8:
                        valueArray[6] = '\0';
                        break;
                    case 9:
                        valueArray[7] = '\0';
                        break;
                }
                Write();
                if (sls < 10) sls++;
                SelectionStart = sls;

            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Thêm số vào chuỗi giá trị (StringValue).
        /// </summary>
        /// <param name="c"></param>
        private void InsertStringValue(char c)
        {
            try
            {
                var sls = SelectionStart;
                if(char.IsDigit(c))
                switch (sls)
                {
                    case 0://           [|dd/MM/yyyy   ]
                        valueArray[0] = c;
                        break;
                    case 1://           [d|d/MM/yyyy   ]
                    case 2://           [dd|/MM/yyyy   ]
                        if(sls == 1) sls++;
                    
                        if (valueArray[0] == '3')
                        {
                            if (c == '0' || c == '1')
                                valueArray[1] = c;
                            else valueArray[1] = '0';
                        }
                        else if (valueArray[0] == '0' || valueArray[0] == '1' || valueArray[0] == '2')
                        {
                            valueArray[1] = c;
                        }
                        
                        break;

                    case 3:
                        valueArray[2] = c;
                        break;
                    case 4://           [dd/M|M/yyyy   ]
                    case 5://           [dd/MM|/yyyy   ]
                        if (sls == 4) sls++;

                        if(valueArray[2]=='0')
                            valueArray[3] = c;
                        else if (valueArray[2] == '1' && (c == '0' || c == '1' || c == '2'))
                            valueArray[3] = c;
                        else
                        {
                            valueArray[2] = '0';
                            valueArray[3] = c=='0'?'1':c;
                        }
                        break;

                    case 6:
                        valueArray[4] = c;
                        break;
                    case 7:
                        valueArray[5] = c;
                        break;
                    case 8:
                        valueArray[6] = c;
                        break;
                    case 9:
                        valueArray[7] = c;
                        break;
                    case 10:
                        valueArray[7] = c;
                        break;
                }
                Write();
                if (sls < 10) sls++;
                SelectionStart = sls;
            }
            catch
            {
                // ignored
            }
        }

        private void V6ColorDateTime_Enter(object sender, EventArgs e)
        {
            Write();
            SelectionStart = 0;
        }

        private void V6ColorDateTime_Leave(object sender, EventArgs e)
        {
            CheckValue();
        }

        private void CheckValue()
        {
            Value = Value;
        }

    }
}
