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
    /// Interaction logic for PersoneelAdd.xaml
    /// </summary>
    public partial class PersoneelAdd : Window
    {
        public PersoneelAdd()
        {
            InitializeComponent();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var FunctieQuery = ctx.Functie.Select(x => x);
               
                cbFunctie.ItemsSource = FunctieQuery.ToList();
                cbFunctie.DisplayMemberPath = "FunctieTitel";
                cbFunctie .SelectedValuePath = "FunctieID";
                cbFunctie.SelectedIndex = 0;
            }
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                string voornaam= "";
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
                
               Personeelslid nieuwepersoneelslid = new Personeelslid();
                nieuwepersoneelslid.Voornaam = voornaam;
                nieuwepersoneelslid.Achternaam = achternaam;
                nieuwepersoneelslid.FunctieID= functieID;
                nieuwepersoneelslid.Usernaam= usernaam;
                nieuwepersoneelslid.Wachtwoord = wachtwoord;
                ctx.Personeelslid.Add(nieuwepersoneelslid);
                ctx.SaveChanges();
                MessageBox.Show("Personeelslid Toevoegd");
            }
        }
    }
    
}
