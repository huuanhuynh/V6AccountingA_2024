using System.Collections.Generic;

namespace V6ThuePost.VnptObjects
{
    public class Customers
    {
        public Customers()
        {
            Customer_List = new List<Customer>();
        }

        /// <summary>
        /// &lt;Customers>&lt;Customer>...&lt;/Customer>[...]&lt;/Customers>
        /// </summary>
        public List<Customer> Customer_List;
    }
}
