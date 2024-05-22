using Aspose.BarCode.Generation;
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

namespace Bakery
{
    /// <summary>
    /// Логика взаимодействия для cart.xaml
    /// </summary>
    /// 
    public partial class cart : Page
    {

        public cart()
        {
            int idusercart = Convert.ToInt32(App.Current.Properties["Id"].ToString());
            InitializeComponent();

            var orderobj = Entities.GetContext().Order
                               .Where(x => x.IdUser == idusercart)
                               .Select(x => x.IdGoods)
                               .ToList();
            var goodsInCart = Entities.GetContext().GoodsBakery
                                         .Where(x => orderobj.Contains(x.Id))
                                         .ToList();

            cartbakery.ItemsSource = goodsInCart;
        }

        private void removecart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gobackbutton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new goodslistuser((sender as Button).DataContext as Users));
        }
        private void orderbutton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new OrderForm());

        }
    }
}
