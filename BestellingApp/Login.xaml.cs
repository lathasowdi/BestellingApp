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
using System.Data.SqlClient;
using System.Security.Cryptography;

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
            tbUsernaam.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usernaam = "";
            if (tbUsernaam.Text.Trim()== "")
            {
                MessageBox.Show("Geef Usernaam a.u.b",
                "ALERT",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning);

            }
            else
            {
                usernaam = tbUsernaam.Text.Trim();
            }
            string wachtwoord = "";
            if (tbWachtwoord.Password.Trim() == "")
            {
                MessageBox.Show("Geef Wachtwoord a.u.b",
                "ALERT",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning);

            }
            else
            {
                wachtwoord = tbWachtwoord.Password.Trim();
             
            }
            if (usernaam != null && wachtwoord != null)
            {
                using (BestellingenEntities ctx = new BestellingenEntities())
                {
                    Personeelslid loggedin = ctx.Personeelslid.Where(p => p.Usernaam == usernaam).FirstOrDefault();
                    if (loggedin != null)
                    {
                        if (loggedin.Wachtwoord == wachtwoord)
                        {
                            MainMenu mainMenu = new MainMenu(loggedin);

                            mainMenu.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(" Wachtwoord is verkeerd!", "ALERT",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(" Usernaam is verkeerd!", "ALERT",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);
                    }
                }
            }
            
        }

        private void tbWachtwoord_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
