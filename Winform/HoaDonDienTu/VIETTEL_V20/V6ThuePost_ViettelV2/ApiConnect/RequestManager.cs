﻿using System;
using System.IO;
using System.Net;
using System.Text;

namespace V6ThuePostViettelV2Api
{
    public class RequestManager
    {
        public string LastResponse { protected set; get; }

        CookieContainer cookies = new CookieContainer();

        internal string GetCookieValue(Uri SiteUri,string name)
        {
            Cookie cookie = cookies.GetCookies(SiteUri)[name];
            return (cookie == null) ? null : cookie.Value;
        }

        public string GetResponseContent(HttpWebResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }
            Stream dataStream = null;
            StreamReader reader = null;
            string responseFromServer = null;

            try
            {
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Cleanup the streams and the response.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {                
                if (reader != null)
                {
                    reader.Close();
                }
                if (dataStream != null)
                {
                    dataStream.Close();
                }
                response.Close();
            }
            LastResponse = responseFromServer;
            return responseFromServer;
        }

        public HttpWebResponse SendPOSTRequest(string uri, string content, string username, string password, bool allowAutoRedirect)
        {
            HttpWebRequest request = GeneratePOSTRequest(uri, content, username, password, allowAutoRedirect);
            return GetResponse(request);
        }

        public HttpWebResponse SendGETRequest(string uri, string username, string password, bool allowAutoRedirect)
        {
            HttpWebRequest request = GenerateGETRequest(uri, username, password, allowAutoRedirect);
            return GetResponse(request);
        }

        public HttpWebResponse SendRequest(string uri, string content, string method, string username, string password, bool allowAutoRedirect)
        {
            HttpWebRequest request = GenerateRequest(uri, content, method, username, password, allowAutoRedirect);
            return GetResponse(request);
        }

        public HttpWebRequest GenerateGETRequest(string uri, string username, string password, bool allowAutoRedirect)
        {
            return GenerateRequest(uri, null, "GET", username, password, allowAutoRedirect);
        }

        internal HttpWebRequest GeneratePOSTRequest(string uri, string content, string username, string password, bool allowAutoRedirect)
        {
            return GenerateRequest(uri, content, "POST", username, password, allowAutoRedirect);
        }

        internal HttpWebRequest GenerateRequest(string uri, string content, string method, string username, string password, bool allowAutoRedirect)
        {
            try
            {
                if (uri == null)
                {
                    throw new ArgumentNullException("uri");
                }

                method = method != null ? method.ToUpper() : "";

                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

                // Set the Method property of the request to POST.
                request.Method = method;
                // Set cookie container to maintain cookies
                request.CookieContainer = cookies;
                request.AllowAutoRedirect = allowAutoRedirect;
                // If username is empty use defaul credentials
                if (string.IsNullOrEmpty(username))
                {
                    if (string.IsNullOrEmpty(password))
                    {
                        request.Credentials = CredentialCache.DefaultNetworkCredentials;
                    }
                    else
                    {
                        request.Headers.Add("Authorization", "Bearer " + password); // Lúc này password là Token.
                    }
                }
                else
                {
                    var encoded = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + password));
                    request.Headers.Add("Authorization", "Basic " + encoded);
                    //request.Credentials = new NetworkCredential(username, password);
                }

                if (method == "POST")
                {
                    // Convert POST data to a byte array.
                    request.KeepAlive = true;
                    //request.UserAgent =	"Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36";
                    //request.UserAgent =	"PostmanRuntime/7.26.5";
                    //request.UserAgent =	"Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36 PostmanRuntime/7.26.5";
                    //request.Accept = "*/*";
                    //request.Headers.Add("Origin", "chrome-extension://aejoelaoggembcahagimdiliamlcdmfm");
                    //request.Headers.Add("Sec-Fetch-Site", "none");
                    //request.Headers.Add("Sec-Fetch-Mode", "cors");
                    //request.Headers.Add("Sec-Fetch-Dest", "empty");
                    //request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                    //request.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
                    request.SendChunked = true; // request.TransferEncoding = "chunked";
                    
                    byte[] byteArray = Encoding.UTF8.GetBytes(content);
                    request.ContentLength = byteArray.Length;

                    // Set the ContentType property of the WebRequest.
                    if (uri.ToLower().EndsWith("InvoiceAPI/InvoiceWS/cancelTransactionInvoice".ToLower()))
                    {
                        request.ContentType = "application/x-www-form-urlencoded";
                    }
                    else
                    {
                        request.ContentType = "application/json";//";charset=UTF-8";
                        //request.MediaType = "application/json";
                        //request.Headers.Add("X-XSS-Protection", "1; mode=block");
                    }                    
                    //request.Accept = "application/json";
                    
                    //request.TransferEncoding = "gzip, deflate, br";
                    
                    // Set the ContentLength property of the WebRequest.
                    

                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36";
                    // Get the request stream.
                    Stream dataStream = request.GetRequestStream();

                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    dataStream.Close();
                }
                return request;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string POSTM()
        //{
        //    string AccessToken = lblToken.Text;

        //    HttpClient tRequest = new HttpClient();
        //    tRequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

        //    Task<HttpResponseMessage> getTask = tRequest.PostAsJsonAsync(new Uri(strURL).ToString(), TestMaster);

        //    HttpResponseMessage urlContents = await getTask;

        //    Console.WriteLine("urlContents.ToString");
        //    lblEDDR.Text = urlContents.ToString();
        //}

        internal HttpWebResponse GetResponse(HttpWebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();                
                cookies.Add(response.Cookies);                
                // Print the properties of each cookie.
                Console.WriteLine("\nCookies: ");
                foreach (Cookie cook in cookies.GetCookies(request.RequestUri))
                {
                    Console.WriteLine("Domain: {0}, String: {1}", cook.Domain, cook.ToString());
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Web exception occurred. Status code: {0}", ex.Status);
                return (HttpWebResponse) ex.Response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

    }
}