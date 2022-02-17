using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml.Linq;

namespace SignTokenCore
{
	public static class VNPTEInvoiceSignToken
	{
		public static string PublishInvWithToken68(string Account, string ACpass, string xmlInvData, string username, string password, string serialCert, string pattern, string serial, string linkWS)
		{
			string result;
			try
			{
				string hashInvWithToken = UtilsWSSignToken.getHashInvWithToken68(Account, ACpass, xmlInvData, username, password, serialCert, 0, "", pattern, serial, linkWS);
				bool flag = hashInvWithToken.Contains("ERR:");
				if (flag)
				{
					result = hashInvWithToken;
				}
				else
				{
					XElement xElement = XElement.Parse(hashInvWithToken);
					IEnumerable<XElement> enumerable = xElement.Elements("Inv");
					string text = "<Invoices><SerialCert>" + serialCert + "</SerialCert>";
					string text2 = "";
					int num = 1;
					foreach (XElement current in enumerable)
					{
						string value = current.Element("idInv").Value;
						bool flag2 = num == enumerable.Count<XElement>();
						if (flag2)
						{
							text2 += value;
						}
						else
						{
							text2 = text2 + value + ";";
						}
						num++;
					}
					foreach (XElement current2 in enumerable)
					{
						string value2 = current2.Element("key").Value;
						string value3 = current2.Element("idInv").Value;
						string value4 = current2.Element("hashValue").Value;
						string text3 = VNPTEInvoiceSignToken.signHashCert(value4, serialCert);
						bool flag3 = text3.Contains("ERR:-") || "".Equals(text3);
						if (flag3)
						{
							result = text3;
							return result;
						}
						text += "<Inv><key>";
						text += value2;
						text += "</key>";
						text += "<idInv>";
						text += value3;
						text += "</idInv>";
						text += "<signValue>";
						text += text3;
						text += "</signValue></Inv>";
					}
					text += "</Invoices>";
					string text4 = UtilsWSSignToken.publishInvWithToken(Account, ACpass, text, username, password, pattern, serial, linkWS);
					bool flag4 = !text4.Contains("OK");
					if (flag4)
					{
						VNPTEInvoiceSignToken.callThread(Account, ACpass, text2, username, password, pattern, linkWS);
					}
					result = text4;
				}
			}
			catch (Exception ex)
			{
				result = "ERR:5 " + ex.Message;
			}
			return result;
		}

		public static string PublishInvWithToken32(string Account, string ACpass, string xmlInvData, string username, string password, string serialCert, string pattern, string serial, string linkWS)
		{
			string result;
			try
			{
				string hashInvWithToken = UtilsWSSignToken.getHashInvWithToken32(Account, ACpass, xmlInvData, username, password, serialCert, 0, "", pattern, serial, linkWS);
				bool flag = hashInvWithToken.Contains("ERR:");
				if (flag)
				{
					result = hashInvWithToken;
				}
				else
				{
					XElement xElement = XElement.Parse(hashInvWithToken);
					IEnumerable<XElement> enumerable = xElement.Elements("Inv");
					string text = "<Invoices><SerialCert>" + serialCert + "</SerialCert>";
					string text2 = "";
					int num = 1;
					foreach (XElement current in enumerable)
					{
						string value = current.Element("idInv").Value;
						bool flag2 = num == enumerable.Count<XElement>();
						if (flag2)
						{
							text2 += value;
						}
						else
						{
							text2 = text2 + value + ";";
						}
						num++;
					}
					foreach (XElement current2 in enumerable)
					{
						string value2 = current2.Element("key").Value;
						string value3 = current2.Element("idInv").Value;
						string value4 = current2.Element("hashValue").Value;
						string text3 = VNPTEInvoiceSignToken.signHashCert(value4, serialCert);
						bool flag3 = text3.Contains("ERR:-") || "".Equals(text3);
						if (flag3)
						{
							result = text3;
							return result;
						}
						text += "<Inv><key>";
						text += value2;
						text += "</key>";
						text += "<idInv>";
						text += value3;
						text += "</idInv>";
						text += "<signValue>";
						text += text3;
						text += "</signValue></Inv>";
					}
					text += "</Invoices>";
					string text4 = UtilsWSSignToken.publishInvWithToken(Account, ACpass, text, username, password, pattern, serial, linkWS);
					bool flag4 = !text4.Contains("OK");
					if (flag4)
					{
						VNPTEInvoiceSignToken.callThread(Account, ACpass, text2, username, password, pattern, linkWS);
					}
					result = text4;
				}
			}
			catch (Exception ex)
			{
				result = "ERR:5 " + ex.Message;
			}
			return result;
		}

