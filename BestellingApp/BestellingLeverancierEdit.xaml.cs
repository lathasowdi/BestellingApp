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
    /// Interaction logic for BestellingLeverancierEdit.xaml
    /// </summary>
    public partial class BestellingLeverancierEdit : Window
    {
        public Personeelslid loggedinpersoneelid { get; set; }
        public BestellingLeverancierEdit(Personeelslid loggedin)
        {
            InitializeComponent();
            loggedinpersoneelid = loggedin;
            UpdatecbBestellingLeverancier();

            UpdatecbLeverancier();
            UpdatecbProduct();
        }
        private void UpdatecbBestellingLeverancier()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                cbBestellingLeverancier.ItemsSource = null;
                var LeverancierBestellingen = ctx.Bestelling.Join(ctx.Leverancier,
                    b => b.LeverancierID,
                    k => k.LeverancierID,
                    (b, k) => new { b, k, Naam = k.Contactpersoon, ID = b.BestellingID });

                cbBestellingLeverancier.DisplayMemberPath = "Naam";
                cbBestellingLeverancier.SelectedValuePath = "ID";
                cbBestellingLeverancier.ItemsSource = LeverancierBestellingen.ToList();
                cbBestellingLeverancier.SelectedIndex = -1;
            }
        }
        private void UpdatecbProduct()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Productquery = ctx.Product.Select(k => k).ToList();
                cbProduct.DisplayMemberPath = "Naam";
                cbProduct.SelectedValuePath = "ProductID";
                cbProduct.ItemsSource = Productquery;
                cbProduct.SelectedIndex = 0;
            }
        }
        private void UpdatecbLeverancier()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Leverancierquery = ctx.Leverancier.Select(x => x).ToList();
                cbLeverancier.DisplayMemberPath = "Contactpersoon";
                cbLeverancier.SelectedValuePath = "LeverancierID";
                cbLeverancier.ItemsSource = Leverancierquery;
                cbLeverancier.SelectedIndex = 0;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                if (cbBestellingLeverancier.SelectedValue != null)
                {
                    ctx.Bestelling.RemoveRange(ctx.Bestelling.Where(b => b.BestellingID == (int)cbBestellingLeverancier.SelectedValue));

                    ctx.BestellingProduct.RemoveRange(ctx.BestellingProduct.Where(b => b.BestellingID == (int)cbBestellingLeverancier.SelectedValue));
                    ctx.SaveChanges();
                    UpdatecbBestellingLeverancier();
                }
            }
            lbLijst.ItemsSource = null;
            cbLeverancier.SelectedIndex = -1;

            MessageBox.Show("Bestelling is verwijderen");
        }

       

        private void cbBestellingKlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                if (cbBestellingLeverancier.SelectedValue != null)
                {
                    if (ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestellingLeverancier.SelectedValue) != null)
                    {
                        var selectedBestelling = ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestellingLeverancier.SelectedValue);

                        cbLeverancier.SelectedValue = selectedBestelling.LeverancierID;
                        var selectedBestellingProduct = ctx.BestellingProduct.Where(b => b.BestellingID == (int)cbBestellingLeverancier.SelectedValue).ToList();

                        var JoinedQuery = ctx.BestellingProduct.Join(ctx.Product,
                            b => b.ProductID,
                            p => p.ProductID,
                            (b, p) => new { b, p, Naam = p.Naam, ID = p.ProductID }).Where(b => b.b.BestellingID == (int)cbBestellingLeverancier.SelectedValue).ToList();

                        lbLijst.ItemsSource = JoinedQuery;
                        lbLijst.DisplayMemberPath = "Naam";
                        lbLijst.SelectedValuePath = "ID";
                    }

                }
            }
        }

        private void lbLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                if (lbLijst.SelectedValue != null)
                {
                    var selectedProduct = ctx.BestellingProduct.Where(x => x.BestellingID == (int)cbBestellingLeverancier.SelectedValue).Single(b => b.ProductID == (int)lbLijst.SelectedValue);
                    tbAantal.Text = selectedProduct.Aantal.ToString();
                }
            }
        }

        private void cbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                if (cbCategorie.SelectedIndex >= 0)
                {
                    var productLijst = ctx.Product.Select(x => new
                    {
                        Naam = x.Naam + " (Prijs: " + (x.InKoopprijs + x.Marge) + ")",
                        Id = x.ProductID,
                        CategorieID = x.CategorieID
                    }).Where(p => p.CategorieID == (int)cbCategorie.SelectedValue).ToList();
                    cbProduct.DisplayMemberPath = "Naam";
                    cbProduct.SelectedValuePath = "Id";
                    cbProduct.ItemsSource = productLijst;
                }
            }
        }
    }
}
