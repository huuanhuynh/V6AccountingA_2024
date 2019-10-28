using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Albc
{
    public class AlbcFieldInfo
    {
        public string FieldName { get; set; }
        public AlbcFieldType FieldType { get; set; }
        public int FieldWidth { get; set; }

        public string GetFormat()
        {
            return "format";
        }

        public AlbcFieldInfo Copy()
        {
            return new AlbcFieldInfo()
            {
                FieldName = FieldName,
                FieldType = FieldType,
                FieldWidth = FieldWidth,
            };
        }
    }

    public enum AlbcFieldType
    {
        /// <summary>
        /// String
        /// </summary>
        C = 1,
        /// <summary>
        /// Date
        /// </summary>
        D = 2,
        /// <summary>
        /// Number
        /// </summary>
        N = 3
    }
}
