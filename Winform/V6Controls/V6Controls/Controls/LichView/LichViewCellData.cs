using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace V6Controls.Controls.LichView
{
    public class LichViewCellData
    {
        /// <summary>
        /// Ngày, trong đoạn từ 1 đến 31
        /// </summary>
        public int Key { get; set; }
        public decimal Num1 { get; set; }
        public decimal Num2 { get; set; }
        public decimal Num3 { get; set; }
        public string Detail1 { get; set; }
        public string Detail2 { get; set; }
        public string Detail3 { get; set; }
        /// <summary>
        /// Vùng được vẽ của toàn ô trên control.
        /// </summary>
        public Rectangle Rectangle { get; set; }
        /// <summary>
        /// Vùng được vẽ của Detail1 trên control.
        /// </summary>
        public RectangleF Detail1Rectangle { get; set; }
        /// <summary>
        /// Vùng được vẽ của Detail2 trên control.
        /// </summary>
        public RectangleF Detail2Rectangle { get; set; }
        /// <summary>
        /// Vùng được vẽ của Detail3 trên control.
        /// </summary>
        public RectangleF Detail3Rectangle { get; set; }
    }
}
