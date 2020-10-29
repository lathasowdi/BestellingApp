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
    /// Interaction logic for LeverancierAdd.xaml
    /// </summary>
    public partial class LeverancierAdd : Window
    {
        public LeverancierAdd()
        {
            InitializeComponent();
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
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
                
               Leverancier nieuweLeverancier = new Leverancier();
                nieuweLeverancier.Contactpersoon = contactperson;
                nieuweLeverancier.Telefoonnummer = telefoon;
                nieuweLeverancier.Emailadres = email;
                nieuweLeverancier.Straatnaam = straatnaam;
                nieuweLeverancier.Huisnummer = huisnummer;
                nieuweLeverancier.Bus = bus;
                nieuweLeverancier.Postcode = postcode;
                nieuweLeverancier.Gemeente = Gemeente;
                ctx.Leverancier.Add(nieuweLeverancier);
                ctx.SaveChanges();
                MessageBox.Show("Leverancier is Toegevoegd");
            }
        }
    }
    
}
