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

        private Cart curcart = new Cart();

        public cart()
        {

            InitializeComponent();

            var cartItems = GetCartItems();
            cartbakery.ItemsSource = cartItems;
        }

        //int idusercart = Convert.ToInt32(App.Current.Properties["Id"].ToString());

        public List<GoodsBakery> GetCartItems()
        {
            int idusercart = Convert.ToInt32(App.Current.Properties["Id"].ToString());

            var userOrders = Entities.GetContext().Order
                                        .Where(x => x.IdUser == idusercart)
                                        .Select(x => x.Id)
                                        .ToList();

            var userCartItems = Entities.GetContext().Cart
                                       .Where(x => userOrders.Contains(x.Id))
                                       .Select(x => x.Id)
                                       .ToList();

            var goodsInCart = Entities.GetContext().GoodsBakery
                                     .Where(x => userCartItems.Contains(x.Id))
                                     .ToList();

            return goodsInCart;
        }

        private void gobackbutton_Click_1(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new goodslistuser((sender as Button).DataContext as Users));
        }

        private void removecart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
