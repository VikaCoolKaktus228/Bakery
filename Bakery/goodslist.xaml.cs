using Bakery.editgoods;
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
    /// Логика взаимодействия для goodslist.xaml
    /// </summary>
    public partial class goodslist : Page
    {
        public goodslist()
        {
            InitializeComponent();
            List<GoodsBakery> bakerygoods = AppConect.bakerymod.GoodsBakery.ToList();
            bakeryproducts.ItemsSource = bakerygoods;


            List<GoodsBakery> bakgoods = AppConect.bakerymod.GoodsBakery.ToList();

            if (bakgoods.Count > 0)
            {
                tbCounter.Text = "Найдено " + bakgoods.Count + " товаров";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            bakeryproducts.ItemsSource = bakgoods;
            ComboSort.Items.Add("По уменьшению цены");
            ComboSort.Items.Add("По возрастанию цены");

            ComboFilter.Items.Add("вес от 0 до 100");
            ComboFilter.Items.Add("вес от 100 до 500");
            ComboFilter.Items.Add("вес от 500");
            ComboFilter.Items.Add("цена от 0 до 100");
            ComboFilter.Items.Add("цена от 100 до 500");
            ComboFilter.Items.Add("цена от 500 до 1000");
            ComboFilter.Items.Add("цена от 1000");
        }

        GoodsBakery[] findGoods()
        {
            List<GoodsBakery> product = AppConect.bakerymod.GoodsBakery.ToList();
            var productall = product;
            if (TextSearche != null)
            {
                product = product.Where(x => x.NameGoods.ToLower().Contains(TextSearche.Text.ToLower())).ToList();
            }

            if (ComboFilter.SelectedIndex >= 0)
            {
                switch (ComboFilter.SelectedIndex)
                {
                    case 0:
                        product = product.Where(x => x.Weight >= 0 && x.Weight <= 100).ToList();
                        break;
                    case 1:
                        product = product.Where(x => x.Weight >= 100 && x.Weight < 500).ToList();
                        break;
                    case 2:
                        product = product.Where(x => x.Weight > 500).ToList();
                        break;
                    case 3:
                        product = product.Where(x => x.Price >= 0 && x.Price <= 100).ToList();
                        break;
                    case 4:
                        product = product.Where(x => x.Price >= 100 && x.Weight < 500).ToList();
                        break;
                    case 5:
                        product = product.Where(x => x.Price >= 500 && x.Price < 1000).ToList();
                        break;
                    case 6:
                        product = product.Where(x => x.Price > 1000).ToList();
                        break;
                }
            }
            if (ComboSort.SelectedIndex >= 0)
            {
                switch (ComboSort.SelectedIndex)
                {
                    case 0:
                        product = product.OrderByDescending(x => x.Price).ToList<GoodsBakery>();
                        break;
                    case 1:
                        product = product.OrderBy(x => x.Price).ToList();
                        break;
                }
            }
            if (product.Count > 0)
            {
                tbCounter.Text = "Найдено " + product.Count + " товаров";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            bakeryproducts.ItemsSource = product;
            return product.ToArray();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            findGoods();
        }

        private void addgooods_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new AddEditgoods(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities7.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                bakeryproducts.ItemsSource = Entities7.GetContext().GoodsBakery.ToList();
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var goodsfordeleting = bakeryproducts.SelectedItems.Cast<GoodsBakery>().ToList();

            if(MessageBox.Show($"Вы точно хотите удалить следующие {goodsfordeleting.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entities7.GetContext().GoodsBakery.RemoveRange(goodsfordeleting);
                    Entities7.GetContext().SaveChanges();
                    MessageBox.Show("ДАННЫЕ УДАЛЕНЫ");

                    bakeryproducts.ItemsSource = Entities7.GetContext().GoodsBakery.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new AddEditgoods((sender as Button).DataContext as GoodsBakery));
        }

        private void userslistbutton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new UsersList());
        }

        private void eraseall_Click(object sender, RoutedEventArgs e)
        {
            TextSearche.Text = string.Empty;
            ComboFilter.SelectedIndex = -1;
            ComboSort.SelectedIndex = -1;
            findGoods();
        }
    }
}
