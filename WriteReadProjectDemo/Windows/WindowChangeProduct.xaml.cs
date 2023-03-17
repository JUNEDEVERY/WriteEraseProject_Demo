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
using System.Windows.Shapes;

namespace WriteReadProjectDemo.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowChangeProduct.xaml
    /// </summary>
    public partial class WindowChangeProduct : Window
    {
        Product product;
        public WindowChangeProduct(Product product)
        {
            InitializeComponent();
            this.product = product;
            tbProductName.Text = product.ProductName;
            tbProductDescription.Text = product.ProductDescription;
            cmbProductSupplier.ItemsSource = db.tbe.supplier.ToList();
            cmbProductSupplier.SelectedValuePath = "idSupplier";
            cmbProductSupplier.DisplayMemberPath = "supplierName";
            cmbProductSupplier.SelectedValue = product.idSupplier.ToString();
            tbOldPrice.Text = product.ProductCost.ToString();
            tbSale.Text = product.ProductDiscountAmount.ToString();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbProductName.Text) && !string.IsNullOrEmpty(tbProductDescription.Text) && !string.IsNullOrEmpty(tbOldPrice.Text)  && !string.IsNullOrEmpty(tbSale.Text) && cmbProductSupplier.SelectedIndex != null && cmbProductSupplier != null)
            {
                product.ProductName = tbProductName.Text;
                product.ProductDescription = tbProductDescription.Text;
                product.idSupplier = Convert.ToInt32(cmbProductSupplier.SelectedValue);
                product.ProductCost = Convert.ToDecimal(tbOldPrice.Text);
                product.ProductDiscountAmount = Convert.ToInt32(tbSale.Text);
                db.tbe.SaveChanges();
                this.Close();
                MessageBox.Show("Готово");
              
            }

        }
    }
}
