namespace V6ThuePostApi.PostObjects
{
    public class ItemInfo
    {
        public int lineNumber = 1;
        public string itemCode = "ENGLISH_COURSE";
        public string itemName = "Khóa học tiếng anh";
        public string unitName = "khóa học";
        public decimal unitPrice = 3500000.0m;
        public decimal quantity = 10.0m;
        public decimal itemTotalAmountWithoutTax = 35000000m;
        public decimal taxPercentage = 10.0m;
        public decimal taxAmount = 0.0m;
        public decimal discount = 0.0m;
        public decimal itemDiscount = 150000.0m;
    }
}
