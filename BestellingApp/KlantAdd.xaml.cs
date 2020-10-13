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
    /// Interaction logic for KlantBeheer.xaml
    /// </summary>
    public partial class KlantBeheer : Window
    {
        public KlantBeheer()
        {
            InitializeComponent();
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
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
                Klant nieuweklant = new Klant();
                nieuweklant.Voornaam = voornaam;
                nieuweklant.Achternaam = achternaam;
                nieuweklant.Straatnaam = straatnaam;
                nieuweklant.Huisnummer = huisnummer;
                nieuweklant.Bus = bus;
                nieuweklant.Postcode = postcode;
                nieuweklant.Gemeente = Gemeente;
                nieuweklant.Telefoonnummer = telefoon;
                nieuweklant.Emailadres = email;
                nieuweklant.AangemaaktOp = datum;
                nieuweklant.Opmerking = opmerking;
                ctx.Klant.Add(nieuweklant);
                ctx.SaveChanges();
                MessageBox.Show("klant Toevoegd");
            }
        }
    }
}
