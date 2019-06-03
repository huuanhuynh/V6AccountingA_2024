using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
        public string Mode { get; set; }
        /// <summary>
        /// 1:Viettel   2:Vnpt  3:Bkav
        /// </summary>
        public string Branch { get; set; }

        public string Dir { get; set; }
        public string FileName { get; set; }
        public string RptFileFull { get; set; }
        public string Fkey_hd { get; set; }
        /// <summary>
        /// Key hóa đơn cũ dùng cho thay thế.
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

        public string V6PartnerID { get; set; }

        /// <summary>
        /// Mẫu hóa đơn vd:01GTKT0/001
        /// </summary>
        public string Parttern;

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
                if (ResultDictionary != null && ResultDictionary.ContainsKey(RESULT_STRING))
                {
                    return ObjectAndString.ObjectToString(ResultDictionary[RESULT_STRING]);
                }
                return null;
            }
            set
            {
                if (ResultDictionary == null) ResultDictionary = new SortedDictionary<string, object>();
                ResultDictionary[RESULT_STRING] = value;
            }
        }
        public string ResultMessage
        {
            get
            {
                if (ResultDictionary != null && ResultDictionary.ContainsKey(RESULT_MESSAGE))
                {
                    return ObjectAndString.ObjectToString(ResultDictionary[RESULT_MESSAGE]);
                }
                return null;
            }
            set
            {
                if(ResultDictionary == null) ResultDictionary = new SortedDictionary<string, object>();
                ResultDictionary[RESULT_MESSAGE] = value;
            }
        }
        public string ResultError
        {
            get
            {
                if (ResultDictionary != null && ResultDictionary.ContainsKey(RESULT_ERROR))
                {
                    return ObjectAndString.ObjectToString(ResultDictionary[RESULT_ERROR]);
                }
                return null;
            }
            set
            {
                if(ResultDictionary == null) ResultDictionary = new SortedDictionary<string, object>();
                ResultDictionary[RESULT_ERROR] = value;
            }
        }
        
        public string ExceptionMessage
        {
            get
            {
                if (ResultDictionary != null && ResultDictionary.ContainsKey(EXCEPTION_MESSAGE))
                {
                    return ObjectAndString.ObjectToString(ResultDictionary[EXCEPTION_MESSAGE]);
                }
                return null;
            }
            set
            {
                if(ResultDictionary == null) ResultDictionary = new SortedDictionary<string, object>();
                ResultDictionary[EXCEPTION_MESSAGE] = value;
            }
        }

        public object ResultObject
        {
            get
            {
                if (ResultDictionary != null && ResultDictionary.ContainsKey(RESULT_OBJECT))
                {
                    return ResultDictionary[RESULT_OBJECT];
                }
                return null;
            }
        }

        /// <summary>
        /// Chứa tất cả các giá trị đơn có được do hàm trả về (được phân tích và lưu lại).
        /// <para>Trong đó có 2 biến cơ bản [RESULT_STRING] và [RESULT_OBJECT].</para>
        /// </summary>
        public IDictionary<string,object> ResultDictionary = null;

        public string InvoiceNo
        {
            get
            {
                if (ResultDictionary != null && ResultDictionary.ContainsKey(SO_HD))
                {
                    return ResultDictionary[SO_HD].ToString();
                }
                return null;
            }
            set
            {
                if (ResultDictionary == null) ResultDictionary = new SortedDictionary<string, object>();
                ResultDictionary[SO_HD] = value;
            }
        }
        /// <summary>
        /// ID trả về của webservice.
        /// </summary>
        public string Id
        {
            get
            {
                if (ResultDictionary != null && ResultDictionary.ContainsKey(ID))
                {
                    return ResultDictionary[ID].ToString();
                }
                return null;
            }
        }
        public string SecrectCode
        {
            get
            {
                if (ResultDictionary != null && ResultDictionary.ContainsKey(SECRET_CODE))
                {
                    return ResultDictionary[SECRET_CODE].ToString();
                }
                else if (ResultDictionary != null && ResultDictionary.ContainsKey("MTC"))
                {
                    return ResultDictionary["MCT"].ToString();
                }
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
                if (!string.IsNullOrEmpty(ResultError)) return false;
                if (!string.IsNullOrEmpty(ExceptionMessage)) return false;
                return true;
            }
            else if (mode == "E_H1")
            {
                if (!string.IsNullOrEmpty(ResultError)) return false;
                if (!string.IsNullOrEmpty(ExceptionMessage)) return false;
                return true;
            }
            else if (mode == "E_T1")
            {
                if (!string.IsNullOrEmpty(ResultError)) return false;
                if (!string.IsNullOrEmpty(ExceptionMessage)) return false;
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
                    if (!string.IsNullOrEmpty(ResultError)) return false;
                    if (!string.IsNullOrEmpty(ExceptionMessage)) return false;
                    return true;
                }
            }
            else if (mode == "E_T1")
            {
                if (!string.IsNullOrEmpty(ResultError)) return false;
                if (!string.IsNullOrEmpty(ExceptionMessage)) return false;
                return true;
            }
            else if (mode == "M")
            {
                if (!string.IsNullOrEmpty(ResultError)) return false;
                if (!string.IsNullOrEmpty(ExceptionMessage)) return false;
                return true;
            }
            else
            {
                if (!string.IsNullOrEmpty(ResultError)) return false;
                if (!string.IsNullOrEmpty(ExceptionMessage)) return false;
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
