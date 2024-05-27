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
    }
}
