using V6Init;

namespace V6Controls
{
    public class NumberTienNt:V6NumberTextBox
    {
        public NumberTienNt()
        {
            if(V6Options.V6OptionValues != null)
                DecimalPlaces = V6Options.M_IP_TIEN_NT;
            else
                DecimalPlaces = 0;
        }
    }

    public class NumberTien : V6NumberTextBox
    {
        public NumberTien()
        {
            if (V6Options.V6OptionValues != null)
                DecimalPlaces = V6Options.M_IP_TIEN;
            else
                DecimalPlaces = 0;
        }
    }

    public class NumberSoluong : V6NumberTextBox
    {
        public NumberSoluong()
        {
            if (V6Options.V6OptionValues != null)
                DecimalPlaces = V6Options.M_IP_SL;
            else
                DecimalPlaces = 0;

        }
    }

    public class NumberTygia : V6NumberTextBox
    {
        public NumberTygia()
        {
            if (V6Options.V6OptionValues != null)
                DecimalPlaces = V6Options.M_IP_TY_GIA;
            else
                DecimalPlaces = 0;

        }
    }

    public class NumberGia : V6NumberTextBox
    {
        public NumberGia()
        {
            if (V6Options.V6OptionValues != null)
                DecimalPlaces = V6Options.M_IP_GIA;
            else
                DecimalPlaces = 0;
        }
    }

    public class NumberGiaNt : V6NumberTextBox
    {
        public NumberGiaNt()
        {
            if (V6Options.V6OptionValues != null)
                DecimalPlaces = V6Options.M_IP_GIA_NT;
            else
                DecimalPlaces = 0;
        }
    }
}
