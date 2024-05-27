﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Page
    {
        public registration()
        {
            InitializeComponent();
            emailreg.MaxLength = 100;
            phonereg.MaxLength = 15;
            loginreg.MaxLength = 50;
            tbpasswordreg.MaxLength = 20;
            repeatpasswordreg.MaxLength = 20;
        }

        private void repeatpasswordreg_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(repeatpasswordreg.Password != tbpasswordreg.Text)
            {
                regbutton.IsEnabled = false;
            }
            else { regbutton.IsEnabled = true; }
        }

        private void regbutton_Click(object sender, RoutedEventArgs e)
        {
            if (AppConect.bakerymod.Users.Count(x => x.Login == loginreg.Text) > 0)
            {
                MessageBox.Show("такой пользователь уже существует",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (emailreg.Text.Contains("@"))
            {
                if (phonereg.Text.Length < 10 || !phonereg.Text.Contains("+") || phonereg.Text.Length > 15 )
                {
                    MessageBox.Show("Неверный формат телефона",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    registr();
                }
            }
            else
            {
                MessageBox.Show("Неверный формат почты",
                       "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            
        }

        public void registr()
        {
            try
            {
                Users user = new Users()
                {
                    Login = loginreg.Text,
                    Name = namereg.Text,
                    Email = emailreg.Text,
                    Phone = phonereg.Text,
                    Password = tbpasswordreg.Text,
                    Role = 2
                };
                AppConect.bakerymod.Users.Add(user);
                AppConect.bakerymod.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка добавления данных",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void emailreg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void regbut_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new authorizathion());
        }

        private void phonereg_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9 || e.Key == Key.OemPlus || e.Key == Key.Add) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }
    }
}
