using Bakery.Mangerpages;
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

namespace Bakery.regauth
{
    /// <summary>
    /// Логика взаимодействия для authorizathion.xaml
    /// </summary>
    public partial class authorizathion : Page
    {
        public authorizathion()
        {
            InitializeComponent();

        }

        private void loginreg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var password = AppConect.bakerymod.Users.FirstOrDefault(x => x.Password == passwordauth.Password);
                if (password == null)
                {
                    MessageBox.Show("неверный пароль",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var user = AppConect.bakerymod.Users.FirstOrDefault(x => x.Login == loginauth.Text && x.Password == passwordauth.Password);
                if (user == null)
                {
                    MessageBox.Show("Такого пользователя не существует",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;

                }
                else
                {
                    switch (user.RoleId)
                    {
                        case 1:
                            MessageBox.Show("здраствйте админ",
                    "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.BakeryFrame.Navigate(new goodslist());
                            break;
                        case 2:
                            App.Current.Properties["Id"] = user.Id;
                            MessageBox.Show("здраствйте пользователь",
                    "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);  
                            AppFrame.BakeryFrame.Navigate(new goodslistuser((sender as Button).DataContext as Users));
                            break;
                        case 3:
                            MessageBox.Show("здраствйте менеджер",
                    "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.BakeryFrame.Navigate(new ManOrder());
                            break;
                        default:
                            MessageBox.Show("данные не обнаружены!",
                    "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString(),
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new registration());
        }

        private void loginauth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                passwordauth.Focus();
            }
        }
    }
}
