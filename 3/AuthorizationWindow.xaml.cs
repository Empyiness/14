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

namespace _3
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            PasswordTextBox.Focus();
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (PasswordTextBox.Password != "1234")
            {
                e.Cancel = true;
                MessageBox.Show("Нет");
                PasswordTextBox.Focus();
                return;
            }
        }
    }
}
