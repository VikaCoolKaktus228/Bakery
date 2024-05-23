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
using System.Runtime.Remoting.Contexts;

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

            var orderobj = Entities3.GetContext().Order
                               .Where(x => x.IdUser == idusercart)
                               .Select(x => x.Id)
                               .ToList();

            var cartobj = Entities3.GetContext().Cart
                               .Where(c => orderobj.Contains(c.IdOrder))
                               //.Select(x => x.IdGoods)
                               .ToList();
            //var goodsInCart = Entities3.GetContext().GoodsBakery
            //                             .Where(x => cartobj.Contains(x.Id))
            //                             .Select(x => x.Id)
            //                             .ToList();
            cartbakery.ItemsSource = cartobj;
        }

        private void CreatePDF()
        {
            Document doc = new Document();

            try
            {
                string fileName = System.IO.Path.Combine("C:\\Users\\10210806\\Downloads", $"order_{DateTime.Now:yyyyMMddHH}.pdf"); 
                PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));

                doc.Open();
                BaseFont basefont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                Font font = new Font(basefont, 12);
                Font font1 = new Font(basefont, 25, 3, BaseColor.BLUE);
                Paragraph paragraph1 = new Paragraph("список товаров", font1);
                doc.Add(paragraph1);
                int sum = 0;

                var orderobj = Entities3.GetContext().Order
                               .Where(x => x.IdUser == idusercart)
                               .Select(x => x.Id)
                               .ToList();

                var cartobj = Entities3.GetContext().Cart
                                   .Where(c => orderobj.Contains(c.IdOrder))
                                   .ToList();

                foreach (var item in cartobj)
                {
                    if (item is Cart)
                    {
                        Cart data = (Cart)item;
                        //Image img = Image.GetInstance("C:\\Users\\10210806\\source\\repos\\Bakery\\Bakery\\" + data.GoodsBakery.CurrentPhoto);
                        //img.ScaleAbsolute(100f, 100f);
                        //doc.Add(img);
                        doc.Add(new Paragraph("Haзвaние: " + data.GoodsBakery.NameGoods, font));
                        doc.Add(new Paragraph("Oпиcaние: " + data.GoodsBakery.Description, font));
                        doc.Add(new Paragraph("Производитель: " + data.GoodsBakery.Provider1.Provider1, font));
                        doc.Add(new Paragraph("Cтоимость: " + data.GoodsBakery.Price.ToString() + "py6.", font));
                        sum += (int)data.GoodsBakery.Price;
                    }
                }
                Paragraph paragraph = new Paragraph("Cyммa = " + sum.ToString() + "руб.", font);
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

        //public void RemoveItemsFromCart(List<Cart> goodsfordeleting)
        //{
        //    int idusercart = Convert.ToInt32(App.Current.Properties["Id"].ToString());


        //    var context = Entities3.GetContext();
        //    var goodsIds = goodsfordeleting.Select(x => x.IdGoods).ToList();



        //    var ordersToRemove = context.Order
        //                               .Where(x => x.IdUser == idusercart)
        //                               .Select(x => x.Id)
        //                               .ToList();
        //    var cartobjtoremove = context.Cart
        //        .Where(c => ordersToRemove.Contains(c.IdOrder) && goodsIds.Contains(c.IdGoods))
        //        .ToList();
        //    context.Cart.RemoveRange(cartobjtoremove);
        //    context.SaveChanges();

        //    var orderobj = Entities3.GetContext().Order
        //                       .Where(x => x.IdUser == idusercart)
        //                       .Select(x => x.Id)
        //                       .ToList();

        //    var cartobj = Entities3.GetContext().Cart
        //                       .Where(c => orderobj.Contains(c.IdOrder))
        //                       .ToList();

        //    cartbakery.ItemsSource = cartobj;

        //}
        private void removecart_Click(object sender, RoutedEventArgs e)
        {
            cartbakery.ItemsSource = Entities3.GetContext().Cart.ToList(); 
            Button b = sender as Button;
            int ID = int.Parse(((b.Parent as StackPanel).Children[0] as TextBlock).Text); 
            AppConect.bakerymod.Cart.Remove(AppConect.bakerymod.Cart.Where(x => x.Id == ID).First());
            AppConect.bakerymod.SaveChanges();
            AppFrame.BakeryFrame.GoBack(); 
            AppFrame.BakeryFrame.Navigate(new cart());
        }


            private void gobackbutton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.BakeryFrame.Navigate(new goodslistuser((sender as Button).DataContext as Users));
        }

        private void addmanagerorder()
        {
            try
            {
                var order = Entities3.GetContext().Order.FirstOrDefault(o => o.IdUser == idusercart);
                ManagerOrder neworder = new ManagerOrder()
                {
                    IdUser = idusercart,
                    IdOrder = order.Id,
                    DateTimeOrder = DateTime.Now,
                    Status = 2

                };

                AppConect.bakerymod.ManagerOrder.Add(neworder);
                AppConect.bakerymod.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            }
        private void orderbutton_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show($"Вы точно хотите сформировать заказ?", "Внимание",
               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    addmanagerorder();
                    CreatePDF();
                    MessageBox.Show("PDF документ заказа успешно сформирован!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    //var goodsIds = Entities3.GetContext().Order
                    //.Where(x => x.IdUser == idusercart).Select(x => x.Id);
                    //var cartids = Entities3.GetContext().Cart.Where(c => goodsIds.Contains(c.IdOrder)).ToList();
                    //RemoveItemsFromCart(cartids);
                    //AppFrame.BakeryFrame.Navigate(new OrderForm());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }
    }
}
