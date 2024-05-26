using Bakery.editgoods;
using Bakery.regauth;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConect.bakerymod = new Entities7();
            AppFrame.BakeryFrame = BakFrame;

            BakFrame.Navigate(new authorizathion());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(App.Current.Properties["Id"]);
            if(userId == 1)
            {
                var order = Entities7.GetContext().Order.FirstOrDefault(o => o.IdUser == userId && o.IdStatus == 2);

                var cartItems = Entities7.GetContext().Cart.Where(c => c.IdOrder == order.Id).ToList();
                Entities7.GetContext().Cart.RemoveRange(cartItems);
                Entities7.GetContext().SaveChanges();
            }

        }
    }
}
