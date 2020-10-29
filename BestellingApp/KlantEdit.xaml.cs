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
    /// Interaction logic for KlantEdit.xaml
    /// </summary>
    public partial class KlantEdit : Window
    {
        public KlantEdit()
        {
            InitializeComponent();
            updatecombobox();
        }
        private void updatecombobox()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var klantquery = ctx.Klant.Select(k => k);

                cbklant.ItemsSource = klantquery.ToList();
                cbklant.SelectedIndex = 0;
            }
        }

        private void btnBewerken_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                string voornaam = "";
                if (tbVoornaam.Text.Trim() != "")
                {
                    voornaam = tbVoornaam.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Voornaam a.u.b");
                }
                string achternaam = "";
                if (tbAchternaam.Text.Trim() != "")
                {
                    achternaam = tbAchternaam.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Achternaam a.u.b");
                }
                string straatnaam = "";
                if (tbStraatnaam.Text.Trim() != "")
                {
                    straatnaam = tbStraatnaam.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Straatnaam a.u.b");
                }
                int huisnummer = 0;
                if (tbHuisnummer.Text.Trim() != "")
                {
                    huisnummer = Convert.ToInt32(tbHuisnummer.Text);
                }
                else
                {
                    MessageBox.Show("Geef HuisNummer a.u.b");
                }

                string bus = "";
                if (tbBus.Text.Trim() != "")
                {
                    bus = tbBus.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Bus a.u.b");
                }
                string postcode = "";
                if (tbPostcode.Text.Trim() != "")
                {
                    postcode = tbPostcode.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Postcode a.u.b");
                }
                string Gemeente = "";
                if (tbGemeente.Text.Trim() != "")
                {
                    Gemeente = tbGemeente.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Gemeente a.u.b");
                }
                string telefoon = "";
                if (tbTelefoonnummer.Text.Trim() != "")
                {
                    telefoon = tbTelefoonnummer.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Telefoonnummer a.u.b");
                }
                string email = "";
                if (tbEmail.Text.Trim() != "")
                {
                    email = tbEmail.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef E-mail a.u.b");
                }
                string datum = "";
                if (tbAangemaaktop.Text.Trim() != "")
                {
                    datum = tbAangemaaktop.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Datum a.u.b");
                }
                string opmerking = "";
                if (tbOpmerking.Text.Trim() != "")
                {
                    opmerking = tbOpmerking.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Opmerking a.u.b");
                }
                var selectedklant = (Klant)cbklant.SelectedItem;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().Voornaam = voornaam;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().Achternaam = achternaam;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().Straatnaam = straatnaam;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().Huisnummer = huisnummer;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().Bus= bus;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().Postcode = postcode;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().Gemeente = Gemeente;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().Telefoonnummer = telefoon;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().Emailadres = email;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().AangemaaktOp = datum;
                ctx.Klant.Where(p => p.KlantID == selectedklant.KlantID).FirstOrDefault().Opmerking = opmerking;
                ctx.SaveChanges();
                MessageBox.Show("klant Bewerk is gedaan");
                     
                updatecombobox();
            }
        }

        
        private void cbklant_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedklant = (Klant)cbklant.SelectedItem;
                tbVoornaam.Text = selectedklant.Voornaam;
                tbAchternaam.Text = selectedklant.Achternaam;
                tbStraatnaam.Text = selectedklant.Straatnaam;
                tbHuisnummer.Text = selectedklant.Huisnummer.ToString();
                tbBus.Text = selectedklant.Bus;
                tbPostcode.Text = selectedklant.Postcode;
                tbGemeente.Text = selectedklant.Gemeente;
                tbTelefoonnummer.Text = selectedklant.Telefoonnummer;
                tbEmail.Text = selectedklant.Emailadres;
                tbAangemaaktop.Text = selectedklant.AangemaaktOp;
                tbOpmerking.Text = selectedklant.Opmerking;
            }
        }
    }
}
