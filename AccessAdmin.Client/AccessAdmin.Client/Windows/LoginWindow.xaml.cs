using System;
using System.Windows;
using AccessAdmin.Client.BusinessLogic;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AccessAdmin.Client.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var helper = new UserHelper();
            try
            {
                var validUser = helper.AuthenticateUser(this.MailBox.Text, this.PasswordBox.Text).Result;
                if (validUser != 1)
                {
                    await this.ShowMessageAsync("Error", "You are not a user of the system!");
                    return;
                }

                AdminHelper.UserMail = this.MailBox.Text;

                this.DialogResult = true;
            }
            catch (Exception)
            {
                await this.ShowMessageAsync("Error", "You are not authenticated!");
            }
        }
    }
}
