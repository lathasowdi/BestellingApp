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
    /// Interaction logic for CategorieOverzicht.xaml
    /// </summary>
    public partial class CategorieOverzicht : Window
    {
        public CategorieOverzicht()
        {
            InitializeComponent();
            BestellingenEntities ctx = new BestellingenEntities();
            listgrid.ItemsSource = ctx.Categorie.ToList();
            List<string> sortLeverancier = new List<string>()
        {"naam Up",
        "naam Down",
        };
           cbSort.ItemsSource = sortLeverancier;

            var categorie = ctx.Categorie.ToList();

           lbCategorie.ItemsSource = categorie;
        }
        public void UpdateQuery()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                IQueryable<Categorie> categorie;


                switch (cbSort.SelectedItem)
                {
                    case "naam Down":
                        categorie = ctx.Categorie.Select(b => b).OrderByDescending(x => x.CategorieNaam);
                        break;
                    case "naam Up":
                        categorie = ctx.Categorie.Select(b => b).OrderBy(x => x.CategorieNaam);
                        break;
                    
                    default:
                        categorie = ctx.Categorie.Select(b => b);
                        break;
                };

                lbCategorie.ItemsSource = categorie.ToList();
            }
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateQuery();
        }
    }
}
