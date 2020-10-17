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
    /// Interaction logic for LeverancierEdit.xaml
    /// </summary>
    public partial class LeverancierEdit : Window
    {
        public LeverancierEdit()
        {
            InitializeComponent();
            upcbLeverancier();


        }
        private void upcbLeverancier()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Leverancierquery = ctx.Leverancier.Select(k => k);

                cbLeverancier.ItemsSource = Leverancierquery.ToList();

            }
        }

        private void btnBewerken_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                string contactperson = "";
                if (tbContactperson.Text.Trim() != "")
                {
                    contactperson = tbContactperson.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef ContactPerson a.u.b");
                }
                string telefoon = "";
                if (tbTelefoon.Text.Trim() != "")
                {
                    telefoon = tbTelefoon.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef TelefoonNummer a.u.b");
                }
                string email = "";
                if (tbEmail.Text.Trim() != "")
                {
                    email = tbEmail.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Email a.u.b");
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
                if (tbpostcode.Text.Trim() != "")
                {
                    postcode = tbpostcode.Text.Trim();
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
                var selectedLeverancier = (Leverancier)cbLeverancier.SelectedItem;
                ctx.Leverancier.Where(p => p.LeverancierID == selectedLeverancier.LeverancierID).FirstOrDefault().Contactpersoon = contactperson;
                ctx.Leverancier.Where(p => p.LeverancierID == selectedLeverancier.LeverancierID).FirstOrDefault().Telefoonnummer = telefoon;
                ctx.Leverancier.Where(p => p.LeverancierID == selectedLeverancier.LeverancierID).FirstOrDefault().Emailadres = email;
                ctx.Leverancier.Where(p => p.LeverancierID == selectedLeverancier.LeverancierID).FirstOrDefault().Straatnaam = straatnaam;
                ctx.Leverancier.Where(p => p.LeverancierID == selectedLeverancier.LeverancierID).FirstOrDefault().Huisnummer = huisnummer;
                ctx.Leverancier.Where(p => p.LeverancierID == selectedLeverancier.LeverancierID).FirstOrDefault().Bus = bus;
                ctx.Leverancier.Where(p => p.LeverancierID == selectedLeverancier.LeverancierID).FirstOrDefault().Postcode = postcode;
                ctx.Leverancier.Where(p => p.LeverancierID == selectedLeverancier.LeverancierID).FirstOrDefault().Gemeente = Gemeente;
                ctx.Leverancier.Where(p => p.LeverancierID == selectedLeverancier.LeverancierID).FirstOrDefault().Telefoonnummer = telefoon;
                ctx.SaveChanges();
                upcbLeverancier();
                MessageBox.Show("Leverancier Bewerken is gedaan!", "INFO",
                     MessageBoxButton.OKCancel,
                     MessageBoxImage.Information);

            }
        }
            private void cbLeverancier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedLeverancier = (Leverancier)cbLeverancier.SelectedItem;
                tbContactperson.Text = selectedLeverancier.Contactpersoon;
                tbTelefoon.Text = selectedLeverancier.Telefoonnummer;
                tbEmail.Text = selectedLeverancier.Emailadres;
                tbStraatnaam.Text = selectedLeverancier.Straatnaam;
                tbHuisnummer.Text = selectedLeverancier.Huisnummer.ToString();
                tbBus.Text = selectedLeverancier.Bus;
               tbpostcode.Text = selectedLeverancier.Postcode;
                tbGemeente.Text = selectedLeverancier.Gemeente;
                
            }
        }
    }
}
