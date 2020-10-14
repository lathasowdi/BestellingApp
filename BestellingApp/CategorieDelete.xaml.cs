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
    /// Interaction logic for CategorieDelete.xaml
    /// </summary>
    public partial class CategorieDelete : Window
    {
        public CategorieDelete()
        {
            InitializeComponent();
            updatecombobox();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedCategorie = (Categorie)cbCategorie.SelectedItem;
                ctx.Categorie.RemoveRange(ctx.Categorie.Where(k => k.CategorieID == selectedCategorie.CategorieID));
                ctx.SaveChanges();
            }
            MessageBox.Show("Categorie is verwijderen",
                "INFO",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Exclamation);
            updatecombobox();
        }
        public void updatecombobox()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Categoriequery = ctx.Categorie.Select(k => k);
                cbCategorie.ItemsSource = Categoriequery.ToList();
                cbCategorie.SelectedIndex = 0;
            }
        }
    }
    
}
