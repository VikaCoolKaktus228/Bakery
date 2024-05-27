using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Bakery.editgoods
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        private Users curuser = new Users();
        public AddUser()
        {
            InitializeComponent();
            DataContext = curuser;
            combouserrole.ItemsSource = Entities7.GetContext().Role.Select(x => x.Role1).ToList();
        }



    private void adduserbutton_Click(object sender, RoutedEventArgs e)
        {
            if (AppConect.bakerymod.Users.Count(x => x.Login == loginuser.Text) > 0)
            {
                MessageBox.Show("такой пользователь уже существует",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (emailuser.Text.Contains("@"))
            {
                if (phoneuser.Text.Length < 10 || !phoneuser.Text.Contains("+") || phoneuser.Text.Length > 15)
                {
                    MessageBox.Show("Неверный формат телефона",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    adduser();
                }
            }
            else
            {
                MessageBox.Show("Неверный формат почты",
                       "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        public void adduser()
        {
            try
            {
                curuser = new Users()
                {
                    Name = nameuser.Text,
                    Phone = phoneuser.Text,
                    Email = emailuser.Text,
                    Password = passworduser.Text,
                    Login = loginuser.Text,
                    Role = Convert.ToInt32(combouserrole.SelectedIndex + 1)
                };

                AppConect.bakerymod.Users.Add(curuser);
                AppConect.bakerymod.SaveChanges();

                MessageBox.Show("Товар успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.BakeryFrame.Navigate(new goodslist());
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void phoneuser_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9 || e.Key == Key.OemPlus || e.Key == Key.Add) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        private void backbttn_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new UsersList());
        }
    }
}
