using System;

namespace V6Tools.V6Convert
{
    public class ConvertException : Exception
    {
        public ConvertException(string m) : base("ConvertException: " + m) { }
    }
}
