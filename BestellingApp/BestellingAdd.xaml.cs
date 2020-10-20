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
    /// Interaction logic for BestellingAdd.xaml
    /// </summary>
    public partial class BestellingAdd : Window
    {
        

        public BestellingAdd()
        {
            InitializeComponent();
           
            using (BestellingenEntities ctx = new BestellingenEntities())
               {
                

                cbPersoneelslid.ItemsSource = null;
                var Personeellidquery = ctx.Personeelslid.Select(x => new { lid = x.Voornaam + " " + x.Achternaam, ID= x.PersoneelslidID }).ToList();
                cbPersoneelslid.DisplayMemberPath = "lid";
                cbPersoneelslid.SelectedValuePath = "ID";
                cbPersoneelslid.ItemsSource = Personeellidquery;
                cbPersoneelslid.SelectedIndex = 0;

                var Leverancierquery = ctx.Leverancier.Select(x => x).ToList();
                cbLeverancier.DisplayMemberPath = "Contactpersoon";
                cbLeverancier.SelectedValuePath = "LeverancierID";
                cbLeverancier.ItemsSource = Leverancierquery;
                cbLeverancier.SelectedIndex = 0;
                cbKlant.ItemsSource = null;
                var Klantquery = ctx.Klant.Select(x => new { Naam = x.Voornaam + " " + x.Achternaam, Id = x.KlantID }).ToList();
                cbKlant.DisplayMemberPath = "Naam";
                cbKlant.SelectedValuePath = "Id";
                cbKlant.ItemsSource = Klantquery;
                cbKlant.SelectedIndex = 0;
                var Categoriequery = ctx.Categorie.Select(x => x).ToList();
                cbCategorie.DisplayMemberPath = "CategorieNaam";
                cbCategorie.SelectedValuePath = "CategorieID";
                cbCategorie.ItemsSource = Categoriequery;
                cbCategorie.SelectedIndex = 0;
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
                bestelling.LeverancierID = (int)cbLeverancier.SelectedValue;
                bestelling.KlantID = (int)cbKlant.SelectedValue;
                ctx.Bestelling.Add(bestelling);
                ctx.SaveChanges();
               
                UpdatecbBestellingKlant();
            }
            MessageBox.Show("Bestelling is Toevoegen");

        }

        private void cbBestelling_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                        cbLeverancier.SelectedValue = selectedBestelling.LeverancierID;
                        cbKlant.SelectedValue = selectedBestelling.KlantID;
                    }
                }

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                ctx.Bestelling.Remove(ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestellingKlant.SelectedValue));
                ctx.SaveChanges();
            }
            UpdatecbBestellingKlant();
            MessageBox.Show("Bestelling is verwijderen");
        }
        private void UpdatecbBestellingKlant()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                cbBestellingKlant.ItemsSource = null;
                var listBestellingen = ctx.Bestelling.Join(ctx.Klant,
                    b => b.KlantID,
                    k => k.KlantID,
                    (b, k) => new { b, k, Naam = k.Voornaam + "" + k.Achternaam, ID = b.BestellingID });

                cbBestellingKlant.DisplayMemberPath = "Naam";
                cbBestellingKlant.SelectedValuePath = "ID";
                cbBestellingKlant.ItemsSource = listBestellingen.ToList();
                cbBestellingKlant.SelectedIndex = 0;
            }
        }
        private void UpdatecbBestellingLeverancier()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                cbBestellingLeverancier.ItemsSource = null;
                var listBestellingen = ctx.Bestelling.Join(ctx.Leverancier,
                    b => b.LeverancierID,
                    k => k.LeverancierID,
                    (b, k) => new { b, k, Naam = k.Contactpersoon, ID = b.BestellingID });

                cbBestellingLeverancier.DisplayMemberPath = "Naam";
                cbBestellingLeverancier.SelectedValuePath = "ID";
                cbBestellingLeverancier.ItemsSource = listBestellingen.ToList();
                cbBestellingLeverancier.SelectedIndex = 0;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedBestelling = ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestellingKlant.SelectedValue);
                selectedBestelling.DatumOpgemaakt = (DateTime)dtDatumOpgemaakt.SelectedDate;
                selectedBestelling.PersoneelslidID = (int)cbPersoneelslid.SelectedValue;
                selectedBestelling.LeverancierID = (int)cbLeverancier.SelectedValue;
                selectedBestelling.KlantID = (int)cbKlant.SelectedValue;
                ctx.SaveChanges();
            }
            MessageBox.Show("Bestelling Bewerk is gedaan");
            UpdatecbBestellingKlant();
        }

        private void chbEdit_Checked(object sender, RoutedEventArgs e)
        {

            if ((bool)chbEdit.IsChecked)
            {
                cbBestellingKlant.IsEnabled = true;
                btnBekijk.IsEnabled = false;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnAdd.IsEnabled = false;
                chbBekijk.IsEnabled = false;
                UpdatecbBestellingKlant();
            }

        }

        private void chbEdit_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((bool)!chbEdit.IsChecked)
            {
                cbBestellingKlant.IsEnabled = false;
                btnBekijk.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAdd.IsEnabled = true;
                chbBekijk.IsEnabled = false;

            }
        }

        private void chbBekijk_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((bool)!chbBekijk.IsChecked)
            {
                cbBestellingKlant.IsEnabled = false;
                btnBekijk.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAdd.IsEnabled = true;
                chbEdit.IsEnabled = false;
                UpdatecbBestellingKlant();
            }
        }

        private void chbBekijk_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)chbBekijk.IsChecked)
            {
                cbBestellingKlant.IsEnabled = true;
                btnBekijk.IsEnabled = true;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAdd.IsEnabled = false;
                UpdatecbBestellingKlant();
            }

        }

        private void btnBekijk_Click(object sender, RoutedEventArgs e)
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
                        cbLeverancier.SelectedValue = selectedBestelling.LeverancierID;
                        cbKlant.SelectedValue = selectedBestelling.KlantID;
                    }
                }
            }
        }

        //private void cbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    using (BestellingenEntities ctx = new BestellingenEntities())
        //    {
        //        var selectedProducts = ctx.Product.Select(b => b.CategorieID == (int)cbCategorie.SelectedValue).ToList();
        //        cbProduct.DisplayMemberPath = "Naam";
        //        cbProduct.SelectedValuePath = "ProductID";
        //        cbProduct.ItemsSource = selectedProducts;
        //        cbProduct.SelectedIndex = 0;
        //    }
        //}
    }
}
