using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6ThuePost.ResponseObjects;
using V6ThuePostBkavApi.ResponseObjects;
using V6Tools.V6Convert;

namespace V6ThuePostManager
{
    public class PostManagerParams
    {
        /// <summary>
        /// Dữ liệu 3 bảng map, ad, am?
        /// </summary>
        public DataSet DataSet { get; set; }
        /// <summary>
        /// <para>Khi download pdf viettel 1: thể hiện, mặc định(2): Chuyển đổi</para>
        /// <para>Vnpt M: mới, S: sửa, X: xóa-cancel</para>
        /// </summary>
        public string Mode { get; set; }
        /// <summary>
        /// 1:Viettel 2:Vnpt 3:Bkav 4:Vnpt_token 5:SoftDreams 6:ThaiSon 7:Monet 8:Minvoice 9...
        /// </summary>
        public string Branch { get; set; }

        public string Dir { get; set; }
        public string FileName { get; set; }
        public string RptFileFull { get; set; }
        /// <summary>
        /// Fkey từ V6
        /// </summary>
        public string Fkey_hd { get; set; }
        /// <summary>
        /// <para>Key hóa đơn cũ dùng cho thay thế.</para>
        /// <para>1_Viettel:AA/17E0003470</para>
        /// <para>3_Bkav:[01GTKT0/001]_[AB/19E]_[0000009]</para>
        /// <para>6_ThaiSon:SO_CT0</para>
        /// </summary>
        public string Fkey_hd_tt { get; set; }
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Dữ liệu hóa đơn bị thay thế.
        /// </summary>
        public IDictionary<string, object> AM_old { get; set; }
        /// <summary>
        /// Dữ liệu hóa đơn mới.
        /// </summary>
        public IDictionary<string, object> AM_new { get; set; }
        /// <summary>
        /// Mã tạo ra bởi server HĐĐT
        /// </summary>
        public string V6PartnerID { get; set; }
        /// <summary>
        /// F4 hay F6 ?
        /// </summary>
        public string Key_Down { get; set; }

        /// <summary>
        /// Mẫu hóa đơn vd:01GTKT0/001
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        /// Ký hiệu hóa đơn vd:AA/19E
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// Ngày chứng từ.
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        public Control Form { get; set; }

        /// <summary>
        /// Kết quả trả về của Web_service đã được phân tích.
        /// </summary>
        public PM_Result Result = null;
    }

    /// <summary>
    /// PostManagerResult
    /// </summary>
    public class PM_Result
    {
        private const string EXCEPTION_MESSAGE = "EXCEPTION_MESSAGE";
        private const string RESULT_ERROR = "RESULT_ERROR";
        private const string RESULT_MESSAGE = "RESULT_MESSAGE";
        private const string RESULT_OBJECT = "RESULT_OBJECT";
        private const string RESULT_STRING = "RESULT_STRING";
        private const string SO_HD = "SO_HD";
        private const string ID = "ID";
        private const string SECRET_CODE = "SECRET_CODE";
        public string ResultString
        {
            get
            {
                if (V6ReturnValues != null)
                {
                    return V6ReturnValues.RESULT_STRING;
                }
                return null;
            }
            set
            {
                if (V6ReturnValues == null) V6ReturnValues = new V6Return();
                V6ReturnValues.RESULT_STRING = value;
            }
        }
        public string ResultMessage
        {
            get
            {
                if (V6ReturnValues != null)
                {
                    return V6ReturnValues.RESULT_MESSAGE;
                }
                return null;
            }
            set
            {
                if (V6ReturnValues == null) V6ReturnValues = new V6Return();
                V6ReturnValues.RESULT_MESSAGE = value;
            }
        }
        public string ResultErrorMessage
        {
            get
            {
                if (V6ReturnValues != null)
                {
                    return V6ReturnValues.RESULT_ERROR_MESSAGE;
                }
                return null;
            }
            set
            {
                if (V6ReturnValues == null) V6ReturnValues = new V6Return();
                V6ReturnValues.RESULT_ERROR_MESSAGE = value;
            }
        }
        
        public object ResultObject
        {
            get
            {
                if (V6ReturnValues != null)
                {
                    return V6ReturnValues.RESULT_OBJECT;
                }
                return null;
            }
        }

        //public IDictionary<string,object> ResultDictionary0 = null;
        /// <summary>
        /// Chứa tất cả các giá trị đơn có được do hàm trả về (được phân tích và lưu lại).
        /// <para>Trong đó có 2 biến cơ bản [RESULT_STRING] và [RESULT_OBJECT].</para>
        /// </summary>
        public V6Return V6ReturnValues = null;

        /// <summary>
        /// Số hóa đơn. (Thông với V6ReturnValues.SO_HD)
        /// </summary>
        public string InvoiceNo
        {
            get
            {
                if (V6ReturnValues != null)
                {
                    return V6ReturnValues.SO_HD;
                }
                return null;
            }
            set
            {
                if (V6ReturnValues == null) V6ReturnValues = new V6Return();
                V6ReturnValues.SO_HD = value;
            }
        }
        /// <summary>
        /// ID trả về của webservice.
        /// </summary>
        public string Id
        {
            get
            {
                if (V6ReturnValues != null)
                {
                    return V6ReturnValues.ID;
                }
                return null;
            }
        }
        public string SecrectCode
        {
            get
            {
                if (V6ReturnValues != null)
                {
                    return V6ReturnValues.SECRET_CODE;
                }
                //else if (ResultDictionary != null && ResultDictionary.ContainsKey("MTC"))
                //{
                //    return V6ReturnValues["MCT"].ToString();
                //}
                return null;
            }
        }

        /// <summary>
        /// SO_HD:Value;ID:value2;SECRET_CODE:SecrectCode
        /// </summary>
        public string PartnerInfors
        {
            get
            {
                string result = null;
                if (!string.IsNullOrEmpty(InvoiceNo))   result += string.Format(";{0}:{1}", SO_HD, InvoiceNo);
                if (!string.IsNullOrEmpty(ID))          result += string.Format(";{0}:{1}", ID, Id);
                if (!string.IsNullOrEmpty(SecrectCode)) result += string.Format(";{0}:{1}", SECRET_CODE, SecrectCode);

                if (result != null && result.Length > 1) result = result.Substring(1);
                return result;
            }
        }

        public bool IsSuccess(string mode)
        {
            // Trong mỗi nhánh phải có return true của riêng nó.
            if (mode == "E_G1")
            {
                if (!string.IsNullOrEmpty(ResultErrorMessage)) return false;
                return true;
            }
            else if (mode == "E_H1")
            {
                if (!string.IsNullOrEmpty(ResultErrorMessage)) return false;
                return true;
            }
            else if (mode == "E_T1")
            {
                if (!string.IsNullOrEmpty(ResultErrorMessage)) return false;
                return true;
            }
            else if (mode == "E_S1")
            {
                if (ResultObject == null) return false;
                if (ResultObject is InvoiceResult) // BKAV
                {
                    InvoiceResult bkavIR = ResultObject as InvoiceResult;
                    if (bkavIR.Status == 0) return true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(ResultErrorMessage)) return false;
                    return true;
                }
            }
            else if (mode == "E_T1")
            {
                if (!string.IsNullOrEmpty(ResultErrorMessage)) return false;
                return true;
            }
            else if (mode == "M")
            {
                if (!string.IsNullOrEmpty(ResultErrorMessage)) return false;
                return true;
            }
            else
            {
                if (!string.IsNullOrEmpty(ResultErrorMessage)) return false;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return ResultString;
        }
    }
}
