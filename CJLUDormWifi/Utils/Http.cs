using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CJLUDormWifi.Utils
{
    public class Http
    {

        public static string Post(string Url, string postDataStr = "")
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(Url);
                request.Method = "POST";
                request.ServerCertificateValidationCallback = (_s, _x509s, _x509c, _ssl) => { return (true); };
                ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 |
                                                        SecurityProtocolType.Tls |
                                                        SecurityProtocolType.Tls11 |
                                                        SecurityProtocolType.Tls12 |
                                                        (SecurityProtocolType)12288;
                // request.ContentType = "application/json";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Upgrade-Insecure-Requests", "1");
                request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36 Edg/110.0.1587.63");

                byte[] bt = Encoding.UTF8.GetBytes(postDataStr);
                string responseData = String.Empty;
                request.ContentLength = bt.Length;
                //GetRequestStream 输入流数据
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bt, 0, bt.Length);
                    reqStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return e.Message;
            }
        }

        public static string Get(string Url, string getDataStr = "")
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(Url + (getDataStr == "" ? "" : "?") + getDataStr);
                request.Method = "GET";
                request.ServerCertificateValidationCallback = (_s, _x509s, _x509c, _ssl) => { return (true); };
                ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 |
                                                        SecurityProtocolType.Tls |
                                                        SecurityProtocolType.Tls11 |
                                                        SecurityProtocolType.Tls12 |
                                                        (SecurityProtocolType)12288;
                // request.ContentType = "application/json";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return e.Message;
            }
        }
    }
}
