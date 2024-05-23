using Aspose.BarCode.Generation;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = iTextSharp.text.Image;
using System.Windows.Markup;
using System.IO;
using System.Xml.Linq;
using Paragraph = iTextSharp.text.Paragraph;



namespace Bakery
{
    /// <summary>
    /// Логика взаимодействия для OrderForm.xaml
    /// </summary>
    public partial class OrderForm : Page
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        int a = 1;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "https://bom.firpo.ru/Public/86");
            gen.Parameters.Barcode.XDimension.Pixels = 34;
            string dataDir = @"C:\Users\10210806\source\repos\Bakery\Bakery\";
            gen.Save(dataDir + a.ToString() + "1.png", BarCodeImageFormat.Png);
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(dataDir + a.ToString() + "1.png");
            bitmap.EndInit();
            img.Source = bitmap;
            a++;
        }

        private void exitbttn_Click(object sender, RoutedEventArgs e)
        {
            //AppFrame.BakeryFrame.Navigate(new goodslistuser((sender as Button).DataContext as Users));
            Environment.Exit(0);
        }
    }
}
