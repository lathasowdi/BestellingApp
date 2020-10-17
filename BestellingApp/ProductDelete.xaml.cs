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

namespace BestellingApp
{
    /// <summary>
    /// Interaction logic for ProductDelete.xaml
    /// </summary>
    public partial class ProductDelete : Window
    {
        public ProductDelete()
        {
            InitializeComponent();
            updatecombobox();
        }
        private void updatecombobox()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Productquery = ctx.Product.Select(k => k);

                cbProduct.ItemsSource = Productquery.ToList();

            }
        }
         

private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedProduct = (Product)cbProduct.SelectedItem;
                ctx.Product.RemoveRange(ctx.Product.Where(k => k.ProductID == selectedProduct.ProductID));
                ctx.SaveChanges();
            }
            updatecombobox();
            MessageBox.Show("Product is verwijderen");
        }
    }
}
