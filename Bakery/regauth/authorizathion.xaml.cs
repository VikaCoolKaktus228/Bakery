﻿using System;
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

namespace Bakery.regauth
{
    /// <summary>
    /// Логика взаимодействия для authorizathion.xaml
    /// </summary>
    public partial class authorizathion : Page
    {
        public authorizathion()
        {
            InitializeComponent();

        }

        private void loginreg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = AppConect.bakerymod.Users.FirstOrDefault(x => x.Login == loginauth.Text && x.Password == passwordauth.Password);
                if (user == null)
                {
                    MessageBox.Show("Такого пользователя не существует",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    switch (user.Role)
                    {
                        case 1:
                            MessageBox.Show("здраствйте админ",
                    "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.BakeryFrame.Navigate(new goodslist());
                            break;
                        case 2:
                            MessageBox.Show("здраствйте пользователь",
                    "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.BakeryFrame.Navigate(new goodslistuser());
                            break;
                        default:
                            MessageBox.Show("данные не обнаружены!",
                    "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString(),
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new registration());
        }
    }
}
