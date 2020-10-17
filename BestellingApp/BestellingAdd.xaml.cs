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
                var Personeellidquery = ctx.Personeelslid.Select(x => new { lid = x.Voornaam + " " + x.Achternaam + " " + x.Usernaam, ID = x.PersoneelslidID }).ToList();
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
            }
            MessageBox.Show("Bestelling is Toevoegen");

        }

        private void cbBestelling_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                if (cbBestelling.SelectedValue != null)
                {
                    if (ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestelling.SelectedValue) != null)
                    {
                        var selectedBestelling = ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestelling.SelectedValue);
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
                ctx.Bestelling.Remove(ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestelling.SelectedValue));
                ctx.SaveChanges();
            }
            UpdatecbBestelling();
            MessageBox.Show("Bestelling is verwijderen");
        }
        private void UpdatecbBestelling()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                cbBestelling.ItemsSource = null;
                var listBestellingen = ctx.Bestelling.Join(ctx.Klant,
                    b => b.KlantID,
                    k => k.KlantID,
                    (b, k) => new { b, k, Naam = k.Voornaam + "" + k.Achternaam + "" + k.AangemaaktOp, ID = b.BestellingID });

                cbBestelling.DisplayMemberPath = "Naam";
                cbBestelling.SelectedValuePath = "ID";
                cbBestelling.ItemsSource = listBestellingen.ToList();
                cbBestelling.SelectedIndex = 0;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedBestelling = ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestelling.SelectedValue);
                selectedBestelling.DatumOpgemaakt = (DateTime)dtDatumOpgemaakt.SelectedDate;
                selectedBestelling.PersoneelslidID = (int)cbPersoneelslid.SelectedValue;
                selectedBestelling.LeverancierID = (int)cbLeverancier.SelectedValue;
                selectedBestelling.KlantID = (int)cbKlant.SelectedValue;
                ctx.SaveChanges();
            }
            MessageBox.Show("Bestelling Bewerk is gedaan");
            UpdatecbBestelling();
        }

        private void chbEdit_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)chbEdit.IsChecked)
            {
                cbBestelling.IsEnabled = true;
                btnBekijk.IsEnabled = false;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnAdd.IsEnabled = false;
                UpdatecbBestelling();
            }

        }

        private void chbEdit_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((bool)!chbEdit.IsChecked)
            {
                cbBestelling.IsEnabled = false;
                btnBekijk.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAdd.IsEnabled = true;

            }
        }

        private void chbBekijk_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((bool)!chbBekijk.IsChecked)
            {
                cbBestelling.IsEnabled = false;
                btnBekijk.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAdd.IsEnabled = true;
                UpdatecbBestelling();
            }
        }

        private void chbBekijk_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)chbBekijk.IsChecked)
            {
                cbBestelling.IsEnabled = true;
                btnBekijk.IsEnabled = true;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAdd.IsEnabled = false;
                UpdatecbBestelling();
            }

        }

        private void btnBekijk_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                if (cbBestelling.SelectedValue != null)
                {
                    if (ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestelling.SelectedValue) != null)
                    {
                        var selectedBestelling = ctx.Bestelling.Single(b => b.BestellingID == (int)cbBestelling.SelectedValue);
                        dtDatumOpgemaakt.SelectedDate = selectedBestelling.DatumOpgemaakt;
                        cbPersoneelslid.SelectedValue = selectedBestelling.PersoneelslidID;
                        cbLeverancier.SelectedValue = selectedBestelling.LeverancierID;
                        cbKlant.SelectedValue = selectedBestelling.KlantID;
                    }
                }
            }
        }
    }    
}