		public static string AdjustReplaceInvWithToken68(string Account, string ACpass, string xmlInvData, string username, string password, string serialCert, int type, string invToken, string pattern, string serial, string linkWS)
		{
			string result;
			try
			{
				string[] array = invToken.Split(new char[]
				{
					';'
				});
				bool flag = array.Length != 3;
				if (flag)
				{
					result = "ERR:4";
				}
				else
				{
					string text = array[0];
					string text2 = array[1];
					int num = int.Parse(array[2]);
					string hashInvWithToken = UtilsWSSignToken.getHashInvWithToken68(Account, ACpass, xmlInvData, username, password, serialCert, type, invToken, pattern, serial, linkWS);
					bool flag2 = hashInvWithToken.Contains("ERR:");
					if (flag2)
					{
						result = hashInvWithToken;
					}
					else
					{
						XElement xElement = XElement.Parse(hashInvWithToken);
						IEnumerable<XElement> enumerable = xElement.Elements("Inv");
						string text3 = string.Concat(new object[]
						{
							"<Invoices><SerialCert>",
							serialCert,
							"</SerialCert><PatternOld>",
							text,
							"</PatternOld><SerialOld>",
							text2,
							"</SerialOld><NoOlde>",
							num,
							"</NoOlde>"
						});
						string text4 = "";
						int num2 = 1;
						foreach (XElement current in enumerable)
						{
							string value = current.Element("idInv").Value;
							bool flag3 = num2 == enumerable.Count<XElement>();
							if (flag3)
							{
								text4 += value;
							}
							else
							{
								text4 = text4 + value + ";";
							}
							num2++;
						}
						foreach (XElement current2 in enumerable)
						{
							string value2 = current2.Element("key").Value;
							string value3 = current2.Element("idInv").Value;
							string value4 = current2.Element("hashValue").Value;
							string text5 = VNPTEInvoiceSignToken.signHashCert(value4, serialCert);
							bool flag4 = text5.Contains("ERR:-") || "".Equals(text5);
							if (flag4)
							{
								VNPTEInvoiceSignToken.callThread(Account, ACpass, text4, username, password, pattern, linkWS);
								result = text5;
								return result;
							}
							text3 += "<Inv><key>";
							text3 += value2;
							text3 += "</key>";
							text3 += "<idInv>";
							text3 += value3;
							text3 += "</idInv>";
							text3 += "<signValue>";
							text3 += text5;
							text3 += "</signValue></Inv>";
						}
						text3 += "</Invoices>";
						string text6 = UtilsWSSignToken.AdjustReplaceInvWithToken(Account, ACpass, text3, username, password, type, pattern, serial, linkWS);
						bool flag5 = !text6.Contains("OK");
						if (flag5)
						{
							VNPTEInvoiceSignToken.callThread(Account, ACpass, text4, username, password, pattern, linkWS);
						}
						result = text6;
					}
				}
			}
			catch (Exception ex)
			{
				result = "ERR:5 " + ex.Message;
			}
			return result;
		}

		public static string AdjustReplaceInvWithToken32(string Account, string ACpass, string xmlInvData, string username, string password, string serialCert, int type, string invToken, string pattern, string serial, string linkWS)
		{
			string result;
			try
			{
				string[] array = invToken.Split(new char[]
				{
					';'
				});
				bool flag = array.Length != 3;
				if (flag)
				{
					result = "ERR:89";
				}
				else
				{
					string text = array[0];
					string text2 = array[1];
					int num = int.Parse(array[2]);
					string hashInvWithToken = UtilsWSSignToken.getHashInvWithToken32(Account, ACpass, xmlInvData, username, password, serialCert, type, invToken, pattern, serial, linkWS);
					bool flag2 = hashInvWithToken.Contains("ERR:");
					if (flag2)
					{
						result = hashInvWithToken;
					}
					else
					{
						XElement xElement = XElement.Parse(hashInvWithToken);
						IEnumerable<XElement> enumerable = xElement.Elements("Inv");
						string text3 = string.Concat(new object[]
						{
							"<Invoices><SerialCert>",
							serialCert,
							"</SerialCert><PatternOld>",
							text,
							"</PatternOld><SerialOld>",
							text2,
							"</SerialOld><NoOlde>",
							num,
							"</NoOlde>"
						});
						string text4 = "";
						int num2 = 1;
						foreach (XElement current in enumerable)
						{
							string value = current.Element("idInv").Value;
							bool flag3 = num2 == enumerable.Count<XElement>();
							if (flag3)
							{
								text4 += value;
							}
							else
							{
								text4 = text4 + value + ";";
							}
							num2++;
						}
						foreach (XElement current2 in enumerable)
						{
							string value2 = current2.Element("key").Value;
							string value3 = current2.Element("idInv").Value;
							string value4 = current2.Element("hashValue").Value;
							string text5 = VNPTEInvoiceSignToken.signHashCert(value4, serialCert);
							bool flag4 = text5.Contains("ERR:-") || "".Equals(text5);
							if (flag4)
							{
								VNPTEInvoiceSignToken.callThread(Account, ACpass, text4, username, password, pattern, linkWS);
								result = text5;
								return result;
							}
							text3 += "<Inv><key>";
							text3 += value2;
							text3 += "</key>";
							text3 += "<idInv>";
							text3 += value3;
							text3 += "</idInv>";
							text3 += "<signValue>";
							text3 += text5;
							text3 += "</signValue></Inv>";
						}
						text3 += "</Invoices>";
						string text6 = UtilsWSSignToken.AdjustReplaceInvWithToken(Account, ACpass, text3, username, password, type, pattern, serial, linkWS);
						bool flag5 = !text6.Contains("OK");
						if (flag5)
						{
							VNPTEInvoiceSignToken.callThread(Account, ACpass, text4, username, password, pattern, linkWS);
						}
						result = text6;
					}
				}
			}
			catch (Exception ex)
			{
				result = "ERR:5 " + ex.Message;
			}
			return result;
		}

