using Microsoft.Extensions.Configuration;
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
using System.Windows.Shapes;

namespace StudentManagementWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Password))
            {
                MessageBox.Show("Please input all fields", "Login");
                return;
            }

            if (isValidLogin(txtEmail.Text, txtPass.Password))
            {
                MainWindow adminWindow = new MainWindow();
                adminWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Login failed! Email or password incorrect", "Login");
            }
        }

        private bool isValidLogin(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            var adminEmail = config["Admin:Email"];
            var adminPass = config["Admin:Password"];
            if (adminEmail == email && adminPass == password)
            {
                return true;
            }
            return false;
        }
    }
}
