namespace V6ThuePost.ViettelObjects
{
    public class SummarizeInfo
    {
        public decimal sumOfTotalLineAmountWithoutTax = 0;
        public decimal totalAmountWithoutTax = 0;
        public decimal totalTaxAmount = 0;
        public decimal totalAmountWithTax = 0;
        public string totalAmountWithTaxInWords = "";
        public decimal discountAmount = 0.0m;
        public decimal taxPercentage = 0.0m;
    }
}