		public static string CancelInvoiceWithToken(string Account, string ACpass, string xmlData, string username, string pass, string pattern, string linkWS)
		{
			string result;
			try
			{
				string text = UtilsWSSignToken.CancelInvoiceWithToken(Account, ACpass, xmlData, username, pass, pattern, linkWS);
				result = text;
			}
			catch (Exception ex)
			{
				result = "ERR:5 " + ex.Message;
			}
			return result;
		}

		public static string ImportCertWithToken(string Account, string ACpass, string username, string pass, string serialCert, string linkWS)
		{
			string result;
			try
			{
				X509Certificate2 certificateBySerial = VNPTEInvoiceSignToken.GetCertificateBySerial(serialCert);
				bool flag = certificateBySerial == null;
				if (flag)
				{
					result = "ERR:7";
				}
				else
				{
					byte[] rawData = certificateBySerial.RawData;
					string certStr = Convert.ToBase64String(rawData);
					string text = UtilsWSSignToken.importCertWithToken(Account, ACpass, username, pass, certStr, linkWS);
					result = text;
				}
			}
			catch (Exception ex)
			{
				result = "ERR:5 " + ex.Message;
			}
			return result;
		}

		public static string getStatusInv(string Account, string ACpass, string username, string pass, string xmlFkeyInv, string pattern, string linkWS)
		{
			string result;
			try
			{
				string statusInv = UtilsWSSignToken.getStatusInv(Account, ACpass, username, pass, xmlFkeyInv, pattern, linkWS);
				result = statusInv;
			}
			catch (Exception ex)
			{
				result = "ERR:5 " + ex.Message;
			}
			return result;
		}

		public static string getHashInv(string Account, string ACpass, string username, string pass, string serialCert, string xmlFkeyInv, string pattern, string linkWS)
		{
			string result;
			try
			{
				string hashInv = UtilsWSSignToken.getHashInv(Account, ACpass, username, pass, serialCert, xmlFkeyInv, pattern, linkWS);
				result = hashInv;
			}
			catch (Exception ex)
			{
				result = "ERR:5 " + ex.Message;
			}
			return result;
		}

		public static string PublishInv(string Account, string ACpass, string xmlHash, string username, string password, string serialCert, string pattern, string serial, string linkWS)
		{
			string result;
			try
			{
				XElement xElement = XElement.Parse(xmlHash);
				IEnumerable<XElement> enumerable = xElement.Elements("Inv");
				string text = "<Invoices><SerialCert>" + serialCert + "</SerialCert>";
				string text2 = "";
				int num = 1;
				foreach (XElement current in enumerable)
				{
					string value = current.Element("idInv").Value;
					bool flag = num == enumerable.Count<XElement>();
					if (flag)
					{
						text2 += value;
					}
					else
					{
						text2 = text2 + value + ";";
					}
					num++;
				}
				foreach (XElement current2 in enumerable)
				{
					string value2 = current2.Element("key").Value;
					string value3 = current2.Element("idInv").Value;
					string value4 = current2.Element("hashValue").Value;
					string text3 = VNPTEInvoiceSignToken.signHashCert(value4, serialCert);
					bool flag2 = text3.Contains("ERR:-") || "".Equals(text3);
					if (flag2)
					{
						VNPTEInvoiceSignToken.callThread(Account, ACpass, text2, username, password, pattern, linkWS);
						result = text3;
						return result;
					}
					text += "<Inv><key>";
					text += value2;
					text += "</key>";
					text += "<idInv>";
					text += value3;
					text += "</idInv>";
					text += "<signValue>";
					text += text3;
					text += "</signValue></Inv>";
				}
				text += "</Invoices>";
				string text4 = UtilsWSSignToken.publishInvWithToken(Account, ACpass, text, username, password, pattern, serial, linkWS);
				bool flag3 = !text4.Contains("OK");
				if (flag3)
				{
					VNPTEInvoiceSignToken.callThread(Account, ACpass, text2, username, password, pattern, linkWS);
				}
				result = text4;
			}
			catch (Exception ex)
			{
				result = "ERR:5 " + ex.Message;
			}
			return result;
		}

