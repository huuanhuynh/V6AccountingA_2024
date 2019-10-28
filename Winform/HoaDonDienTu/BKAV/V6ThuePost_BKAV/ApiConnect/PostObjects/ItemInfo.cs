namespace V6ThuePostBkavApi.PostObjects
{
    public class ItemInfo
    {
        public int lineNumber = 0;
        public string itemCode = "";
        public string itemName = "";
        public string unitName = "";
        public decimal unitPrice = 0.0m;
        public decimal quantity = 0.0m;
        public decimal itemTotalAmountWithoutTax = 0m;
        public decimal taxPercentage = 0.0m;
        public decimal taxAmount = 0.0m;
        public decimal discount = 0.0m;
        public decimal itemDiscount = 0.0m;
    }
}
