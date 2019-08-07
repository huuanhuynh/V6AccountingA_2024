using System;
using System.Windows.Forms;
using V6Init;

namespace V6Controls
{
    public class NumberTienNt:V6NumberTextBox
    {
        public NumberTienNt()
        {
            DecimalPlaces = V6Options.M_IP_TIEN_NT;
        }
    }

    public class NumberTien : V6NumberTextBox
    {
        public NumberTien()
        {
            DecimalPlaces = V6Options.M_IP_TIEN;
        }
    }

    public class NumberSoluong : V6NumberTextBox
    {
        public NumberSoluong()
        {
            DecimalPlaces = V6Options.M_IP_SL;
        }
    }

    public class NumberTygia : V6NumberTextBox
    {
        public NumberTygia()
        {
            DecimalPlaces = V6Options.M_IP_TY_GIA;
        }
    }

    public class NumberGia : V6NumberTextBox
    {
        public NumberGia()
        {
            DecimalPlaces = V6Options.M_IP_GIA;
        }
    }

    public class NumberGiaNt : V6NumberTextBox
    {
        public NumberGiaNt()
        {
            DecimalPlaces = V6Options.M_IP_GIA_NT;
        }
    }
    
    /// <summary>
    /// Control số cho việc nhập tháng (1-12);
    /// </summary>
    public class NumberMonth : V6NumberTextBox
    {
        public NumberMonth()
        {
            DecimalPlaces = 0;
            try
            {
                Value = V6Setting.M_SV_DATE.Month;
            }
            catch (Exception)
            {
                //
            }
            TextChanged += NumberMonth_TextChanged;
            V6LostFocus += NumberMonth_V6LostFocus;
        }

        void NumberMonth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Value < 1) Value = 0;
                if (Value > 12) Value = 12;
            }
            catch (Exception)
            {
                //
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                if (Value < 12) Value++;
                return true;
            }
            
            if (keyData == Keys.Down)
            {
                if (Value > 1) Value--;
                return true;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void NumberMonth_V6LostFocus(object sender)
        {
            if (Value < 1) Value = 1;
            else if (Value > 12) Value = 12;
        }
    }
    
    /// <summary>
    /// Control số cho việc nhập năm (4 số);
    /// </summary>
    public class NumberYear : V6NumberTextBox
    {
        public NumberYear()
        {
            DecimalPlaces = 0;
            try
            {
                Value = V6Setting.M_SV_DATE.Year;
            }
            catch (Exception)
            {
                //
            }
            Leave += NumberYear_Leave;
            V6LostFocus += NumberYear_V6LostFocus;
        }

        void NumberYear_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Value <= 0) Value = V6Setting.M_SV_DATE.Year;
            }
            catch (Exception)
            {
                //
            }
        }

        void NumberYear_V6LostFocus(object sender)
        {
            
        }
    }
}