		private static void callWSRolBack(string Account, string ACpass, string id, string username, string pass, string pattern, string linkWS)
		{
			try
			{
				UtilsWSSignToken.rolBackWithToken(Account, ACpass, id, username, pass, pattern, linkWS);
			}
			catch (Exception var_0_15)
			{
			}
		}

		private static X509Certificate2 GetCertificateBySerial(string serial)
		{
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
			x509Store.Open(OpenFlags.ReadOnly);
			X509Certificate2Enumerator enumerator = x509Store.Certificates.GetEnumerator();
			X509Certificate2 result;
			while (enumerator.MoveNext())
			{
				X509Certificate2 current = enumerator.Current;
				bool flag = current.GetSerialNumberString().ToUpper().CompareTo(serial.ToUpper().Trim()) == 0;
				if (flag)
				{
					try
					{
						bool flag2 = !current.HasPrivateKey;
						if (flag2)
						{
							throw new Exception("Không lấy được private key, chọn chứng thư khác!");
						}
					}
					catch
					{
						throw new Exception("Không lấy được private key, chọn chứng thư khác!");
					}
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		private static string signHashCert(string hashValue, string serial)
		{
			string result;
			try
			{
				RSACryptoServiceProvider rSACryptoServiceProvider = null;
				X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Enumerator enumerator = x509Store.Certificates.GetEnumerator();
				while (enumerator.MoveNext())
				{
					X509Certificate2 current = enumerator.Current;
					bool flag = current.GetSerialNumberString().ToUpper() == serial.ToUpper();
					if (flag)
					{
						rSACryptoServiceProvider = (RSACryptoServiceProvider)current.PrivateKey;
						bool flag2 = rSACryptoServiceProvider == null;
						if (flag2)
						{
							result = "ERR:-2";
							return result;
						}
						break;
					}
				}
				string text = Convert.ToBase64String(rSACryptoServiceProvider.SignHash(Convert.FromBase64String(hashValue), CryptoConfig.MapNameToOID("SHA1")));
				x509Store.Close();
				result = text;
			}
			catch (Exception ex)
			{
				bool flag3 = ex.Message.Contains("cancelled by the user");
				if (flag3)
				{
					result = "ERR:-1";
				}
				else
				{
					result = "ERR:-3 " + ex;
				}
			}
			return result;
		}

		private static void callThread(string Account, string ACpass, string id, string username, string pass, string pattern, string linkWS)
		{
			Thread thread = new Thread(delegate(){VNPTEInvoiceSignToken.callWSRolBack(Account, ACpass, id, username, pass, pattern, linkWS);});
			thread.Start();
		}

		public static string importInv(string username, string pass, string xmlData, string linkWS)
		{
			string result;
			try
			{
				result = UtilsWSSignToken.importInv(username, pass, xmlData, linkWS);
			}
			catch (Exception ex)
			{
				result = "ERR: psd910 " + ex.Message;
			}
			return result;
		}

		public static string ImportAndPublishInv(string Account, string ACpass, string xmlInvData, string username, string pass, string pattern, string serial, string linkWS)
		{
			return UtilsWSSignToken.ImportAndPublishInv(Account, ACpass, xmlInvData, username, pass, pattern, serial, linkWS);
		}

		public static string ImportAndCreateBarcodeInv(string Account, string ACpass, string xmlInvData, string linkWS)
		{
			return UtilsWSSignToken.ImportAndCreateBarcodeInv(Account, ACpass, xmlInvData, linkWS);
		}

		public static string ConfirmPaymentDetail(string username, string pass, string invToken, string linkWS)
		{
			return UtilsWSSignToken.ConfirmPaymentDetail(username, pass, invToken, linkWS);
		}

		public static string SendAgainEmailServ(string Account, string ACpass, string username, string password, string xmlDataInvoiceEmail, string hdPattern, string Serial, string linkWS)
		{
			return UtilsWSSignToken.SendAgainEmailServ(Account, ACpass, username, password, xmlDataInvoiceEmail, hdPattern, Serial, linkWS);
		}
	}
}
