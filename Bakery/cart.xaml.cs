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

        private Users Userid = new Users();
        public cart()
        {
            InitializeComponent();
            //int userCart = Convert.ToInt32(App.Current.Properties["idUser"] = Userid.idUser);

            //List<orders> order = AppConect..orders.ToList();
            //ListOrders.ItemsSource = Entities.GetContext().orders
            //                               .Where(x => x.idUsers == Useriddd.idUser)
            //                               .Select(x => x.idGoods)
            //                               .ToList();
            ////order = order.Where(x => x.idUsers == 1).Select(x => x.idGoods).ToList();

            //if (order.Count > 0)
            //{
            //    tbCounter.Text = "Всего в корзине " + order.Count + " товаров";
            //}
            //else
            //{
            //    tbCounter.Text = "Ваша корзина пуста!";
            //}
        }
    }
}
