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
using Bakery.editgoods;

namespace Bakery.editgoods
{
    /// <summary>
    /// Логика взаимодействия для AddEditUsers.xaml
    /// </summary>
    public partial class AddEditUsers : Page
    {
        private Users curuser = new Users();
        public AddEditUsers(Users selecteduser)
        {
           
            InitializeComponent();
            if (selecteduser != null)
            {
                curuser = selecteduser;
            }

            combouserrole.ItemsSource = Entities9.GetContext().Role.ToList();

            DataContext = curuser;
        }

        private void edituserbutton_Click(object sender, RoutedEventArgs e)
        {

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
                    edituser();
                }
            }
            else
            {
                MessageBox.Show("Неверный формат почты",
                       "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }

        public void edituser()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(curuser.Name) || curuser.RoleId < 0 ||
                string.IsNullOrWhiteSpace(curuser.Phone)
                || string.IsNullOrWhiteSpace(curuser.Login) || string.IsNullOrWhiteSpace(curuser.Password)
                )
            {
                errors.AppendLine("заполните все данные");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (curuser.Id != 0)
            {
                try
                {
                    curuser.Name = nameuser.Text;
                    curuser.Phone = phoneuser.Text;
                    curuser.Email = emailuser.Text;
                    curuser.Login = loginuser.Text;
                    curuser.Password = passworduser.Text;
                    curuser.RoleId = Convert.ToInt32(combouserrole.SelectedIndex + 1);
                    Entities9.GetContext().SaveChanges();
                    MessageBox.Show("данные пользователя успешно измененны!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.BakeryFrame.Navigate(new UsersList());
                    //AppConect.bakerymod.Users.Add(curuser);
                }
                catch
                {
                    MessageBox.Show("Ошибка при изменении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

            private void backbttn_Click(object sender, RoutedEventArgs e)
            {
                AppFrame.BakeryFrame.Navigate(new UsersList());
            }

        private void loginuser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                combouserrole.Focus();
            }
        }

        private void combouserrole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                passworduser.Focus();
            }
        }

        private void passworduser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                emailuser.Focus();
            }
        }

        private void emailuser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                phoneuser.Focus();
            }
        }

        private void nameuser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                loginuser.Focus();
            }
        }

        private void phoneuser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!phoneuser.Text.StartsWith("+"))
            {
                phoneuser.TextChanged -= phoneuser_TextChanged;
                var currentText = phoneuser.Text;
                phoneuser.Text = "+" + currentText.TrimStart('+');

                phoneuser.TextChanged += phoneuser_TextChanged;

                phoneuser.SelectionStart = phoneuser.Text.Length;
            }
        }
    }
} 
