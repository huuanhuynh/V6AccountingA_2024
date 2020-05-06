using System.Windows.Forms;
using V6Controls.Controls;

namespace V6Controls.Structs
{
    public class AlctControls
    {
        public int FOrder { get; set; }
        /// <summary>
        /// Control chính
        /// </summary>
        public Control DetailControl { get; set; }
        public string LabelText { get; set; }
        public LookupButton LookupButton { get; set; }
        public bool IsCarry { get; set; }
        /// <summary>
        /// fstatus, Trạng trái ẩn/hiện đăng ký trong Alct1.
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
