using DevExpress.Data.Filtering;
using System;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.DXreport
{
    public class V6DXGetDatabase : ICustomFunctionOperatorBrowsable
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
                    return "GetDatabase(tablename, field, string keysDic, returnType CDN)\r\nLấy 1 giá trị csdl, chuỗi keysDic dạng Key:value;Key2:Value2. type trả về C chuỗi D date N number";
                return "GetDatabase(tablename, field, string keysDic, returnType CDN)\r\nGet 1 value in database, keysDic format Key:value;Key2:Value2. Return type C string D date N number";
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
                return 4;
            }
        }
        public int MinOperandCount {
            get {
                return 3;
            }
        }
        /// <summary>
        /// Xử lý hàm.
        /// </summary>
        /// <param name="operands"></param>
        /// <returns></returns>
        public object Evaluate(params object[] operands)
        {
            string tablename = "" + operands[0];
            string field = "" + operands[1];
            string keydic = "" + operands[2];
            var keys = ObjectAndString.StringToDictionary(keydic);
            object value = V6BusinessHelper.SelectOneValue(tablename, field, keys);
            //return value;
            
            string type = "C";
            if (operands.Length > 3) type = operands[3].ToString().ToUpper();

            switch (type)
            {
                case "D":
                    return ObjectAndString.ObjectToDate(value);
                    break;
                case "N":
                    return ObjectAndString.ObjectToDecimal(value);
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
                return "GetDatabase";
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
