using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Bakery.editgoods
{
    /// <summary>
    /// Логика взаимодействия для AddEditgoods.xaml
    /// </summary>
    public partial class AddEditgoods : Page
    {
        private GoodsBakery curbakery = new GoodsBakery();
        public AddEditgoods(GoodsBakery selectedgood)
        {
            InitializeComponent();
            if (selectedgood != null)
            {
                curbakery = selectedgood;
            }

            goodscategoryy.ItemsSource = Entities9.GetContext().Category.ToList();
            goodsallergienss.ItemsSource = Entities9.GetContext().Allergens.ToList();
            goodsproviderr.ItemsSource = Entities9.GetContext().Provider.ToList();
            goodstypee.ItemsSource = Entities9.GetContext().TypeOfGoods.ToList();

            DataContext = curbakery;


        }

        public void AddNewGoods()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(curbakery.NameGoods) || curbakery.CategoryId < 0 || curbakery.Weight < 0
                || curbakery.TypeOfGoodsId < 0 || string.IsNullOrWhiteSpace(curbakery.CallorieValue) || curbakery.AllergensId < 0
                || string.IsNullOrWhiteSpace(curbakery.Description) || curbakery.Price < 0
                || curbakery.ProviderId < 0 || curbakery.OnStock <= 0)
            {
                errors.AppendLine("Заполните все поля");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;

            }
            try
            {
                curbakery = new GoodsBakery()
                {
                    NameGoods = goodsnamee.Text,
                    Price = Convert.ToInt32(price.Text),
                    TypeOfGoodsId = Convert.ToInt32(goodstypee.SelectedIndex + 1),
                    ProviderId = Convert.ToInt32(goodsproviderr.SelectedIndex + 1),
                    CategoryId = Convert.ToInt32(goodscategoryy.SelectedIndex + 1),
                    Weight = Convert.ToInt32(goodsweightt.Text),
                    OnStock = Convert.ToInt32(onstock.Text),
                    Description = description.Text,
                    Image = null,
                    CallorieValue = goodscalloriess.Text,
                    AllergensId = Convert.ToInt32(goodsallergienss.SelectedIndex + 1)
                };

                AppConect.bakerymod.GoodsBakery.Add(curbakery);
                AppConect.bakerymod.SaveChanges();

                MessageBox.Show("Товар успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.BakeryFrame.Navigate(new goodslist());
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateGoods()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(curbakery.NameGoods) || curbakery.CategoryId < 0 || curbakery.Weight < 0
                || curbakery.TypeOfGoodsId < 0 || string.IsNullOrWhiteSpace(curbakery.CallorieValue) || curbakery.AllergensId < 0
                || string.IsNullOrWhiteSpace(curbakery.Description) || curbakery.Price < 0
                || curbakery.ProviderId < 0 || curbakery.OnStock <= 0)
            {
                errors.AppendLine("Заполните все поля");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;

            }
            if(curbakery.Id > 0)
            {
                try
                {
                    curbakery.NameGoods = goodsnamee.Text;
                    curbakery.Price = Convert.ToInt32(price.Text);
                    curbakery.TypeOfGoodsId = Convert.ToInt32(goodstypee.SelectedIndex + 1);
                    curbakery.ProviderId = Convert.ToInt32(goodsproviderr.SelectedIndex + 1);
                    curbakery.CategoryId = Convert.ToInt32(goodscategoryy.SelectedIndex + 1);
                    curbakery.Weight = Convert.ToInt32(goodsweightt.Text);
                    curbakery.OnStock = Convert.ToInt32(onstock.Text);
                    curbakery.Description = description.Text;
                    curbakery.CallorieValue = goodscalloriess.Text;
                    curbakery.AllergensId = Convert.ToInt32(goodsallergienss.SelectedIndex + 1);
                    curbakery.Image = curbakery.Image;


                    Entities9.GetContext().SaveChanges();

                    MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.BakeryFrame.Navigate(new goodslist());
                }
                catch
                {
                    MessageBox.Show("Ошибка при изменении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
                
        }
        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            if (curbakery.Id == 0)
            {
                AddNewGoods();
            }
            else
            {
                UpdateGoods();
            }
        }

        private void goodsweightt_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                goodscalloriess.Focus();
            }

        }

        private void onstock_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                description.Focus();
            }
        }

        private void price_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        private void gobackbttn_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new goodslist());
        }

        private void goodsnamee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                goodscategoryy.Focus();
            }
        }

        private void goodscategoryy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                goodsweightt.Focus();
            }
        }

        private void goodscalloriess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                goodsproviderr.Focus();
            }
        }

        private void goodsproviderr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                goodstypee.Focus();
            }
        }

        private void goodstypee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                goodsallergienss.Focus();
            }
        }

        private void goodsallergienss_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                onstock.Focus();
            }
        }

        private void description_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                price.Focus();
            }
        }
    }
}
