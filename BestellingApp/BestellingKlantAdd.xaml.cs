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
    /// Interaction logic for BestellingKlantAdd.xaml
    /// </summary>
    public partial class BestellingKlantAdd : Window
    {
        public Personeelslid loggedinpersoneelid { get; set; }

       
        public BestellingKlantAdd(Personeelslid loggedin)
        {
            InitializeComponent();
            loggedinpersoneelid = loggedin;
               
                Updatecbklant();
                UpdatecbProduct();
        }
        
        private void Updatecbklant()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                cbKlant.ItemsSource = null;
                var Klantquery = ctx.Klant.Select(x => new { Naam = x.Voornaam + " " + x.Achternaam, Id = x.KlantID }).ToList();
                cbKlant.DisplayMemberPath = "Naam";
                cbKlant.SelectedValuePath = "Id";
                cbKlant.ItemsSource = Klantquery;
                cbKlant.SelectedIndex =-1;
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
                cbProduct.SelectedIndex = -1;
            }
        }
        public List<klantBesteling> KlantProductLijst = new List<klantBesteling>();
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ErrorHandling();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedProduct = ctx.Product.Single(p => p.ProductID == (int)cbProduct.SelectedValue);
                klantBesteling product = new klantBesteling(selectedProduct.ProductID, selectedProduct.Naam, Convert.ToInt32(tbAantal.Text));
                KlantProductLijst.Add(product);
                UpdatelbLijst();
            }
        }
        public void UpdatelbLijst()
        {
           lbLijst.ItemsSource = null;
            lbLijst.ItemsSource = KlantProductLijst;
            lbLijst.DisplayMemberPath = "Naam";
            lbLijst.SelectedValuePath = "ProductID";

        }
        public class klantBesteling
        {
            public int ProductID { get; set; }
            public string Naam { get; set; }
            public int Aantal { get; set; }

            public klantBesteling(int productid, string naam, int aantal)
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
           

            if (cbKlant.SelectedIndex < 0)
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

        private void btnBestel_Click(object sender, RoutedEventArgs e)
        {
            if (KlantProductLijst.Count > 0)
            {
                using (BestellingenEntities ctx = new BestellingenEntities())
                {
                    Bestelling bestelling = new Bestelling();
                    bestelling.DatumOpgemaakt = DateTime.Now;
                    bestelling.PersoneelslidID = loggedinpersoneelid.PersoneelslidID;
                    bestelling.KlantID = (int)cbKlant.SelectedValue;
                    ctx.Bestelling.Add(bestelling);
                    ctx.SaveChanges();
                    foreach (var item in KlantProductLijst)
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
           
            KlantProductLijst.Clear();
            lbLijst.ItemsSource = null;
           tbAantal.Clear();
            cbProduct.SelectedIndex = -1;
           
            cbKlant.SelectedIndex = -1;
        }
           

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(lbLijst.SelectedIndex != -1)
            KlantProductLijst.RemoveAt(lbLijst.SelectedIndex);
            UpdatelbLijst();
        }
    }
}
