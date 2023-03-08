using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HandyControl.Controls;

namespace CJLUDormWifi.Utils
{
	class Wlan
	{
		public static string getLoginPage()
		{
			string page = "http://edge-http.microsoft.com/captiveportal/generate_204";
			string location = "";
			WebRequest req = WebRequest.Create(page);
			req.ContentType = "text/html";
			req.Method = "GET";
			req.Headers.Add("Upgrade-Insecure-Requests", "1");
			req.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36 Edg/110.0.1587.63");

			try
			{
				location = req.GetResponse().ResponseUri.AbsoluteUri;

			} catch(Exception ex)
			{

				MessageBox.Show(ex.Message);
			}

			if (location == page ) return ""; 
			return location;
		}

		public static bool Login(string phone, string pass) {
			string page = getLoginPage();
			if (page.Contains("portal1.cjlu.edu.cn")) return false;

			string base_uri = "";

			if (page.Contains(":7080")) base_uri = page.Split(":7080")[0] + ":7080/zmcc/";
			else base_uri = page.Split(":7090")[0] + ":7090/zmcc/";

			string ac_name = "";
			string ac_ip = "";
			string user_ip = "";

			string[] split = page.Substring(page.IndexOf("?") + 1).Split("&");

			for (int i = 0; i < split.Length; i++)
			{
				string str = split[i];
				if (str.StartsWith("wlanuserip="))
				{
					user_ip = str.Split("=")[1];
				}
				if (str.StartsWith("wlanacname="))
				{
					ac_name = str.Split("=")[1];
				}
				if (str.StartsWith("wlanacip="))
				{
					ac_ip = str.Split("=")[1];
				}
			}

			string pwdRSA = System.Web.HttpUtility.UrlEncode(RSA.Encrypt(pass), System.Text.Encoding.UTF8);
			// pwdRSA = "QRyhG93ns29Qe6FIdWVVAaTzvOB%2BOUekYQE1mld5pGRZ3mUunirovRACLBVAd43cpUp%2FU8ZyYQQxzllE5n76GW23c%2FBJ3%2BQVmMy8C%2F8U6jLMArVrMCggkMeqjDLhhV9jhLcjgf69gNM93StJF%2Bn29YV283QXCbjT3QCt5Ouwh8s%3D";


            string para_post = "wlanAcName=" + ac_name + "&wlanAcIp=" + ac_ip + "&wlanUserIp=" + user_ip + "&ssid=&userName=" + phone + "&_userPwd=%E8%BE%93%E5%85%A5%E5%9B%BA%E5%AE%9A%E5%AF%86%E7%A0%81%2F%E4%B8%B4%E6%97%B6%E5%AF%86%E7%A0%81&userPwd=" + pwdRSA + "&verifyCode=&verifyHidden=&issaveinfo=&passType=0";

			TimeSpan mTimeSpan = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0);
			string timestamp = mTimeSpan.TotalSeconds.ToString();

			string url = base_uri + "portalLogin.wlan?" + timestamp;

			string res = Http.Post(url, para_post);
			File.WriteAllText("cmcc.txt", res);

			if (res.Contains("alert")) return false;

			return true;
		}
	}
}
