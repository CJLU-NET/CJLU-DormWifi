using System;
using System.IO;
using System.Text;

namespace CJLUDormWifi.Utils
{
    public class Intranet
    {
        public static string portalUri = "https://portal1.cjlu.edu.cn:802/eportal/";
        public static string baseUri = "https://portal1.cjlu.edu.cn/";
        public static string getIPPage = "https://portal1.cjlu.edu.cn/drcom/chkstatus?callback=";

        public static bool Login(string username, string password)
        {
            string ip = GetIp();
            // File.WriteAllText("ip.txt", ip);
            string url ="https://portal1.cjlu.edu.cn:802/eportal/portal/login";

            string data = Http.Get(url + "?callback=&login_method=1&user_account=%2C0%2C" + username + "&user_password=" + password + "&wlan_user_ip=" + ip + "&wlan_user_ipv6=&wlan_user_mac=000000000000&wlan_ac_ip=&wlan_ac_name=1234567890&jsVersion=4.2.1&terminal_type=1&lang=zh-cn&v=5793&lang=zh");

            // File.WriteAllText("portal.txt", data);

            // File.WriteAllText("11.txt", Http.Get("https://portal2.cjlu.edu.cn/3.htm?wlanuserip=" + ip + "&wlanacip=192.168.8.1&wlanacname=me60&redirect=&session="));

            if (data.Contains("result\":1")) return true;

            return false;
        }

        private static string GetIp()
        {
            string data = Http.Get(getIPPage);
            string ip = Helper.GetMiddleStr(data, "v46ip\":\"", "\",").Replace("result\":0,\"m46\":0,\"v46ip\":\"", "");
            // File.WriteAllText("ip.txt", ip);
            return ip;
        }
        public static bool Ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒
            try
            {
                System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
