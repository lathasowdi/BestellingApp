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
    /// Interaction logic for ProductAdd.xaml
    /// </summary>
    public partial class ProductAdd : Window
    {
        public ProductAdd()
        {
            InitializeComponent();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Leverancierquery = ctx.Leverancier.Select(k => k).ToList();
                cbLeverancier.DisplayMemberPath = "Contactpersoon";
                cbLeverancier.SelectedValuePath = "LeverancierID";
                cbLeverancier.ItemsSource = Leverancierquery;
                cbLeverancier.SelectedIndex =-1;
                var Categoriequery = ctx.Categorie.Select(k => k).ToList();
                cbCategorie.DisplayMemberPath = "CategorieNaam";
                cbCategorie.SelectedValuePath = "CategorieID";
                cbCategorie.ItemsSource = Categoriequery;
                cbCategorie.SelectedIndex =-1;

            }
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
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
                double inkoopprijs = 0;
                if (tbInkoopprijs.Text.Trim() != "")
                {
                    inkoopprijs = Convert.ToDouble(tbInkoopprijs.Text.Trim());
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

                Product nieuweProduct = new Product();
                nieuweProduct.Naam = naam;
                nieuweProduct.InKoopprijs = inkoopprijs;
                nieuweProduct.Marge = Marge;
                nieuweProduct.Eenheid = eenheeid;
                nieuweProduct.BTW = btw;
                nieuweProduct.LeverancierID = leverancierID;
                nieuweProduct.CategorieID = categorieID;
                ctx.Product.Add(nieuweProduct);
                ctx.SaveChanges();
            }
            MessageBox.Show("Product Toevoegd");
            MessageBox.Show("Product Toevoegd");

        }
    }
}
