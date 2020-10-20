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
        public BestellingKlantAdd()
        {
            InitializeComponent();
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
                cbPersoneelslid.SelectedIndex = 0;
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
                cbKlant.SelectedIndex = 0;
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                Bestelling bestelling = new Bestelling();
                bestelling.DatumOpgemaakt = (DateTime)dtDatumOpgemaakt.SelectedDate;
                bestelling.PersoneelslidID = (int)cbPersoneelslid.SelectedValue;
                bestelling.KlantID = (int)cbKlant.SelectedValue;
                ctx.Bestelling.Add(bestelling);
                ctx.SaveChanges();
                BestellingProduct bestellingProduct = new BestellingProduct();
                bestellingProduct.BestellingID = bestelling.BestellingID;
                bestellingProduct.ProductID= (int)cbProduct.SelectedValue;
                int aantal = 0;
                if (tbAantal.Text.Trim() != "")
                {
                    aantal = Convert.ToInt32(tbAantal.Text);
                }
                else
                {
                    MessageBox.Show("Geef Aantal a.u.b");
                }
                bestellingProduct.Aantal = aantal;
                ctx.BestellingProduct.Add(bestellingProduct);
                ctx.SaveChanges();
            }
            MessageBox.Show("Bestelling is Toevoegen");
        }
    }
}
