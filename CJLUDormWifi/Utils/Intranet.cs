namespace CJLUDormWifi.Utils
{
    public class Intranet
    {
        public static string portalUri = "https://portal1.cjlu.edu.cn:801/eportal/";
        public static string baseUri = "https://portal1.cjlu.edu.cn/";
        public static string loginPage = "https://portal1.cjlu.edu.cn/a38.htm";
        public static string resultPage = "https://portal1.cjlu.edu.cn/3.htm";
        public static string getIPPage = "https://portal1.cjlu.edu.cn/a79.htm";

        public static bool Login(string username, string password, bool? connect = true)
        {
            string ip = GetIp();
            // File.WriteAllText("ip.txt", ip);
            string para_get = "c=ACSetting&a=Login&wlanuserip=" + ip + "&wlanacip=null&wlanacname=1234567890&port=&iTermType=1&mac=000000000000&ip=" + ip + "&redirect=null";
            string url = portalUri + "?" + para_get;

            string user = username;
            string _user = user;

            if ((bool) connect)
            {
                user = "__" + username;
            }

            string para_post = "DDDDD=" + user + "&upass=" + password + "&R1=0&R2=&R6=0&para=00&0MKKey=123456";

            string data = Http.Get(resultPage, "wlanuserip=" + ip + "&wlanacip=null&wlanacname=1234567890&port=&iTermType=1&mac=000000000000&ip=" + ip + "&redirect=null");
            /*string uid = Helper.GetMiddleStr(data, "UID='", "';").Trim();

            if (uid == _user) return true;*/

            data = Http.Post(url, para_post);
            /* uid = Helper.GetMiddleStr(data, "UID='", "';").Trim();

             if (uid == _user) return true;
             File.WriteAllText("1.txt", data);*/

            // File.WriteAllText("11.txt", Http.Get("https://portal2.cjlu.edu.cn/3.htm?wlanuserip=" + ip + "&wlanacip=192.168.8.1&wlanacname=me60&redirect=&session="));

            if (data.Contains("用户登录成功")) return true;

            return false;
        }

        private static string GetIp()
        {
            string data = Http.Get(getIPPage);
            string ip = Helper.GetMiddleStr(data, ";v46ip='", "' ");
            // File.WriteAllText("ip.txt", ip);
            return ip;
        }

        
    }
}
