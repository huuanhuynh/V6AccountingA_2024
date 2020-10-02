﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V6ThuePost.ResponseObjects
{
    /// <summary>
    /// Đối tượng hứng giá trị trả về cần thiết cho V6.
    /// </summary>
    public class V6Return
    {
        /// <summary>
        /// Đối tượng trả về nếu có Model.
        /// </summary>
        public object RESULT_OBJECT;
        /// <summary>
        /// Toàn bộ chuỗi trả về của server.
        /// </summary>
        public string RESULT_STRING { get; set; }
        /// <summary>
        /// Chuỗi lỗi nếu có lỗi.
        /// </summary>
        public string RESULT_ERROR_MESSAGE;
        /// <summary>
        /// Câu thông báo trả về.
        /// </summary>
        public string RESULT_MESSAGE;
        /// <summary>
        /// Số hóa đơn trả về từ WS.
        /// </summary>
        public string SO_HD;
        /// <summary>
        /// Mã số bí mật. MTC.
        /// </summary>
        public string SECRET_CODE;
        /// <summary>
        /// Đường dẫn của file download được.
        /// </summary>
        public string PATH;


        /// <summary>
        /// ID (GUID) trả về của webservice.
        /// </summary>
        public string ID { get; set; }

        //Indexer:
        //public string this[string resultString]
        //{
        //    get { throw new NotImplementedException(); }
        //    set { throw new NotImplementedException(); }
        //}
    }
}
