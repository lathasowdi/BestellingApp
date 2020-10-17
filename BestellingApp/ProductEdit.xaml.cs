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
    /// Interaction logic for ProductEdit.xaml
    /// </summary>
    public partial class ProductEdit : Window
    {
        public ProductEdit()
        {
            InitializeComponent();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
               
                var Productquery = ctx.Product.Select(k => k).ToList();
                cbProduct.DisplayMemberPath = "Naam";
                cbProduct.SelectedValuePath = "ProductID";
                cbProduct.ItemsSource = Productquery;
                cbProduct.SelectedIndex = 0;
                cbLeverancier.ItemsSource = null;
                var Leverancierquery = ctx.Leverancier.Select(k => k).ToList();
                cbLeverancier.DisplayMemberPath = "Contactpersoon";
                cbLeverancier.SelectedValuePath = "LeverancierID";
                cbLeverancier.ItemsSource = Leverancierquery;
                cbLeverancier.SelectedIndex = 0;
                var Categoriequery = ctx.Categorie.Select(k => k).ToList();
                cbCategorie.DisplayMemberPath = "CategorieNaam";
                cbCategorie.SelectedValuePath = "CategorieID";
                cbCategorie.ItemsSource = Categoriequery;
                cbCategorie.SelectedIndex = 0;
            }
        }

        private void btnBewerken_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                string naam = "";
                if (tbNaam.Text.Trim() != "")
                {
                    naam = tbNaam.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Naam a.u.b");
                }
                float inkoopprijs = 0;
                if (tbInkoopprijs.Text.Trim() != "")
                {
                    inkoopprijs =(float) Convert.ToDouble(tbInkoopprijs.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Geef Inkoopprijs a.u.b");
                }
                double Marge = 0;
                if (tbMarge.Text.Trim() != "")
                {
                    Marge = Convert.ToDouble(tbMarge.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Geef Marge a.u.b");
                }
                string eenheeid = "";
                if (tbEenheid.Text.Trim() != "")
                {
                    eenheeid = tbEenheid.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Eenheid a.u.b");
                }
                double btw = 0;
                if (tbBtw.Text.Trim() != "")
                {
                    btw = Convert.ToDouble(tbBtw.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Geef BTW a.u.b");
                }
                int leverancierID = (int)cbLeverancier.SelectedValue;
                int categorieID = (int)cbCategorie.SelectedValue;
                ctx.SaveChanges();
               
            }
            
            MessageBox.Show("Product Bewerk is gedaan");
        }


            private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedProduct = (Product)cbProduct.SelectedItem;
                tbNaam.Text = selectedProduct.Naam;
                tbInkoopprijs.Text = selectedProduct.InKoopprijs.ToString();
                tbMarge.Text = selectedProduct.Marge.ToString();
                tbEenheid.Text = selectedProduct.Eenheid;
                tbBtw.Text = selectedProduct.BTW.ToString();
               cbLeverancier.SelectedValue = selectedProduct.LeverancierID;
                cbCategorie.SelectedValue = selectedProduct.CategorieID;
                
            }
        }
       
    }
}
