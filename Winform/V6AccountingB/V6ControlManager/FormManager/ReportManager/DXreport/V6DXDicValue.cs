using DevExpress.Data.Filtering;
using System;
using V6AccountingBusiness;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.DXreport
{
    public class V6DXDicValue : ICustomFunctionOperatorBrowsable
    {
        public FunctionCategory Category {
            get {
                return FunctionCategory.Text;
            }
        }
        public string Description {
            get
            {
                if (V6Setting.IsVietnamese)
                    return "DicValue(string dic, string key, string typeCDN)\r\nLấy giá trị trong chuỗi Dic Key:value;Key2:Value2. type C chuỗi D date N number";
                return "DicValue(string dic, string key, string typeCDN)\r\nGet value in Dic string Key:value;Key2:Value2. type C string D date N number";
            }
        }

        /// <summary>
        /// Kiểm tra có đủ tham số truyền vào chưa?
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool IsValidOperandCount(int count)
        {
            return count >= MinOperandCount && count <= MaxOperandCount;
        }
        public bool IsValidOperandType(int operandIndex, int operandCount, Type type) {
            return true;
        }
        public int MaxOperandCount {
            get {
                return 3;
            }
        }
        public int MinOperandCount {
            get {
                return 2;
            }
        }
        /// <summary>
        /// Xử lý hàm.
        /// </summary>
        /// <param name="operands"></param>
        /// <returns></returns>
        public object Evaluate(params object[] operands)
        {
            string value = "";
            
            string dicstring = "" + operands[0];
            string key = "" + operands[1];
            var dic = ObjectAndString.StringToDictionary(dicstring);
            if (dic.ContainsKey(key.ToUpper())) value = "" + dic[key.ToUpper()];

            string type = "C";
            if (operands.Length > 2) type = operands[2].ToString().ToUpper();

            switch (type)
            {
                case "D":
                    return ObjectAndString.StringToDate(value);
                    break;
                case "N":
                    return ObjectAndString.StringToDecimal(value);
                    break;
                default:
                    return value;
                    break;
            }
        }
        /// <summary>
        /// Tên hàm.
        /// </summary>
        public string Name {
            get {
                return "DicValue";
            }
        }
        public Type ResultType(params Type[] operands) {

            string type = "C";
            if (operands.Length > 2) type = operands[2].ToString().ToUpper();

            switch (type)
            {
                case "D":
                    return typeof(DateTime);
                    break;
                case "N":
                    return typeof(decimal);
                    break;
                default:
                    return typeof(string);
                    break;
            }
        }
    }
}
