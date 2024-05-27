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

namespace Bakery.editgoods
{
    /// <summary>
    /// Логика взаимодействия для EditUsers.xaml
    /// </summary>
    public partial class UsersList : Page
    {
        public UsersList()
        {
            InitializeComponent();
            List<Users> usersli = AppConect.bakerymod.Users.ToList();
            userlist.ItemsSource = usersli;

           //List<Users> userslist = AppConect.bakerymod.Users.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities7.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                userlist.ItemsSource = Entities7.GetContext().Users.ToList();
            }
        }

        private void changeuser_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new AddEditUsers((sender as Button).DataContext as Users));
        }

        private void deleteuser_Click(object sender, RoutedEventArgs e)
        {
            var usersfordeleting = userlist.SelectedItems.Cast<Users>().ToList();

            if(usersfordeleting.Count > 0)
            {
                if (MessageBox.Show($"Вы точно хотите удалить следующие {usersfordeleting.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Entities7.GetContext().Users.RemoveRange(usersfordeleting);
                        Entities7.GetContext().SaveChanges();
                        MessageBox.Show("пользователь удален");

                        userlist.ItemsSource = Entities7.GetContext().Users.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("выберите пользователя!");
            }
            
        }

        private void adduserbttn_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new AddUser());
        }
    }
}
