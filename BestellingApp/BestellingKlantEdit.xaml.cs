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
    /// Interaction logic for BestellingEdit.xaml
    /// </summary>
    public partial class BestellingEdit : Window
    {
        public BestellingEdit()
        {
            InitializeComponent();
            UpdatecbBestellingKlant();
            UpdatecbPersoneelslid();
            Updatecbklant();
            UpdatecbProduct();


        }
        private void UpdatecbPersoneelslid()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {

                cbPersoneelslid.ItemsSource = null;
                var Personeellidquery = ctx.Personeelslid.Select(x => new { lid = x.Voornaam + " " + x.Achternaam, ID = x.PersoneelslidID }).ToList();
                cbPersoneelslid.DisplayMemberPath = "lid";
                cbPersoneelslid.SelectedValuePath = "ID";
                cbPersoneelslid.ItemsSource = Personeellidquery;
                
            }
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
                
            }
        }
        private void UpdatecbBestellingKlant()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                cbBestellingKlant.ItemsSource = null;
                var klantBestellingen = ctx.Bestelling.Join(ctx.Klant,
                    b => b.KlantID,
                    k => k.KlantID,
                    (b, k) => new { b, k, Naam = k.Voornaam+""+k.Achternaam, ID = b.BestellingID });

                cbBestellingKlant.DisplayMemberPath = "Naam";
                cbBestellingKlant.SelectedValuePath = "ID";
                cbBestellingKlant.ItemsSource = klantBestellingen.ToList();
                cbBestellingKlant.SelectedIndex = 0;
            }
        }

        private void cbBestellingKlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                if (cbBestellingKlant.SelectedValue != null)
                {
                    if (ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestellingKlant.SelectedValue) != null)
                    {
                        var selectedBestelling = ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestellingKlant.SelectedValue);
                        dtDatumOpgemaakt.SelectedDate = selectedBestelling.DatumOpgemaakt;
                        cbPersoneelslid.SelectedValue = selectedBestelling.PersoneelslidID;
                        cbKlant.SelectedValue = selectedBestelling.KlantID;
                        var selectedBestellingProduct = ctx.BestellingProduct.Single(b => b.BestellingID == (int)cbBestellingKlant.SelectedValue);
                        cbProduct.SelectedValue = selectedBestellingProduct.ProductID;
                        tbAantal.Text = selectedBestellingProduct.Aantal.ToString();
                    }

                }
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedBestellingklant = ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestellingKlant.SelectedValue);
                selectedBestellingklant.DatumOpgemaakt = (DateTime)dtDatumOpgemaakt.SelectedDate;
                selectedBestellingklant.PersoneelslidID = (int)cbPersoneelslid.SelectedValue;
                selectedBestellingklant.KlantID = (int)cbKlant.SelectedValue;
                ctx.SaveChanges();
                var selectedBestellingProduct = ctx.BestellingProduct.Single(b => b.BestellingID == (int)cbBestellingKlant.SelectedValue);
                selectedBestellingProduct.ProductID = (int)cbProduct.SelectedValue;
                selectedBestellingProduct.Aantal = Convert.ToInt32(tbAantal.Text);
                ctx.SaveChanges();
            }
            MessageBox.Show("Bestelling Bewerk is gedaan");
            UpdatecbBestellingKlant();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                if (cbBestellingKlant.SelectedValue != null)
                {
                    ctx.Bestelling.RemoveRange(ctx.Bestelling.Where(b => b.BestellingID == (int)cbBestellingKlant.SelectedValue));
                  
                    ctx.BestellingProduct.RemoveRange(ctx.BestellingProduct.Where(b => b.BestellingID == (int)cbBestellingKlant.SelectedValue));
                    ctx.SaveChanges();
                    UpdatecbBestellingKlant();
                }
            }
           
            MessageBox.Show("Bestelling is verwijderen");
        }
    }
}   
