using DevExpress.Data.Filtering;
using System;
using V6AccountingBusiness;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.DXreport
{
    public class V6DXMoneyToWords : ICustomFunctionOperatorBrowsable
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
                    return "MoneyToWords(decimal, string lang, string ma_nt)\r\nĐọc số tiền. lang là V hoặc E. ma_nt VND USD... NUMBER(doc so thong thuong)";
                return "MoneyToWords(decimal, string lang, string ma_nt)\r\nRead the amount. lang is V or E. ma_nt VND USD... NUMBER(read number only)";
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
            string result = "";
            if (operands[0] == null) return result;
            decimal money = 0;
            string lang = V6Setting.Language;
            string ma_nt = "VND";
            if (operands.Length > 0)
            {
                money = ObjectAndString.ObjectToDecimal(operands[0]);
            }
            if (operands.Length > 1)
            {
                lang = operands[1].ToString();
            }
            if (operands.Length > 2)
            {
                ma_nt = operands[2].ToString();
            }

            if (ma_nt.ToLower() == "number")
                result = DocSo.DocSoTien(money);
            else
                result = V6BusinessHelper.MoneyToWords(money, lang, ma_nt);
			
            return result;
        }
        /// <summary>
        /// Tên hàm.
        /// </summary>
        public string Name {
            get {
                return "MoneyToWords";
            }
        }
        public Type ResultType(params Type[] operands) {
            return typeof(string);
        }
    }
}
