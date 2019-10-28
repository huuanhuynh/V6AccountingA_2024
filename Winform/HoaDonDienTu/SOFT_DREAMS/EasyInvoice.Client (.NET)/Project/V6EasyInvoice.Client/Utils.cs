using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EasyInvoice.Json.Linq;

namespace V6EasyInvoice.Client
{
	public static class Utils
	{
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			foreach (T current in source)
			{
				action(current);
			}
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			int num = 0;
			foreach (T current in source)
			{
				action(current, num++);
			}
		}

		public static bool IsNullOrWhiteSpace(this string value)
		{
			if (value == null)
			{
				return true;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static byte[] ReadAsBytes(this Stream input)
		{
			byte[] array = new byte[16384];
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int count;
				while ((count = input.Read(array, 0, array.Length)) > 0)
				{
					memoryStream.Write(array, 0, count);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		public static string AsString(byte[] buffer)
		{
			if (buffer == null)
			{
				return "";
			}
			return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
		}

		public static string AsString(Stream stream)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		public static string UrlEncode(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length <= 32766)
			{
				return Uri.EscapeDataString(input);
			}
			StringBuilder stringBuilder = new StringBuilder(input.Length * 2);
			string text;
			for (int i = 0; i < input.Length; i += text.Length)
			{
				int length = Math.Min(input.Length - i, 32766);
				text = input.Substring(i, length);
				stringBuilder.Append(Uri.EscapeDataString(text));
			}
			return stringBuilder.ToString();
		}

		public static string MergeBaseUrlAndResource(Uri baseUrl, string resource)
		{
			string text = resource;
			if (!text.IsNullOrWhiteSpace() && text.StartsWith("/"))
			{
				text = text.Substring(1);
			}
			if (baseUrl == null || baseUrl.AbsoluteUri.IsNullOrWhiteSpace())
			{
				return text;
			}
			Uri baseUri = baseUrl;
			if (!baseUrl.AbsoluteUri.EndsWith("/") && !text.IsNullOrWhiteSpace())
			{
				baseUri = new Uri(baseUrl.AbsoluteUri + "/");
			}
			return new Uri(baseUri, text).AbsoluteUri;
		}

		public static bool IsValidJson(string value)
		{
			bool result;
			try
			{
				JToken.Parse(value);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
