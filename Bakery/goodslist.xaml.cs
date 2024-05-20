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
            AppFrame.BakeryFrame.Navigate(new AddEditgoods());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var goodsfordeleting = bakeryproducts.SelectedItems.Cast<GoodsBakery>().ToList();

            if(MessageBox.Show($"Вы точно хотите удалить следующие {goodsfordeleting.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entities.GetContext().GoodsBakery.RemoveRange(goodsfordeleting);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("ДАННЫЕ УДАЛЕНЫ");

                    bakeryproducts.ItemsSource = Entities.GetContext().GoodsBakery.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            //AppFrame.BakeryFrame.Navigate(new AddEditgoods(sender as Button).DataContext as GoodsBakery);
        }
    }
}
