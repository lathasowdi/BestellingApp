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
    /// Interaction logic for CategorieEdit.xaml
    /// </summary>
    public partial class CategorieEdit : Window
    {
        public CategorieEdit()
        {
            InitializeComponent();
            cbboxupdate();


        }
        private void cbboxupdate()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Categoriequery = ctx.Categorie.Select(k => k);

                cbCategorie.ItemsSource = Categoriequery.ToList();
                cbCategorie.SelectedIndex = 0;

            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                string categorienaam = "";
                if (tbCategorienaam.Text.Trim() != "")
                {
                    categorienaam = tbCategorienaam.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef categorienaam a.u.b");
                }
                var selectedcategorienaam = (Categorie)cbCategorie.SelectedItem;
                ctx.Categorie.Where(p => p.CategorieID == selectedcategorienaam.CategorieID).FirstOrDefault().CategorieNaam= categorienaam;
                ctx.SaveChanges();
                cbboxupdate();
                MessageBox.Show("CategorieNaam is Verandered");
                   
            }
        }
    }
}
