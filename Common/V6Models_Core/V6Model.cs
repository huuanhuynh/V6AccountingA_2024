using System;
using System.ComponentModel.DataAnnotations;

namespace V6Soft.Models.Core
{
    /// <summary>
    ///     Base class for all models in V6 system.
    /// </summary>
    public abstract class V6Model
    {
        public Guid UID { get; set; }

        /// <summary>
        ///     Column: user_id0
        ///     Description: Người dùng nhập dữ liêu.
        /// </summary>
        public decimal CreatedUserId { get; set; }

        /// <summary>
        ///     Column: user_id2
        ///     Description: Người dùng chỉnh sửa dữ liêu.
        /// </summary>
        public decimal ModifiedUserId { get; set; }

        /// <summary>
        ///     Column: time0
        ///     Description: Thời gian tạo dữ liêu.
        /// </summary>
        //[Required]
        public string CreatedTime { get; set; }

        /// <summary>
        ///     Column: time2
        ///     Description: Thời gian sửa dữ liệu.
        /// </summary>
        //[Required]
        public string ModifiedTime { get; set; }

        /// <summary>
        ///     Column: date0
        ///     Description: Ngày tạo dữ liệu.
        /// </summary>
        //[Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        ///     Column: date2
        ///     Description: Ngày sửa dữ liệu.
        /// </summary>
        //[Required]
        public DateTime ModifiedDate { get; set; }
    }
}
