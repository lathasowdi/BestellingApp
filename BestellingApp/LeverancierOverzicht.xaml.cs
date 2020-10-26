using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for LeverancierOverzicht.xaml
    /// </summary>
    public partial class LeverancierOverzicht : Window
    {
        public LeverancierOverzicht()
        {
            InitializeComponent();
            loadlistview();
        }
        private void loadlistview()
        {
            BestellingenEntities ctx = new BestellingenEntities();
            var leveranciers = ctx.Leverancier.ToList();
            listgrid.ItemsSource = leveranciers;
        //    Dictionary<string, string> sortLeverancier = new Dictionary<string, string>()
        //{
           
        //    {"ContactPersoon Up","cUp" },
        //    {"ContactPersoon Down","cDown" },
        //    {"Straatnaam Up","sUp" },
        //    {"Straatnaam Down","sDown" },
        //    {"Gemeente Up","gUp" },
        //    {"Gemeente Down","gDown" },
        //};
        //    List<string>sortLeverancier = new List<string>()
        //{"ContactPersoon Up",
        //"ContactPersoon Down",
        //"Straatnaam Up",
        //"Straatnaam Down",
        //"Gemeente Up",
        //"Gemeente Down"
        //};
           
            List<string> sortLeverancier = new List<string>()
        {"ContactPersoon Up",
        "ContactPersoon Down",
        "Straatnaam Up",
        "Straatnaam Down",
        "Gemeente Up",
        "Gemeente Down"
        };
            cbSort.ItemsSource = sortLeverancier;

            

            lbleveranciers.ItemsSource = leveranciers;

            lbleveranciers.SelectedValuePath = "LeverancierID";

        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listgrid.ItemsSource);

            //switch (CbSort.SelectedItem)
            //{


            //    case "ContactPersoon Up":
            //        view.SortDescriptions.Add(new SortDescription("Contactpersoon", ListSortDirection.Ascending));
            //        break;
            //    case "ContactPersoon Down":
            //        view.SortDescriptions.Add(new SortDescription("Contactpersoon", ListSortDirection.Descending));
            //        break;
            //    case "Straatnaam Up":
            //        view.SortDescriptions.Add(new SortDescription("Straatnaam", ListSortDirection.Ascending));
            //        break;
            //    case "Straatnaam Down":
            //        view.SortDescriptions.Add(new SortDescription("Straatnaam", ListSortDirection.Descending));
            //        break;
            //    case "Gemeente Up":
            //        view.SortDescriptions.Add(new SortDescription("Gemeente", ListSortDirection.Ascending));
            //        break;
            //    case "Gemeente Down":
            //        view.SortDescriptions.Add(new SortDescription("Gemeente", ListSortDirection.Descending));
            //        break;
            if (cbSort.SelectedValue != null)
            {
                lbleveranciers.UnselectAll();
                UpdateQuery();
            }



        }
        public void UpdateQuery()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                IQueryable<Leverancier> leveranciers;


                switch (cbSort.SelectedItem)
                {
                    case "ContactPersoon Down":
                        leveranciers = ctx.Leverancier.Select(b => b).OrderByDescending(x => x.Contactpersoon);
                        break;
                    case "ContactPersoon Up":
                        leveranciers = ctx.Leverancier.Select(b => b).OrderBy(x => x.Contactpersoon);
                        break;
                    case "Straatnaam Down":
                        leveranciers = ctx.Leverancier.Select(b => b).OrderByDescending(x => x.Straatnaam);
                        break;
                    case "Straatnaam Up":
                        leveranciers = ctx.Leverancier.Select(b => b).OrderBy(x => x.Straatnaam);
                        break;
                    case "Gemeente Down":
                        leveranciers = ctx.Leverancier.Select(b => b).OrderByDescending(x => x.Gemeente);
                        break;
                    case "Gemeente Up":
                        leveranciers = ctx.Leverancier.Select(b => b).OrderBy(x => x.Gemeente);
                        break;
                    default:
                        leveranciers = ctx.Leverancier.Select(b => b);
                        break;
                };

                lbleveranciers.ItemsSource = leveranciers.ToList();
            }
        }

        private void lbleveranciers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbleveranciers.SelectedValue != null)
            {
                int leverancierID = Convert.ToInt32(lbleveranciers.SelectedValue);


                BestellingenEntities ctx = new BestellingenEntities();
                var leverancier = ctx.Leverancier.Select(x => x).Where(x => x.LeverancierID == leverancierID).FirstOrDefault();


                string beschrijf = "";
                beschrijf =
                      $"CONTACTPERSOON:{leverancier.Contactpersoon}" + "\n"
                    + $"TELEFOON:{leverancier.Telefoonnummer}" + "\n"
                    + $"E-MAIL:{leverancier.Emailadres}" + "\n"
                    + $"STRAATNAAM:{leverancier.Straatnaam}" + "\n"
                    + $"HUISNUMMER:{leverancier.Huisnummer}" + "\n"
                    + $"BUS:{leverancier.Bus}" + "\n"
                    + $"POSTCODE:{leverancier.Postcode}" + "\n"
                    + $"GEMEENTE:{leverancier.Gemeente}" + "\n";
                   


                if (lbleveranciers.Items != null)
                {
                    lblLijst.Content
                        = beschrijf;

                }
                else
                {
                    lblLijst.Content = "";
                }
            }
        }
    }
    
}
