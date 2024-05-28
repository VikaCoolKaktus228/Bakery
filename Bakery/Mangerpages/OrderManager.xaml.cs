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
    /// Логика взаимодействия для OrderManager.xaml
    /// </summary>
    public partial class OrderManager : Page
    {
        public OrderManager()
        {
            InitializeComponent();
            List<Order> ordersman = AppConect.bakerymod.Order.ToList();
            orderlist.ItemsSource = ordersman;
        }

        private void changestatusbutton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void aboutOrder_Click_1(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new OrderPage((sender as Button).DataContext as Order));
        }
    }
}
