using System.Windows;
using System.Configuration;
using CJLUDormWifi.Utils;
using System.Diagnostics;
using System.Windows.Documents;

namespace CJLUDormWifi
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Intranet_User.Text = ConfigurationManager.AppSettings["Intranet-User"];
			Intranet_Pass.Password = ConfigurationManager.AppSettings["Intranet-Pass"];
            Wlan_User.Text = ConfigurationManager.AppSettings["Wlan-User"];
            Wlan_Pass.Password = ConfigurationManager.AppSettings["Wlan-Pass"];
        }

		private void Btn_Login_Click(object sender, RoutedEventArgs e)
		{
			Btn_Login.IsEnabled = false;
			Btn_Login.Content = "登录中";

			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings["Intranet-User"].Value = Intranet_User.Text;
			config.AppSettings.Settings["Intranet-Pass"].Value = Intranet_Pass.Password;
            config.AppSettings.Settings["Wlan-User"].Value = Wlan_User.Text;
            config.AppSettings.Settings["Wlan-Pass"].Value = Wlan_Pass.Password;

            config.Save();

			if (!Intranet.Ping("portal1.cjlu.edu.cn"))
            {
                MessageBox.Show("未连接至 CMCC-EDU", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                Btn_Login.IsEnabled = true;
                Btn_Login.Content = "登录";
            } else if (!Intranet.Login(Intranet_User.Text, Intranet_Pass.Password))
			{
                if (!Wlan.Login(Wlan_User.Text, Wlan_Pass.Password))
                {
                    MessageBox.Show("内网认证失败", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Btn_Login.IsEnabled = true;
                    Btn_Login.Content = "登录";
                } else
				{
                    MessageBox.Show("登录成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    Btn_Login.Content = "登录成功";
                }
            } else if (!Wlan.Login(Wlan_User.Text, Wlan_Pass.Password))
            {
                MessageBox.Show("内网认证成功，但移动Wlan认证失败", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                Btn_Login.IsEnabled = true;
                Btn_Login.Content = "登录";
            }
            else
			{
                MessageBox.Show("登录成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                Btn_Login.Content = "登录成功";
			}
		}

		private void Hyperlink_Click(object sender, RoutedEventArgs e)
		{
			Hyperlink link = sender as Hyperlink;

			if (link.NavigateUri.AbsoluteUri == "self://introduction/")
			{
				IntroWindow window = new IntroWindow();
				window.Show();
			}
			else if (link.NavigateUri.AbsoluteUri == "self://about/")
			{
                AboutWindow window = new AboutWindow();
                window.Show();
            }
			else
			{
				Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri) { UseShellExecute = true });
			}
			
		}
	}
}
