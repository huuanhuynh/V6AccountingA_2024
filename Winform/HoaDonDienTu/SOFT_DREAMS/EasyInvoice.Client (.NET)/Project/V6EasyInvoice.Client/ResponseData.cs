using System.Collections.Generic;
using EasyInvoice.Json;

namespace V6EasyInvoice.Client
{
	[JsonObject]
	public class ResponseData
	{
		public string Pattern
		{
			get;
			set;
		}

		public string Serial
		{
			get;
			set;
		}

		public Dictionary<string, string> KeyInvoiceNo
		{
			get;
			set;
		}

		public Dictionary<string, string> KeyInvoiceMsg
		{
			get;
			set;
		}

		public string Html
		{
			get;
			set;
		}

		public int? InvoiceStatus
		{
			get;
			set;
		}

		public List<int> InvoiceNo
		{
			get;
			set;
		}

		public Dictionary<string, string> DigestData
		{
			get;
			set;
		}

		public string Ikey
		{
			get;
			set;
		}

		public string[] Ikeys
		{
			get;
			set;
		}

		public List<InvoiceInfo> Invoices
		{
			get;
			set;
		}
	}
}
