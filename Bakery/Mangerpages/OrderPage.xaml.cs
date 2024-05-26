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

namespace Bakery.Mangerpages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private Order currentorder = new Order();
        public OrderPage(Order selectedorder)
        {
            var curentorderid = selectedorder.Id;
            var curentorderuser = selectedorder.IdUser;
            var curentorderstatus = selectedorder.IdStatus;
            InitializeComponent();

            var moreorder = Entities7.GetContext().OrderManager
                               .Where(m => m.IdOrder == curentorderid)
                               .Select(m => m.IdGoods)
                               .ToList();
            var goodsInorder = Entities7.GetContext().GoodsBakery
                                         .Where(x => moreorder.Contains(x.Id))
                                         .ToList();
            listgoodsorder.ItemsSource = goodsInorder;

            if (selectedorder != null)
            {
                currentorder = selectedorder;
            }

            var userlogin = Entities7.GetContext().Users
                               .FirstOrDefault(s => s.Id == curentorderuser);
            labeluser.Content = userlogin.Login;

            labelId.Content = selectedorder.Id;

            var statusorder = Entities7.GetContext().Status
                               .FirstOrDefault(s => s.Id == curentorderstatus);

            orderstatuscombo.ItemsSource = Entities7.GetContext().Status.Select(x => x.Status1).ToList();

            DataContext = currentorder;

        }

        private void ToOrders_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new OrderManager());
        }

        private void changestatusbttn_Click(object sender, RoutedEventArgs e)
        {
            if (currentorder.Id != 0)
            {
                try
                {

                    currentorder.IdStatus = Convert.ToInt32(orderstatuscombo.SelectedIndex + 1);

                    Entities7.GetContext().SaveChanges();

                    MessageBox.Show("Статус успешно изменен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка при изменении статуса!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
