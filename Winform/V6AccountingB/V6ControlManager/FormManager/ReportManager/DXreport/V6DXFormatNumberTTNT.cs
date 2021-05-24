using DevExpress.Data.Filtering;
using System;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.DXreport
{
    public class V6DXFormatNumberTTNT : ICustomFunctionOperatorBrowsable
    {
        public FunctionCategory Category {
            get {
                return FunctionCategory.Text;
            }
        }
        public string Description {
            get {
                return "FormatNumberTTNT(object number, show0=0)\r\nFormat a number with V6 decimalPlaces and separators.";
            }
        }
        public bool IsValidOperandCount(int count) {
            return count == 1;
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
        public object Evaluate(params object[] operands) {
            decimal number = ObjectAndString.ObjectToDecimal(operands[0]);
            bool show0 = false; if (operands.Length > 1) show0 = ObjectAndString.ObjectToBool(operands[1]);
            string res = ObjectAndString.NumberToString(number, V6Options.M_IP_R_TIENNT, V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR, show0);
            return res;
        }
        public string Name {
            get {
                return "FormatNumberTTNT";
            }
        }
        public Type ResultType(params Type[] operands) {
            return typeof(string);
        }
    }
}
