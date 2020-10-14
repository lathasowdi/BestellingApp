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
    /// Interaction logic for CategorieAdd.xaml
    /// </summary>
    public partial class CategorieAdd : Window
    {
        public CategorieAdd()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
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
                Categorie nieuweCategorie = new Categorie();
                nieuweCategorie.CategorieNaam = categorienaam;
                ctx.Categorie.Add(nieuweCategorie);
                ctx.SaveChanges();
                MessageBox.Show("Categorie Toevoegd");
            }
        }
    }
}

