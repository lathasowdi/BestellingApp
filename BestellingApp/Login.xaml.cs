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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BestellingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tbUsernaam_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbUsernaam.Text=="")
            {
                MessageBox.Show("Geef Usernaam a.u.b");
            }
        }

        private void tbPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbPass.Text=="")
            {
                MessageBox.Show("Geef Wachtwoord a.u.b");
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
           
                string wachtwoord = tbPass.Text;
                string usernaam = tbUsernaam.Text;
                using (BestellingenEntities ctx = new BestellingenEntities())
                {
                    var check = ctx.Personeelslid.Where(p => p.Usernaam == usernaam && p.Wachtwoord == wachtwoord).Count();
                    if (check == 1)
                    {
                        Personeelslid loggedin = ctx.Personeelslid.Where(p => p.Usernaam == usernaam).FirstOrDefault();
                       MainMenu mainMenu = new MainMenu(loggedin);
                        mainMenu.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Gebruikersnaam of wachtwoord verkeerd!");
                    }
                }
            
        }
    }
}
