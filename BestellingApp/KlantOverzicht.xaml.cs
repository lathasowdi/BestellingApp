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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BestellingApp
{
    /// <summary>
    /// Interaction logic for KlantOverzicht.xaml
    /// </summary>
    public partial class KlantOverzicht : Window
    {
        public KlantOverzicht()
        {
            InitializeComponent();
            loadlistview();
        }
        private void loadlistview()
        {
            BestellingenEntities ctx = new BestellingenEntities();
            var klant = ctx.Klant.ToList();
            listgrid.ItemsSource = klant;
            List<string> sortLeverancier = new List<string>()
        {"Voornaam Up",
        "Voornaam Down",
        "Achternaam Up",
        "Achternaam Down",
        "Gemeente Up",
        "Gemeente Down",
        "Straatnaam Up",
        "Straatnaam Down",
        };
            CbSort.ItemsSource = sortLeverancier;
           
           
           
           lbklant.ItemsSource = klant;
            
            lbklant.SelectedValuePath = "KlantID";

        }
        public void UpdateQuery()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                IQueryable<Klant> klant;


                switch (CbSort.SelectedItem)
                {
                    case "Voornaam Down":
                        klant = ctx.Klant.Select(b => b).OrderByDescending(x => x.Voornaam);
                        break;
                    case "Voornaam  Up":
                        klant = ctx.Klant.Select(b => b).OrderBy(x => x.Voornaam);
                        break;
                    case "Achternaam Down":
                        klant = ctx.Klant.Select(b => b).OrderByDescending(x => x.Achternaam);
                        break;
                    case "Achternaam  Up":
                        klant = ctx.Klant.Select(b => b).OrderBy(x => x.Achternaam);
                        break;
                    case "Gemeente Down":
                        klant = ctx.Klant.Select(b => b).OrderByDescending(x => x.Gemeente);
                        break;
                    case "Gemeente Up":
                        klant = ctx.Klant.Select(b => b).OrderBy(x => x.Gemeente);
                        break;
                    case "Straatnaam Down":
                        klant = ctx.Klant.Select(b => b).OrderByDescending(x => x.Straatnaam);
                        break;
                    case "Straatnaam Up":
                        klant = ctx.Klant.Select(b => b).OrderBy(x => x.Straatnaam);
                        break;
                    default:
                        klant = ctx.Klant.Select(b => b);
                        break;
                };
                
                lbklant.ItemsSource = klant.ToList();
            }
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbSort.SelectedValue != null)
            {
                lbklant.UnselectAll();
                UpdateQuery();
            }
               
            
        }

        private void lbklant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbklant.SelectedValue != null)
            {
                int klantId = Convert.ToInt32(lbklant.SelectedValue);


                BestellingenEntities ctx = new BestellingenEntities();
                var klant = ctx.Klant.Select(x => x).Where(x => x.KlantID == klantId).FirstOrDefault(); ;


                string beschrijf = "";
                beschrijf =
                      $"VOORNAAM:{klant.Voornaam}" + "\n"
                    + $"ACHTERNAAM:{klant.Achternaam}" + "\n"
                    + $"STRAATNAAM:{klant.Straatnaam}" + "\n"
                    + $"HUISNUMMER:{klant.Huisnummer}" + "\n"
                    + $"BUS:{klant.Bus}" + "\n"
                    + $"POSTCODE:{klant.Postcode}" + "\n"
                    + $"GEMEENTE:{klant.Gemeente}" + "\n"
                    + $"TELEFOONNUMMER:{klant.Telefoonnummer}" + "\n"
                    + $"E-MAIL:{klant.Emailadres}" + "\n"
                    + $"AANGEMAAKTOP:{klant.AangemaaktOp}" + "\n"
                    + $"OPMERKING:{klant.Opmerking}" + "\n";


                if (lbklant.Items != null)
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
