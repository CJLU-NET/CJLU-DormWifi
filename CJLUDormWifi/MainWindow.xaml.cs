using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

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



            ConfigurationManager.AppSettings["ServerIP"];
            ConfigurationManager.AppSettings["ServerIP"];
            ConfigurationManager.AppSettings["ServerIP"];
            ConfigurationManager.AppSettings["ServerIP"];
        }

        private void Btn_Login_Click(object sender, RoutedEventArgs e)
        {
            Btn_Login.IsEnabled = false;
            Btn_Login.Content = "登录中";
        }
    }
}
