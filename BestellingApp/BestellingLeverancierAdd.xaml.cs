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
        public BestellingLeverancierAdd()
        {
            InitializeComponent();
            UpdatecbPersoneelslid();
            UpdatecbProduct();
            UpdatecbLeverancier();
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                Bestelling bestelling = new Bestelling();
                bestelling.DatumOpgemaakt = (DateTime)dtDatumOpgemaakt.SelectedDate;
                bestelling.PersoneelslidID = (int)cbPersoneelslid.SelectedValue;
                bestelling.LeverancierID = (int)cbLeverancier.SelectedValue;
                ctx.Bestelling.Add(bestelling);
                ctx.SaveChanges();
                BestellingProduct bestellingProduct = new BestellingProduct();
                bestellingProduct.BestellingID = bestelling.BestellingID;
                bestellingProduct.ProductID = (int)cbProduct.SelectedValue;
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
}
