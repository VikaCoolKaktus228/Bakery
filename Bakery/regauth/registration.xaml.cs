using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Bakery.regauth
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Page
    {
        public registration()
        {
            InitializeComponent();
            emailreg.MaxLength = 100;
            phonereg.MaxLength = 15;
            loginreg.MaxLength = 50;
            tbpasswordreg.MaxLength = 20;
            repeatpasswordreg.MaxLength = 20;
        }

        private void repeatpasswordreg_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tbpasswordreg.Text == "" || tbpasswordreg.Text == " ")
            {
                if (repeatpasswordreg.Password != tbpasswordreg.Text)
                {

                    regbutton.IsEnabled = false;
                }
                
            }
            else { regbutton.IsEnabled = true; }
        }

        private void regbutton_Click(object sender, RoutedEventArgs e)
        {
            if (AppConect.bakerymod.Users.Count(x => x.Login == loginreg.Text) > 0)
            {
                MessageBox.Show("такой пользователь уже существует",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }



            if (emailreg.Text.Contains("@") && emailreg.Text.Contains("."))
            {
                if (phonereg.Text.Length < 10 || !phonereg.Text.Contains("+") || phonereg.Text.Length > 15 )
                {
                    MessageBox.Show("Неверный формат телефона",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (tbpasswordreg.Text == "" || tbpasswordreg.Text == " ")
                {
                    MessageBox.Show("введите пароль",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (repeatpasswordreg.Password != tbpasswordreg.Text)
                {
                    MessageBox.Show("пароли не совпадают",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    registr();
                }
            }
            else
            {
                MessageBox.Show("Неверный формат почты",
                       "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            
        }

        public void registr()
        {
            try
            {
                Users user = new Users()
                {
                    Login = loginreg.Text,
                    Name = namereg.Text,
                    Email = emailreg.Text,
                    Phone = phonereg.Text,
                    Password = tbpasswordreg.Text,
                    RoleId = 2
                };
                AppConect.bakerymod.Users.Add(user);
                AppConect.bakerymod.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.BakeryFrame.Navigate(new authorizathion());
            }
            catch
            {
                MessageBox.Show("Ошибка добавления данных",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void emailreg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void regbut_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new authorizathion());
        }

        private void phonereg_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9 ) && e.Key != Key.Back && e.Key !=Key.OemPlus)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                namereg.Focus();
            }
        }

        private void namereg_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.A || e.Key > Key.Z) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                loginreg.Focus();
            }
        }

        private void emailreg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                phonereg.Focus();
            }
        }

        private void loginreg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tbpasswordreg.Focus();
            }
        }

        private void tbpasswordreg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                repeatpasswordreg.Focus();
            }
        }
    }
}
