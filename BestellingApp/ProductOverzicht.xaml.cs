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
    /// Interaction logic for ProductOverzicht.xaml
    /// </summary>
    public partial class ProductOverzicht : Window
    {
        public ProductOverzicht()
        {
            InitializeComponent();
            BestellingenEntities ctx = new BestellingenEntities();
            
            var productList = ctx.Product.Join(
                            ctx.Categorie,
                            p => p.CategorieID,
                            c => c.CategorieID,
                            (p, c) => new { p, c }).Join(
                            ctx.Leverancier,
                            pc => pc.p.LeverancierID,
                            l => l.LeverancierID,
                            (pc, l) => new { pc, l}).ToList();

            listgrid.ItemsSource = productList;
            List<string> sortProduct = new List<string>()
        {"Naam Up",
        "Naam Down",
        "InkoopPrijs Up",
        "InkoopPrijs Down",
        "Leverancier Up",
        "Leverancier Down",
        "Categorie Up",
        "Categorie Down",
        "Marge Up",
        "Marge Down",
        "Btw Up",
        "Btw Down"
        };
            cbSort.ItemsSource = sortProduct;

            var product = ctx.Product.ToList();

            lbProducts.ItemsSource = productList;
            lbProducts.SelectedValuePath = "ProductID";
        }
        public void UpdateQuery()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                IQueryable<Product> product;


                switch (cbSort.SelectedItem)
                {
                    case "Naam Down":
                        product = ctx.Product.Select(b => b).OrderByDescending(x => x.Naam);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "Naam Up":
                        product = ctx.Product.Select(b => b).OrderBy(x => x.Naam);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "InkoopPrijs Down":
                        product = ctx.Product.Select(b => b).OrderByDescending(x => x.InKoopprijs);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "InkoopPrijs Up":
                        product = ctx.Product.Select(b => b).OrderBy(x => x.InKoopprijs);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "Leverancier Up":
                        product = ctx.Product.Select(b => b).OrderBy(x => x.LeverancierID);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "Leverancier Down":
                        product = ctx.Product.Select(b => b).OrderByDescending(x => x.LeverancierID);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "Categorie Down":
                        product = ctx.Product.Select(b => b).OrderByDescending(x => x.CategorieID);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "Categorie Up":
                        product = ctx.Product.Select(b => b).OrderBy(x => x.CategorieID);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "Marge Down":
                        product = ctx.Product.Select(b => b).OrderByDescending(x => x.Marge);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "Marge Up":
                        product = ctx.Product.Select(b => b).OrderBy(x => x.Marge);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "Btw Down":
                        product = ctx.Product.Select(b => b).OrderByDescending(x => x.BTW);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    case "Btw Up":
                        product = ctx.Product.Select(b => b).OrderBy(x => x.BTW);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                    default:
                        product = ctx.Product.Select(b => b);
                        lbProducts.ItemsSource = product.ToList();
                        break;
                };

              
            }
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateQuery();
        }

        private void lbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int productId = Convert.ToInt32(lbProducts.SelectedValue);
            
            
            BestellingenEntities ctx = new BestellingenEntities();
            var productList = ctx.Product.Join(
                           ctx.Categorie,
                           p => p.CategorieID,
                           c => c.CategorieID,
                           (p, c) => new { p, c }).Join(
                           ctx.Leverancier,
                           pc => pc.p.LeverancierID,
                           l => l.LeverancierID,
                           (pc, l) => new { pc, l }).Where(x => x.pc.p.ProductID == productId).FirstOrDefault(); ;
            string beschrijf = "";
            beschrijf = 
                  $"NAAM:{productList.pc.p.Naam}" + "\n"
                + $"INKOOPPRIJS:{productList.pc.p.InKoopprijs}" + "\n"
                + $"MARGE:{productList.pc.p.Marge}" + "\n"
                + $"EENHEID:{productList.pc.p.Eenheid}" + "\n"
                + $"BTW:{productList.pc.p.BTW}" + "\n"
                + $"LEVERANCIE:{productList.l.Contactpersoon}" + "\n"
                + $"CATEGORIE:{productList.pc.c.CategorieNaam}" + "\n";

          
            if (lbProducts.Items != null)
            {
                lblLijst.Content
                    = beschrijf;

            }
            else
            {
                lblLijst.Content = "";
            }
        }
    }
}
