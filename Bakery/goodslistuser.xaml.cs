﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
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
    /// Логика взаимодействия для goodslistuser.xaml
    /// </summary>
    public partial class goodslistuser : Page
    {
        private Users curuser = new Users();
        public goodslistuser(Users user)
        {
            InitializeComponent();
            List<GoodsBakery> bakerygoods = AppConect.bakerymod.GoodsBakery.ToList();
            userbakeryproducts.ItemsSource = bakerygoods;
            DataContext = curuser;


        List<GoodsBakery> bakgoods = AppConect.bakerymod.GoodsBakery.ToList();

            if (bakgoods.Count > 0)
            {
                tbCounter.Text = "Найдено " + bakgoods.Count + " товаров";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            userbakeryproducts.ItemsSource = bakgoods;
            ComboSort.Items.Add("По уменьшению цены");
            ComboSort.Items.Add("По возрастанию цены");

            ComboFilter.Items.Add("вес от 0 до 100");
            ComboFilter.Items.Add("вес от 100 до 500");
            ComboFilter.Items.Add("вес от 500");

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
            userbakeryproducts.ItemsSource = product;
            return product.ToArray();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            findGoods();
        }

        private void cart_Click(object sender, RoutedEventArgs e)
        {
            var goodsforcart = userbakeryproducts.SelectedItems.Cast<GoodsBakery>().ToList();
            if (goodsforcart.Count > 0)
            {

                try
                {

                    var button = sender as Button;
                    int selectg = Convert.ToInt32(button.Tag);
                    int idUsers = Convert.ToInt32(App.Current.Properties["Id"].ToString());
                    int selectedGoodsId = ((GoodsBakery)userbakeryproducts.SelectedItem).Id;


                        var order = Entities9.GetContext().Order.FirstOrDefault(o => o.IdUser == idUsers);
                        if (order == null)
                        {
                            order = new Order()
                            {
                                IdUser = idUsers,
                                IdStatus = 2
                            };
                            Entities9.GetContext().Order.Add(order);
                            Entities9.GetContext().SaveChanges();
                        }

                        var cartnew = new Cart()
                        {
                            IdOrder = order.Id,
                            IdGoods = selectedGoodsId,
                            Cost = +1
                        };

                        Entities9.GetContext().Cart.Add(cartnew);
                        Entities9.GetContext().SaveChanges();

                        MessageBox.Show("Товар успешно добавлен в корзину!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                        AppFrame.BakeryFrame.Navigate(new cart());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при добавлении товара в корзину: " + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите товар из списка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void tocartbttn_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new cart());

        }

        private void erasebutton_Click(object sender, RoutedEventArgs e)
        {
            TextSearche.Text = string.Empty;
            ComboFilter.SelectedIndex = -1;
            ComboSort.SelectedIndex = -1;
            findGoods();
        }

        private void userbakeryproducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            findGoods();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            findGoods();
        }

        private void TextSearche_KeyDown(object sender, KeyEventArgs e)
        {
            List<GoodsBakery> product = AppConect.bakerymod.GoodsBakery.ToList();
            var productall = product;
            if (TextSearche != null)
            {
                product = product.Where(x => x.NameGoods.ToLower().Contains(TextSearche.Text.ToLower())).ToList();
            }
            findGoods();
        }

        private void erasesortbutton_Click(object sender, RoutedEventArgs e)
        {
            ComboSort.SelectedIndex = -1;
        }

        private void erasefiltrbutton_Click(object sender, RoutedEventArgs e)
        {
            ComboFilter.SelectedIndex = -1;
        }

        private void erasesearchebutton_Click(object sender, RoutedEventArgs e)
        {
            TextSearche.Text = string.Empty;
        }
    }
}
