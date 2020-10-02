namespace V6ThuePost.ViettelV2Objects
{
    public class ItemInfo
    {
        public int lineNumber = 0;
        public string itemCode = "";
        public string itemName = "";
        public string unitName = "";
        public decimal unitPrice = 0;
        public decimal quantity = 0.0m;
        public decimal itemTotalAmountWithoutTax = 0;
        public decimal taxPercentage = 0.0m;
        public decimal taxAmount = 0.0m;
        public decimal discount = 0.0m;
        public decimal itemDiscount = 0.0m;
        public string adjustmentTaxAmount;
        public int isIncreaseItem = 0;
    }
}
