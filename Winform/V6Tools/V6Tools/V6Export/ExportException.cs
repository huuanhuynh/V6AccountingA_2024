using System;

namespace V6Tools.V6Export
{
    class ExportException : Exception
    {
        public ExportException(string m) : base("ExportException: " + m) { }
    }
}
