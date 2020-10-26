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
using System.IO;


namespace BestellingApp
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public Personeelslid loggedinpersoneelid { get; set; }
       
        public MainMenu(Personeelslid loggedin)
        {
            InitializeComponent();
           
            loggedinpersoneelid = loggedin;
            loginNaam.Content = "WELKOM" + " " + loggedinpersoneelid.Voornaam.ToUpper()+ " " + loggedinpersoneelid.Achternaam.ToUpper();
            if (loggedinpersoneelid.FunctieID== 1)
            {
                tabKlant.IsSelected = true;
                tabKlant.Visibility = Visibility.Visible;
                tabProduct.IsSelected = true;
                tabProduct.Visibility = Visibility.Visible;
                tabLeverancier.IsSelected = true;
                tabLeverancier.Visibility = Visibility.Visible;
                tabPersoneel.IsSelected = true;
                tabPersoneel.Visibility = Visibility.Visible;
                tabCategorie.IsSelected = true;
                tabCategorie.Visibility = Visibility.Visible;
            }
            else if(loggedinpersoneelid.FunctieID==2)
            {
                tabKlant.IsSelected = false;
                tabKlant.Visibility = Visibility.Hidden;
                tabProduct.IsSelected = false;
                tabProduct.Visibility = Visibility.Hidden;
                tabLeverancier.IsSelected = true;
                tabLeverancier.Visibility = Visibility.Visible;
                tabPersoneel.IsSelected = false;
                tabPersoneel.Visibility = Visibility.Hidden;
                tabCategorie.IsSelected = false;
                tabCategorie.Visibility = Visibility.Hidden;
                tabKlantBestelling.IsSelected = false;
                tabKlantBestelling.Visibility = Visibility.Hidden;
                btnProductOverzicht.IsEnabled = false;
                btnProductOverzicht.Visibility = Visibility.Hidden;
                btnPersoneellidsoverzicht.IsEnabled = false;
                btnPersoneellidsoverzicht.Visibility = Visibility.Hidden;
                btnKlantOverzicht.IsEnabled = false;
                btnKlantOverzicht.Visibility = Visibility.Hidden;
                btnCategorieOverzicht.IsEnabled = false;
                btnCategorieOverzicht.Visibility = Visibility.Hidden;
                tabKlantBestelling.IsEnabled = false;
                tabKlantBestelling.Visibility = Visibility.Hidden;
                btnKlantBestellingToevoegen.IsEnabled = false;
                btnKlantBestellingToevoegen.Visibility = Visibility.Hidden;
                btnklantBestellingBewerken.IsEnabled = false;
                btnklantBestellingBewerken.Visibility = Visibility.Hidden;

            }
            else
            {
                tabKlant.IsSelected = true;
                tabKlant.Visibility = Visibility.Visible;
                tabProduct.IsSelected = false;
                tabProduct.Visibility = Visibility.Hidden;
                tabLeverancier.IsSelected =false;
                tabLeverancier.Visibility = Visibility.Hidden;
                tabPersoneel.IsSelected = false;
                tabPersoneel.Visibility = Visibility.Hidden;
                tabCategorie.IsSelected = false;
                tabCategorie.Visibility = Visibility.Hidden;
                tabLeverancierBestelling.IsSelected = false;
                tabLeverancierBestelling.Visibility = Visibility.Hidden;
                btnProductOverzicht.IsEnabled = false;
                btnProductOverzicht.Visibility = Visibility.Hidden;
                btnPersoneellidsoverzicht.IsEnabled = false;
                btnPersoneellidsoverzicht.Visibility = Visibility.Hidden;
                btnLeverancierOverzicht.IsEnabled = false;
                btnLeverancierOverzicht.Visibility = Visibility.Hidden;
                btnCategorieOverzicht.IsEnabled = false;
                btnCategorieOverzicht.Visibility = Visibility.Hidden;
                tabLeverancierBestelling.IsEnabled = false;
                tabLeverancierBestelling.Visibility = Visibility.Hidden;
                btnLeverancierBestellingToevoegen.IsEnabled = false;
                btnLeverancierBestellingBewerken.IsEnabled = false;
                btnLeverancierBestellingToevoegen.Visibility = Visibility.Hidden;
                btnLeverancierBestellingBewerken.Visibility = Visibility.Hidden;
                tabAanpassen.IsEnabled = false;
                tabAanpassen.Visibility = Visibility.Hidden;
                btnCreate.IsEnabled = false;
                btnCreate.Visibility = Visibility.Hidden;
                btnEdit.IsEnabled = false;
                btnEdit.Visibility = Visibility.Hidden;

            }
        }
       
        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            KlantBeheer klantBeheer = new KlantBeheer();
            klantBeheer.Show();
        }

        private void btnKlantToevoegen_Click(object sender, RoutedEventArgs e)
        {
            KlantBeheer klantBeheer = new KlantBeheer();
            klantBeheer.Show();
        }

        private void btnklantBewerken_Click(object sender, RoutedEventArgs e)
        {
            KlantEdit klantEdit = new KlantEdit();
            klantEdit.Show();
        }

        private void btnKlantVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            KlantDelete klantDelete = new KlantDelete();
            klantDelete.Show();
        }

        private void btnLeverancierToevoegen_Click(object sender, RoutedEventArgs e)
        {
            LeverancierAdd leverancierAdd = new LeverancierAdd();
            leverancierAdd.Show();
        }

        private void btnLeverancierBewerken_Click(object sender, RoutedEventArgs e)
        {
            LeverancierEdit leverancierEdit = new LeverancierEdit();
            leverancierEdit.Show();

        }

        private void btnLeverancierVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            LeverancierDelete leverancierDelete = new LeverancierDelete();
            leverancierDelete.Show();

        }

        private void btnProductToevoegen_Click(object sender, RoutedEventArgs e)
        {
            ProductAdd productAdd = new ProductAdd();
            productAdd.Show();
        }

        private void btnProductBewerken_Click(object sender, RoutedEventArgs e)
        {
            ProductEdit productEdit = new ProductEdit();
            productEdit.Show();

        }

        private void btnProductVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            ProductDelete productDelete = new ProductDelete();
            productDelete.Show();
        }

        private void btnCategorieToevoegen_Click(object sender, RoutedEventArgs e)
        {
            CategorieAdd categorieAdd = new CategorieAdd();
            categorieAdd.Show();

        }

        private void btnCategorieBewerken_Click(object sender, RoutedEventArgs e)
        {
            CategorieEdit categorieEdit = new CategorieEdit();
            categorieEdit.Show();
        }

        private void btnCategorieVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            CategorieDelete categorieDelete = new CategorieDelete();
            categorieDelete.Show();
        }

        private void btnPersoneelToevoegen_Click(object sender, RoutedEventArgs e)
        {
            PersoneelAdd personeelAdd = new PersoneelAdd();
            personeelAdd.Show();
        }

        private void btnPersoneelBewerken_Click(object sender, RoutedEventArgs e)
        {
            PersoneelEdit personeelEdit = new PersoneelEdit();
            personeelEdit.Show();
        }

        private void btnPersoneelVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            PersoneelDelete personeelDelete = new PersoneelDelete();
            personeelDelete.Show();
        }

        private void btnBestellingToevoegen_Click(object sender, RoutedEventArgs e)
        {
            BestellingAdd bestellingAdd = new BestellingAdd();
            bestellingAdd.Show();
        }


        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void btnLeverancierOverzicht_Click(object sender, RoutedEventArgs e)
        {
            LeverancierOverzicht leverancierOverzicht = new LeverancierOverzicht();
            leverancierOverzicht.Show();
        }

        private void btnKlantOverzicht_Click(object sender, RoutedEventArgs e)
        {
            KlantOverzicht klantOverzicht = new KlantOverzicht();
            klantOverzicht.Show();
        }

        private void btnProductOverzicht_Click(object sender, RoutedEventArgs e)
        {
            ProductOverzicht productOverzicht = new ProductOverzicht();
            productOverzicht.Show();
        }

        private void btnCategorieOverzicht_Click(object sender, RoutedEventArgs e)
        {
            CategorieOverzicht categorieOverzicht = new CategorieOverzicht();
            categorieOverzicht.Show();
        }

        private void btnPersoneellidOverzicht_Click(object sender, RoutedEventArgs e)
        {
            PersoneelslidOverzicht personeelslidOverzicht = new PersoneelslidOverzicht();
            personeelslidOverzicht.Show();
        }

        private void btnKlantBestellingToevoegen_Click(object sender, RoutedEventArgs e)
        {
            BestellingKlantAdd bestellingKlantAdd = new BestellingKlantAdd(loggedinpersoneelid);
            bestellingKlantAdd.Show();
        }

        private void btnklantBestellingBewerken_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnKlantBestellingVerwijderen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLeverancierBestellingToevoegen_Click(object sender, RoutedEventArgs e)
        {
            BestellingLeverancierAdd bestellingLeverancierAdd = new BestellingLeverancierAdd(loggedinpersoneelid);
            bestellingLeverancierAdd.Show();
        }

        private void btnLeverancierBestellingBewerken_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLeverancierBestellingVerwijderen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Template controlTemplate = new Template(loggedinpersoneelid);
            controlTemplate.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
