using System;
using System.Collections.Generic;
using System.Text;

namespace GSM
{
    public class Contact
    {
        public string Name = "";
        public string Number = "";
        public string Info1 = "";
        public string Info2 = "";
        public string Info3 = "";
        public string Info4 = "";
        public string Info5 = "";

        public Contact(string name, string number)
        {
            this.Name = name;
            this.Number = number;
        }
        public override string ToString()
        {
            return Number + " (" + Name + ")";
        }
    }
    public class Contacts : List<Contact>
    {
    }
}
