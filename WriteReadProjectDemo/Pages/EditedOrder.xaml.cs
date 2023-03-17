using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WriteReadProjectDemo
{
    /// <summary>
    /// Логика взаимодействия для EditedOrder.xaml
    /// </summary>
    public partial class EditedOrder : Page
    {
        List<SummClass> order = new List<SummClass>();
        public EditedOrder()
        {
            InitializeComponent();
            listSortedFiltres();
            lvOrderEdited.ItemsSource = order;
        }
        public static bool quantinyinStock;
        private void border_Loaded(object sender, RoutedEventArgs e) // загрузка рамки по цвету количества товаров на складе
        {
            Border border = sender as Border;
            int id = Convert.ToInt32(border.Uid);

            List<OrderProduct> orderProduct = db.tbe.OrderProduct.Where(x => x.OrderID == id).ToList();
            foreach (var item in orderProduct)
            {

                Product product1 = db.tbe.Product.FirstOrDefault(x => x.ProductArticleNumber == item.ProductArticleNumber);
                if (item.Product.ProductQuantityInStock > 3)
                {
                    quantinyinStock = true;

                }
                else
                {
                    quantinyinStock = false;
                    break;

                }

            }
            if (quantinyinStock)
            {
                SolidColorBrush scb = (SolidColorBrush)new BrushConverter().ConvertFromString("#20b2aa");
                border.BorderBrush = scb;

            }
            else
            {
                SolidColorBrush scb1 = (SolidColorBrush)new BrushConverter().ConvertFromString("#ff8c00");
                border.BorderBrush = scb1;

            }
        }
        private void tbSostavZakaza_Loaded_1(object sender, RoutedEventArgs e) // вывод состава заказа
        {

            string nameOrderProduct = "";
            TextBlock textBlock = sender as TextBlock;
            int id = Convert.ToInt32(textBlock.Uid);
            List<OrderProduct> orderProduct = db.tbe.OrderProduct.Where(x => x.OrderID == id).ToList();
            foreach (var item in orderProduct)
            {

                Product product1 = db.tbe.Product.FirstOrDefault(x => x.ProductArticleNumber == item.ProductArticleNumber);

                nameOrderProduct += product1.ProductName + $"({item.Count} шт.) ";


            }
            textBlock.Text = "Состав заказа >  " + nameOrderProduct;

        }

        private void tbSummZakaza_Loaded(object sender, RoutedEventArgs e) // вывод суммы заказа
        {

            int summa = 0;
            TextBlock textBlock = sender as TextBlock;
            int id = Convert.ToInt32(textBlock.Uid);
            List<OrderProduct> orderProduct = db.tbe.OrderProduct.Where(x => x.OrderID == id).ToList();
            foreach (var item in orderProduct)
            {

                Product product1 = db.tbe.Product.FirstOrDefault(x => x.ProductArticleNumber == item.ProductArticleNumber);

                summa += (int)product1.ProductCost * item.Count;


            }
            textBlock.Text = "Сумма заказа > " + Convert.ToString(summa);
        }

        public static List<int> orderFIltresSumm;
        private void tbAllSale_Loaded(object sender, RoutedEventArgs e) // вывод скидки
        {
            double summa = 0;
            double summa1 = 0;
            double procent = 0;
            TextBlock textBlock = sender as TextBlock;
            int id = Convert.ToInt32(textBlock.Uid);
            if (textBlock.Uid != null)
            {
                List<OrderProduct> orderProduct = db.tbe.OrderProduct.Where(x => x.OrderID == id).ToList();
                foreach (var item in orderProduct)
                {
                    Product product1 = db.tbe.Product.FirstOrDefault(x => x.ProductArticleNumber == item.ProductArticleNumber);
                    summa += (int)product1.ProductCost * item.Count; // просто цена

                    summa1 += (int)(product1.ProductCost - (product1.ProductCost * product1.ProductDiscountAmount / 100)) * item.Count; // с учетом скидки


                }
                procent = (summa - summa1) / (summa / 100);
                textBlock.Text = "Сумма с учетом скидки > " + summa1.ToString() + $"({procent} %) ";
            }
            else
            {
                textBlock.Text = "";
            }
        }




        private void btnChangeOrder_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int id = Convert.ToInt32(button.Uid);
            Order order = db.tbe.Order.FirstOrDefault(x => x.OrderID == id);
            WindowChangeOrderADM orderADM = new WindowChangeOrderADM(order);
            orderADM.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        void listSortedFiltres() // метод для того, чтобы осуществить фильтрацию и сортировку по цене и скидке
        {
            foreach (var item in db.tbe.Order.ToList())
            {
                SummClass opl = new SummClass();
                opl.order = item;
                double sum = 0, sumDis = 0;
                double discount = 0;
                int orderid = item.OrderID;
                foreach (var item2 in db.tbe.OrderProduct.Where(x => x.OrderID == item.OrderID))
                {
                    sum += (double)item2.Product.ProductCost * item2.Count;
                    sumDis += (double)(item2.Product.ProductCost - (item2.Product.ProductCost / 100 * item2.Product.ProductDiscountAmount)) * item2.Count;
                    string prodID = item2.ProductArticleNumber;
                }
                discount = (sum - sumDis) / (sum / 100);
                opl.Summa = Convert.ToInt32(sum);
                opl.SALE = Convert.ToInt32(discount);
                order.Add(opl);
            }
        }
        public void filtres() // общий метод сортировки и фильтрации
        {
            try
            {
                order.Clear();
                listSortedFiltres();

                if (cmbFiltres != null)
                {
                    if (cmbFiltres.SelectedItem != null)
                    {
                        ComboBoxItem cmb1 = (ComboBoxItem)cmbFiltres.SelectedItem;
                        switch (cmb1.Content)
                        {
                            case "0-10%":
                                {
                                    order = order.Where(x => x.SALE >= 0 && x.SALE < 10).ToList();
                                    break;
                                }
                            case "11-14%":
                                {
                                    order = order.Where(x => x.SALE > 11 && x.SALE <= 14).ToList();

                                    break;
                                }
                            case "15% и более":
                                {
                                    order = order.Where(x => x.SALE > 15).ToList();
                                    break;
                                }
                        }
                    }
                }

                if (cmbSorted != null)
                {
                    if (cmbSorted.SelectedItem != null)
                    {
                        ComboBoxItem cmb = (ComboBoxItem)cmbSorted.SelectedItem;
                        switch (cmb.Content)
                        {
                            case "По возрастанию":
                                {
                                    order = order.OrderBy(x => x.Summa).ToList();
                                    break;
                                }
                            case "По убыванию":
                                {
                                    order = order.OrderByDescending(x => x.Summa).ToList();

                                    break;
                                }
                        }
                    }

                }
                if(order.Count == null || order.Count == 0)
                {
                    MessageBox.Show("Отсутствуют критерии, удовлетворяющие результатам поиска!");
                }
                lvOrderEdited.ItemsSource = order;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
           


          

        }
        private void cmbSorted_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filtres();
        }

        private void btnOutFiltres_Click(object sender, RoutedEventArgs e) // сброс фильтров
        {
         

            cmbSorted.SelectedValue = order;
            cmbFiltres.SelectedValue = order;

        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int id = Convert.ToInt32(button.Uid);
            Order order = db.tbe.Order.FirstOrDefault(x => x.OrderID == id);
            WindowChangeOrder orderchange = new WindowChangeOrder(order);
            orderchange.Show();
        }
    }
}
