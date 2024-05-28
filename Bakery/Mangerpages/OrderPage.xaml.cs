using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using static iTextSharp.text.pdf.AcroFields;

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
            var statusOrder = Entities9.GetContext().Status.FirstOrDefault(s => s.Order.Any(o => o.Id == curentorderid));
            InitializeComponent();

            var moreorder = Entities9.GetContext().OrderManager
                               .Where(m => m.IdOrder == curentorderid)
                               .Select(m => m.IdGoods)
                               .ToList();
            var goodsInorder = Entities9.GetContext().GoodsBakery
                                         .Where(x => moreorder.Contains(x.Id))
                                         .ToList();
            listgoodsorder.ItemsSource = goodsInorder;

            if (selectedorder != null)
            {
                currentorder = selectedorder;
            }

            var userlogin = Entities9.GetContext().Users
                               .FirstOrDefault(s => s.Id == curentorderuser);
            labeluser.Content = userlogin.Login;

            labelId.Content = selectedorder.Id;

            var statusorder = Entities9.GetContext().Status
                               .FirstOrDefault(s => s.IdStatuss == curentorderstatus);

            orderstatuscombo.ItemsSource = Entities9.GetContext().Status.ToList();

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
                    int Id = currentorder.Id;
                    var StatusToUpdate = AppConect.bakerymod.OrderManager.FirstOrDefault(u => u.IdOrder == Id);
                    if (orderstatuscombo.SelectedItem != null)
                    {
                        StatusToUpdate.Order.IdStatus = Convert.ToInt32(orderstatuscombo.SelectedIndex + 1);

                        AppConect.bakerymod.SaveChanges();
                        MessageBox.Show("Статус заказа успешно изменен!",
                            "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("выберите статус!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка при изменении статуса!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void orderstatuscombo_DropDownOpened(object sender, EventArgs e)
        {
            changestatusbttn.IsEnabled = true;
        }
    }
}
