using System;

namespace SignTokenCore
{
	internal static class UtilsWSSignToken
	{
		internal static int convert = 0;

		internal static string getHashInvWithToken68(string Account, string ACpass, string xmlInvData, string username, string password, string serialCert, int type, string invToken, string pattern, string serial, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				xmlInvData,
				username,
				password,
				serialCert,
				type,
				invToken,
				pattern,
				serial,
				UtilsWSSignToken.convert
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "GetHashInvWithTokenTT68", args);
		}

		internal static string getHashInvWithToken32(string Account, string ACpass, string xmlInvData, string username, string password, string serialCert, int type, string invToken, string pattern, string serial, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				xmlInvData,
				username,
				password,
				serialCert,
				type,
				invToken,
				pattern,
				serial,
				UtilsWSSignToken.convert
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "getHashInvWithToken", args);
		}

		internal static string publishInvWithToken(string Account, string ACpass, string xmlInvData, string username, string password, string pattern, string serial, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				xmlInvData,
				username,
				password,
				pattern,
				serial
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "publishInvWithToken", args);
		}

		internal static string AdjustReplaceInvWithToken(string Account, string ACpass, string xmlInvData, string username, string password, int type, string pattern, string serial, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				xmlInvData,
				username,
				password,
				type,
				pattern,
				serial
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "AdjustReplaceInvWithToken", args);
		}

		internal static string CancelInvoiceWithToken(string Account, string ACpass, string xmlData, string username, string password, string pattern, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				xmlData,
				username,
				password,
				pattern
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "CancelInvoiceWithToken", args);
		}

		internal static string rolBackWithToken(string Account, string ACpass, string id, string username, string password, string pattern, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				id,
				username,
				password,
				pattern
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "rolBackWithToken", args);
		}

		internal static string importCertWithToken(string Account, string ACpass, string username, string password, string certStr, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				username,
				password,
				certStr
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "importCertWithToken", args);
		}

		internal static string getHashInv(string Account, string ACpass, string username, string password, string serialCert, string xmlFkeyInv, string pattern, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				username,
				password,
				serialCert,
				xmlFkeyInv,
				pattern
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "getHashInv", args);
		}

		internal static string getStatusInv(string Account, string ACpass, string username, string password, string xmlFkeyInv, string pattern, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				username,
				password,
				xmlFkeyInv,
				pattern
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "getStatusInv", args);
		}

		internal static string importInv(string username, string password, string xmlData, string linkWS)
		{
			object[] args = new object[]
			{
				xmlData,
				username,
				password,
				0
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "ImportInv", args);
		}

		internal static string ImportAndPublishInv(string Account, string ACpass, string xmlInvData, string username, string pass, string pattern, string serial, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				xmlInvData,
				username,
				pass,
				pattern,
				serial,
				0
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "ImportAndPublishInv", args);
		}

		internal static string ImportAndCreateBarcodeInv(string Account, string ACpass, string xmlInvData, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				xmlInvData,
				4
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "TaxService", "ImportAndCreateBarcodeInv", args);
		}

		internal static string ConfirmPaymentDetail(string username, string pass, string invToken, string linkWS)
		{
			object[] args = new object[]
			{
				invToken,
				username,
				pass
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "BusinessService", "confirmPaymentDetail", args);
		}

		internal static string SendAgainEmailServ(string Account, string ACpass, string username, string password, string xmlDataInvoiceEmail, string hdPattern, string Serial, string linkWS)
		{
			object[] args = new object[]
			{
				Account,
				ACpass,
				username,
				password,
				xmlDataInvoiceEmail,
				hdPattern,
				Serial
			};
			return (string)WsProxy.CallWebService(linkWS, "", "", "PublishService", "SendAgainEmailServ", args);
		}
	}
}
