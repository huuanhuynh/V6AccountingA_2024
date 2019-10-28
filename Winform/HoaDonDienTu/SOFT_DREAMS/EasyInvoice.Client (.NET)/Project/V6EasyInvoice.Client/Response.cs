using EasyInvoice.Json;

namespace V6EasyInvoice.Client
{
	public class Response
	{
		public int? Status
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		[JsonProperty]
		public ResponseData Data
		{
			get;
			set;
		}
	}
}
