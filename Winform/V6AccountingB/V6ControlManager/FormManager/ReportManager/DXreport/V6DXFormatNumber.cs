using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.DXreport
{
    public class V6DXFormatNumber : ICustomFunctionOperatorBrowsable
    {
        public FunctionCategory Category {
            get {
                return FunctionCategory.Text;
            }
        }
        public string Description {
            get {
                if (V6Setting.IsVietnamese)
                    return "FormatNumber(number, int decimals, decimal_, thousand_, show0=0)\r\nĐịnh dạng số hiển thị tùy chỉnh số lẻ, dấu thập phân và dấu nghìn.";
                return "FormatNumber(number, int decimals, decimal_, thousand_, show0=0)\r\nFormat a number with custom decimalPlaces and separators.";
            }
        }
        public bool IsValidOperandCount(int count) {
            return count >=1 && count <= 4;
        }
        public bool IsValidOperandType(int operandIndex, int operandCount, Type type) {
            return true;
        }
        public int MaxOperandCount {
            get {
                return 5;
            }
        }
        public int MinOperandCount {
            get {
                return 1;
            }
        }
        public object Evaluate(params object[] operands) {
            decimal number = ObjectAndString.ObjectToDecimal(operands[0]);
            if (number == 0)
            {
                bool show0 = false; if (operands.Length > 4) show0 = ObjectAndString.ObjectToBool(operands[4]);
                if (!show0)
                {
                    return "";
                }
            }
            int decimals = -1; if (operands.Length > 1) ObjectAndString.ObjectToInt(operands[1]);
            string decimalSeparator = V6Options.M_NUM_POINT; if (operands.Length > 2) decimalSeparator = operands[2].ToString();
            string thousandSeparator = V6Options.M_NUM_SEPARATOR; if (operands.Length > 3) thousandSeparator = operands[3].ToString();
            

            string res = ObjectAndString.NumberToString(number, decimals, decimalSeparator, thousandSeparator);
            return res;
        }
        public string Name {
            get {
                return "FormatNumber";
            }
        }
        public Type ResultType(params Type[] operands) {
            return typeof(string);
        }
    }
}
