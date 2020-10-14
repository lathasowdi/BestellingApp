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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public Personeelslid loggedinpersoneelid=new Personeelslid();
        public MainMenu(Personeelslid loggedin)
        {
            loggedinpersoneelid = loggedin;
            //if (loggedinpersoneelid.FunctieID == 1)
            //{
            //    tabDatabeheer.IsEnabled = true;
            //    tabDatabeheer.Visibility = Visibility.Visible;
            //}
            //else 
            //{
            //    tabDatabeheer.IsEnabled = false;
            //    tabDatabeheer.Visibility = Visibility.Hidden;
                
            //}
            InitializeComponent();
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
    }
}
