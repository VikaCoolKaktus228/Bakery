using Bakery.editgoods;
using Bakery.regauth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            AppConect.bakerymod = new Entities9();
            AppFrame.BakeryFrame = BakFrame;

            BakFrame.Navigate(new authorizathion());
        }

        private void Window_Closed(object sender, EventArgs e)
        {

            if (Entities9.GetContext().Cart.Any())
            {
                var allCartRecords = Entities9.GetContext().Cart.ToList();
                Entities9.GetContext().Cart.RemoveRange(allCartRecords);
                Entities9.GetContext().SaveChanges();
            }

        }
    }
}
