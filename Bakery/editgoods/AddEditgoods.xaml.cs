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

namespace Bakery.editgoods
{
    /// <summary>
    /// Логика взаимодействия для AddEditgoods.xaml
    /// </summary>
    public partial class AddEditgoods : Page
    {
        private GoodsBakery curbakery = new GoodsBakery();
        public AddEditgoods(GoodsBakery selectedgoods)
        {
            InitializeComponent();
            DataContext = curbakery;
            goodscategoryy.ItemsSource = Entities.GetContext().Category.Select(x => x.Category1).ToList();
            goodsallergienss.ItemsSource = Entities.GetContext().Allergens.Select(x => x.Allergen).ToList();
            goodsproviderr.ItemsSource = Entities.GetContext().Provider.Select(x => x.Provider1).ToList();
            goodstypee.ItemsSource = Entities.GetContext().TypeOfGoods.Select(x => x.Type).ToList();

            if(selectedgoods != null)
            {
                curbakery = selectedgoods;
            }


        }

        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            if (curbakery.Id == 0)
            {
                Entities.GetContext().GoodsBakery.Add(curbakery);
            }

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("vse super");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            AppFrame.BakeryFrame.Navigate(new goodslist());
        }
    }
}
