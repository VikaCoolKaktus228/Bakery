﻿using System;
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
            goodscategoryy.ItemsSource = Entities.GetContext().Category.Select(x => x.Category1).ToList();
            goodsallergienss.ItemsSource = Entities.GetContext().Allergens.Select(x => x.Allergen).ToList();
            goodsproviderr.ItemsSource = Entities.GetContext().Provider.Select(x => x.Provider1).ToList();
            goodstypee.ItemsSource = Entities.GetContext().TypeOfGoods.Select(x => x.Type).ToList();

            DataContext = curbakery;


        }

        public void AddNewGoods()
        {
            try
            {
                curbakery = new GoodsBakery()
                {
                    NameGoods = goodsnamee.Text,
                    Price = Convert.ToInt32(price.Text),
                    TypeOfGoods = Convert.ToInt32(goodstypee.SelectedIndex + 1),
                    Provider = Convert.ToInt32(goodsproviderr.SelectedIndex + 1),
                    Category = Convert.ToInt32(goodscategoryy.SelectedIndex + 1),
                    Weight = Convert.ToInt32(goodsweightt.Text),
                    OnStock = Convert.ToInt32(onstock.Text),
                    Description = description.Text,
                    Image = null,
                    CallorieValue = goodscalloriess.Text,
                    Allergens = Convert.ToInt32(goodsallergienss.SelectedIndex + 1)
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
            try
            {
                curbakery.NameGoods = goodsnamee.Text;
                curbakery.Price = Convert.ToInt32(price.Text);
                curbakery.TypeOfGoods = Convert.ToInt32(goodstypee.SelectedIndex + 1);
                curbakery.Provider = Convert.ToInt32(goodsproviderr.SelectedIndex + 1);
                curbakery.Category = Convert.ToInt32(goodscategoryy.SelectedIndex + 1);
                curbakery.Weight = Convert.ToInt32(goodsweightt.Text);
                curbakery.OnStock = Convert.ToInt32(onstock.Text);
                curbakery.Description = description.Text;
                curbakery.CallorieValue = goodscalloriess.Text;
                curbakery.Allergens = Convert.ToInt32(goodsallergienss.SelectedIndex + 1);

                Entities.GetContext().SaveChanges();

                MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.BakeryFrame.Navigate(new goodslist());
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}