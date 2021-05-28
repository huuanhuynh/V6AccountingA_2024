using DevExpress.Data.Filtering;
using System;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.DXreport
{
    public class V6DXToString : ICustomFunctionOperatorBrowsable
    {
        public FunctionCategory Category {
            get {
                return FunctionCategory.Text;
            }
        }
        public string Description {
            get {
                if (V6Setting.IsVietnamese)
                    return "ToString(object, string format)\r\nChuyển ngày thành chuỗi có định dạng hoặc object khác.";
                return "ToString(object, string format)\r\nFormat date/object to string with format.";
            }
        }
        /// <summary>
        /// Kiểm tra có đủ tham số truyền vào chưa?
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool IsValidOperandCount(int count) {
            return count >=2 && count <= 2;
        }
        public bool IsValidOperandType(int operandIndex, int operandCount, Type type) {
            return true;
        }
        public int MaxOperandCount {
            get {
                return 2;
            }
        }
        public int MinOperandCount {
            get {
                return 1;
            }
        }
        /// <summary>
        /// Xử lý hàm.
        /// </summary>
        /// <param name="operands"></param>
        /// <returns></returns>
        public object Evaluate(params object[] operands)
        {
            string res = "";
            if (operands[0] == null) return res;
            if (ObjectAndString.IsDateTimeType(operands[0].GetType()) || ObjectAndString.IsNumber(operands[0]))
            {
                res = ObjectAndString.ObjectToString(operands[0], operands[1].ToString());
            }
            else
            {
                res = ObjectAndString.ObjectToString(operands[0], operands[1].ToString());
            }
            
            return res;
        }
        /// <summary>
        /// Tên hàm.
        /// </summary>
        public string Name {
            get {
                return "ToString";
            }
        }
        public Type ResultType(params Type[] operands) {
            return typeof(string);
        }
    }
}
