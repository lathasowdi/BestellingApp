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
    /// Interaction logic for BestellingLeverancierAdd.xaml
    /// </summary>
    public partial class BestellingLeverancierAdd : Window
    {
        public Personeelslid loggedinpersoneelid { get; set; }
           
        public BestellingLeverancierAdd(Personeelslid loggedin)
        {
            InitializeComponent();
            loggedinpersoneelid = loggedin;
            UpdatecbProduct();
            UpdatecbLeverancier();
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
        public List<LeverancierBesteling> LeverancierProductLijst = new List<LeverancierBesteling>();
       
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ErrorHandling();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedProduct = ctx.Product.Single(p => p.ProductID == (int)cbProduct.SelectedValue);
                LeverancierBesteling product = new LeverancierBesteling(selectedProduct.ProductID, selectedProduct.Naam, Convert.ToInt32(tbAantal.Text));
                LeverancierProductLijst.Add(product);
                UpdatelbLijst();
            }
        }
        public class LeverancierBesteling
        {
            public int ProductID { get; set; }
            public string Naam { get; set; }
            public int Aantal { get; set; }

            public LeverancierBesteling(int productid, string naam, int aantal)
            {
                ProductID = productid;
                Naam = naam;
                Aantal = aantal;
            }
        }
        public void ErrorHandling()
        {
            string errorshow = "";

            if (cbProduct.SelectedIndex <= 0)
            {
                errorshow += "Select een Product";
            }


            if (cbLeverancier.SelectedIndex < 0)
            {
                errorshow += "\r\n" + "Select een Klant a.u.b";
            }

            if (tbAantal.Text.Trim().Length == 0)
            {

                errorshow += "\r\n" + "Geef Aantal a.u.b";
            }


            if (errorshow.Trim().Length > 0)
            {
                MessageBox.Show(errorshow);
            }
        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lbLijst.SelectedIndex != -1)
                LeverancierProductLijst.RemoveAt(lbLijst.SelectedIndex);
            UpdatelbLijst();

        }
        public void UpdatelbLijst()
        {
            lbLijst.ItemsSource = null;
            lbLijst.ItemsSource = LeverancierProductLijst;
            lbLijst.DisplayMemberPath = "Naam";
            lbLijst.SelectedValuePath = "ProductID";

        }

        private void btnBestel_Click(object sender, RoutedEventArgs e)
        {
            if(LeverancierProductLijst.Count > 0)
            {
                using (BestellingenEntities ctx = new BestellingenEntities())
                {
                    Bestelling bestelling = new Bestelling();
                    bestelling.DatumOpgemaakt = DateTime.Now;
                    bestelling.PersoneelslidID = loggedinpersoneelid.PersoneelslidID;
                    bestelling.LeverancierID = (int)cbLeverancier.SelectedValue;
                    ctx.Bestelling.Add(bestelling);
                    ctx.SaveChanges();
                    foreach (var item in LeverancierProductLijst)
                    {
                        BestellingProduct bestellingProduct = new BestellingProduct();
                        bestellingProduct.BestellingID = bestelling.BestellingID;
                        bestellingProduct.ProductID = item.ProductID;
                        bestellingProduct.Aantal = item.Aantal;
                        ctx.BestellingProduct.Add(bestellingProduct);
                        ctx.SaveChanges();
                    }

                }
                MessageBox.Show("Bestelling is Toevoegen");
            }

           LeverancierProductLijst.Clear();
            lbLijst.ItemsSource = null;
            tbAantal.Clear();
            cbProduct.SelectedIndex = -1;

            cbLeverancier.SelectedIndex = -1;
        }

    }
}
    


