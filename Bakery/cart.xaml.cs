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

            InitializeComponent();

            //ListOrders.ItemsSource = Entities.GetContext().orders
                               //.Where(x => x.idUsers == Useriddd.idUser)
                               //.Select(x => x.idGoods)
                               //.ToList();
        }

        //int idusercart = Convert.ToInt32(App.Current.Properties["Id"].ToString());


        private void removecart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gobackbutton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new goodslistuser((sender as Button).DataContext as Users));
        }
    }
}
