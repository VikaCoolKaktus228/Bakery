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
            string dataDir = @"C:\Users\10210806\source\repos\vikusikkrutaya\bakery11\Bakery";
            gen.Save(dataDir + a.ToString() + "1.png", BarCodeImageFormat.Png);
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(dataDir + a.ToString() + "1.png");
            bitmap.EndInit();
            img.Source = bitmap;
            a++;
        }

        private void CreatePDF()
        {
            Document doc = new Document();

            try
            {
                PdfWriter.GetInstance(doc, new FileStream("..\\..\\output.pdf", FileMode.Create));

                doc.Open();
                BaseFont basefont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                Font font = new Font(basefont, 12);
                Font font1 = new Font(basefont, 25, 3, BaseColor.BLUE);
                Paragraph paragraph1 = new Paragraph("список товаров", font1);
                doc.Add(paragraph1);
                decimal sum = 0;

                foreach (var item in AppConect.bakerymod.GoodsBakery.ToList())
                {
                //    if (item is GoodsBakery)
                //    {
                //        GoodsBakery data = (GoodsBakery)item;
                //        //Image img = Image.GetInstance(@"H:\C#\WPF\" + data.CorrectImage); 
                //        //img.ScaleAbsolute(100f, 100f); // Устанавливаем размер изображения doc.Add(img);
                //    }
                //    //doc.Add(new Paragraph("Haзвaние: " + data.NameGoods, font));
                //    //doc.Add(new Paragraph("Oпиcaние: " + data.Description, font));
                //    //doc.Add(new Paragraph("производитель " + data.Provider, font));
                //    //doc.Add(new Paragraph("Cтоимость: " + data.price.ToString + ()"py6.", font));
                //    //doc.Add(new Paragraph("Разнер скидки:" + data.Product DiscountAmount + "%", font));
                //    //sum += data.ProductCost;
                }
                //Paragraph paragraph = new Paragraph("Cymma = " + sum.ToString(), font); paragraph Alignment Element.ALIGN_RIGHT;
                //doc.Add(paragraph);
            }
            catch (DocumentException de)
            {
                Console.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }
            finally
            {

            }
        }

        private void exitbttn_Click(object sender, RoutedEventArgs e)
        {
            //AppFrame.BakeryFrame.Navigate(new goodslistuser((sender as Button).DataContext as Users));
            Environment.Exit(0);
        }
    }
}
