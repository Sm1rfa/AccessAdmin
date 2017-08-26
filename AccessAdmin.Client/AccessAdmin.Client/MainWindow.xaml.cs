using System.Windows;
using AccessAdmin.Client.BusinessLogic;
using AccessAdmin.Client.Windows;
using MahApps.Metro.Controls;

namespace AccessAdmin.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var winLogin = new LoginWindow();
            if (winLogin.ShowDialog() == true)
            {
            }
            else
            {
                this.Close();
            }

            var helper = new UserHelper();

            var admin = helper.IsUserAdmin(AdminHelper.AdminMail).Result;
            if (admin)
            {
                this.AdminTab.Visibility = Visibility.Visible;
            }
        }
    }
}
