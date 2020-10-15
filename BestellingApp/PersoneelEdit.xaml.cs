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
    /// Interaction logic for PersoneelEdit.xaml
    /// </summary>
    public partial class PersoneelEdit : Window
    {
        public PersoneelEdit()
        {
            InitializeComponent();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Personeelquery = ctx.Personeelslid.Select(k => k);

               cbPersoneel.ItemsSource = Personeelquery.ToList();

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
                int functieID = (int)cbFunctie.SelectedValue;

                string usernaam = "";
                if (tbUsernaam.Text.Trim() != "")
                {
                    usernaam = tbUsernaam.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Usernaam a.u.b");
                }
                string wachtwoord = "";
                if (tbWachtwoord.Text.Trim() != "")
                {
                    wachtwoord = tbWachtwoord.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Wachtwoord a.u.b");
                }

                var selectedPersoneel = (Personeelslid)cbPersoneel.SelectedItem;
                ctx.Personeelslid.Where(p => p.PersoneelslidID == selectedPersoneel.PersoneelslidID).FirstOrDefault().Voornaam = voornaam;
                ctx.Personeelslid.Where(p => p.PersoneelslidID == selectedPersoneel.PersoneelslidID).FirstOrDefault().Achternaam = achternaam;
                ctx.Personeelslid.Where(p => p.PersoneelslidID == selectedPersoneel.PersoneelslidID).FirstOrDefault().FunctieID = functieID;
                ctx.Personeelslid.Where(p => p.PersoneelslidID == selectedPersoneel.PersoneelslidID).FirstOrDefault().Usernaam = usernaam;
                ctx.Personeelslid.Where(p => p.PersoneelslidID == selectedPersoneel.PersoneelslidID).FirstOrDefault().Wachtwoord = wachtwoord;
                ctx.SaveChanges();
                MessageBox.Show("Personeelslid Bewerken is gedaan!");
            }
        }

        private void cbPersoneel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedPersoneel = (Personeelslid)cbPersoneel.SelectedItem;
                tbVoornaam.Text = selectedPersoneel.Voornaam;
                tbAchternaam.Text = selectedPersoneel.Achternaam;
                tbUsernaam.Text = selectedPersoneel.Usernaam;
                tbWachtwoord.Text = selectedPersoneel.Achternaam;
                cbFunctie.SelectedValue = selectedPersoneel.FunctieID;
            }
        }
    }
}
