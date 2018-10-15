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
}
