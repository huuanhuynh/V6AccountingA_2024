using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Models.Core.Constants
{
    public class DefinitionIndex
    {
        public enum Model : ushort
        {
            /// <summary>
            /// Value: 0
            /// </summary>
            Customer = 0,

            /// <summary>
            /// Value: 1
            /// </summary>
            CustomerGroup = 1,

            /// <summary>
            /// Value: 2
            /// </summary>
            Employee = 2,

            /// <summary>
            /// Value: 3
            /// </summary>
            EmployeeGroup = 3,

            /// <summary>
            /// Value: $
            /// </summary>
            Ward = 4,

            /// <summary>
            /// Value: 5
            /// </summary>
            District = 5,

            /// <summary>
            /// Value: 6
            /// </summary>
            Province = 6,

            /// <summary>
            /// Value: 7
            /// </summary>
            PaymentMethod = 7,
        }

        public enum Field : byte
        {
            /// <summary>
            ///     Applies for OID, UID.
            ///     <para/>Value: 0
            /// </summary>
            ID = 0,

            /// <summary>
            ///     Applies for CustomerCode (MaKH), WardCode (MaPhuong) etc.
            ///     <para/>Value: 1
            /// </summary>
            Code = 1,

            /// <summary>
            ///     Applies for Name (Ten), FullName (HoTen) ect.
            ///     <para/>Value: 2
            /// </summary>
            Name = 2,
            
            /// <summary>
            ///     Applies for Status (TinhTrang) column.
            ///     <para/>Value: 3
            /// </summary>
            Status = 3,

            /// <summary>
            ///     Applies for Note (GhiChu) column.
            ///     <para/>Value: 4
            /// </summary>
            Note = 4,

            /// <summary>
            ///     Applies for OtherName (TenKhac) and OtherFullName (HoTenKhac).
            ///     <para/>Value: 4
            /// </summary>
            OtherName = 5,

            /// <summary>
            ///     Applies for Maturity (HanThanhToan).
            /// </summary>
            Maturity = 6
        }
    }
}
