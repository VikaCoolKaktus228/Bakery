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
    /// Логика взаимодействия для cart.xaml
    /// </summary>
    /// 
    public partial class cart : Page
    {
        int idusercart = Convert.ToInt32(App.Current.Properties["Id"].ToString());

        public cart()
        {
           
            InitializeComponent();

            var orderobj = Entities.GetContext().Order
                               .Where(x => x.IdUser == idusercart)
                               .Select(x => x.IdGoods)
                               .ToList();
            var goodsInCart = Entities.GetContext().GoodsBakery
                                         .Where(x => orderobj.Contains(x.Id))
                                         .ToList();

            cartbakery.ItemsSource = goodsInCart;
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
                int sum = 0;

                var orderobj = Entities.GetContext().Order
                               .Where(x => x.IdUser == idusercart)
                               .ToList();

                foreach (var item in orderobj)
                {
                    if (item is Order)
                    {
                        Order data = (Order)item;
                        //Image img = Image.GetInstance("C:\\Users\\User\\Pictures\\bakeryhome\\Bakery\\" + data.GoodsBakery.CurrentPhoto);
                        //img.ScaleAbsolute(100f, 100f);
                        //doc.Add(img);
                        doc.Add(new Paragraph("Haзвaние: " + data.GoodsBakery.NameGoods, font));
                        doc.Add(new Paragraph("Oпиcaние: " + data.GoodsBakery.Description, font));
                        doc.Add(new Paragraph("производитель " + data.GoodsBakery.Provider1.Provider1, font));
                        doc.Add(new Paragraph("Cтоимость: " + data.GoodsBakery.Price.ToString() + "py6.", font));
                        sum += (int)data.GoodsBakery.Price;
                    }
                }
                Paragraph paragraph = new Paragraph("Cymma = " + sum.ToString(), font);
                paragraph.Alignment = Element.ALIGN_RIGHT;
                doc.Add(paragraph);
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
                doc.Close();
            }
        }

        private void removecart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gobackbutton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new goodslistuser((sender as Button).DataContext as Users));
        }
        private void orderbutton_Click(object sender, RoutedEventArgs e)
        {
            CreatePDF();

            AppFrame.BakeryFrame.Navigate(new OrderForm());

        }
    }
}
