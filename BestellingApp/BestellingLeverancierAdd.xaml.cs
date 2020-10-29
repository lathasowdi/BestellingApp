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
    /// Interaction logic for BestellingLeverancierAdd.xaml
    /// </summary>
    public partial class BestellingLeverancierAdd : Window
    {
        public Personeelslid loggedinpersoneelid { get; set; }
           
        public BestellingLeverancierAdd(Personeelslid loggedin)
        {
            InitializeComponent();
            loggedinpersoneelid = loggedin;
            UpdatecbProduct();
            UpdatecbLeverancier();
            UpdatecbCategorie();
            UpdatecbBestellingLeverancier();
        }
        private void UpdatecbBestellingLeverancier()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                cbBestellingLeverancier.ItemsSource = null;
                var LeverancierBestellingen = ctx.Bestelling.Join(ctx.Leverancier,
                    b => b.LeverancierID,
                    k => k.LeverancierID,
                    (b, k) => new { b, k, Naam = k.Contactpersoon, ID = b.BestellingID });

                cbBestellingLeverancier.DisplayMemberPath = "Naam";
                cbBestellingLeverancier.SelectedValuePath = "ID";
                cbBestellingLeverancier.ItemsSource = LeverancierBestellingen.ToList();
                cbBestellingLeverancier.SelectedIndex = -1;
            }
        }
        private void UpdatecbProduct()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Productquery = ctx.Product.Select(k => k).ToList();
                cbProduct.DisplayMemberPath = "Naam";
                cbProduct.SelectedValuePath = "ProductID";
                cbProduct.ItemsSource = Productquery;
                cbProduct.SelectedIndex = 0;
            }
        }
        private void UpdatecbLeverancier()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Leverancierquery = ctx.Leverancier.Select(x => x).ToList();
                cbLeverancier.DisplayMemberPath = "Contactpersoon";
                cbLeverancier.SelectedValuePath = "LeverancierID";
                cbLeverancier.ItemsSource = Leverancierquery;
                cbLeverancier.SelectedIndex = 0;
            }
        }
        public List<LeverancierBesteling> LeverancierProductLijst = new List<LeverancierBesteling>();
       
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ErrorHandling();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedProduct = ctx.Product.Single(p => p.ProductID == (int)cbProduct.SelectedValue);
                LeverancierBesteling product = new LeverancierBesteling(selectedProduct.ProductID, selectedProduct.Naam, Convert.ToInt32(tbAantal.Text), (double)selectedProduct.InKoopprijs, (double)selectedProduct.Marge);
                LeverancierProductLijst.Add(product);
                UpdatelbLijst();
            }
        }
        public void UpdatecbCategorie()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {

                var categorie = ctx.Categorie.Select(x => new
                {
                    Naam = x.CategorieNaam,
                    Id = x.CategorieID
                }).ToList();
                cbCategorie.DisplayMemberPath = "Naam";
                cbCategorie.SelectedValuePath = "Id";
                cbCategorie.ItemsSource = categorie;
            }
        }
        public class LeverancierBesteling
        {
            public int ProductID { get; set; }
            public string Naam { get; set; }
            public int Aantal { get; set; }
            public double InKoopprijs { get; set; }
            public double Marge { get; set; }

            public LeverancierBesteling(int productID, string naam, int aantal, double inKoopprijs, double marge)
            {
                ProductID = productID;
                Naam = naam;
                Aantal = aantal;
                InKoopprijs = inKoopprijs;
                Marge = marge;
            }
        }
        public void ErrorHandling()
        {
            string errorshow = "";

            if (cbProduct.SelectedIndex < 0)
            {
                errorshow += "Select een Product";
            }


            if (cbLeverancier.SelectedIndex < 0)
            {
                errorshow += "\r\n" + "Select een Leverancier a.u.b";
            }

            if (tbAantal.Text.Trim().Length == 0)
            {

                errorshow += "\r\n" + "Geef Aantal a.u.b";
            }


            if (errorshow.Trim().Length > 0)
            {
                MessageBox.Show(errorshow);
            }
        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lbLijst.SelectedIndex != -1)
                LeverancierProductLijst.RemoveAt(lbLijst.SelectedIndex);
            UpdatelbLijst();

        }
        public void UpdatelbLijst()
        {
            lbLijst.ItemsSource = null;
            lbLijst.ItemsSource = LeverancierProductLijst;
            lbLijst.DisplayMemberPath = "Naam";
            lbLijst.SelectedValuePath = "ProductID";

        }

        private void btnBestel_Click(object sender, RoutedEventArgs e)
        {
            if(LeverancierProductLijst.Count > 0)
            {
               
                    using (BestellingenEntities ctx = new BestellingenEntities())
                    {
                        Bestelling bestelling = new Bestelling();
                        bestelling.DatumOpgemaakt = DateTime.Now;
                        bestelling.PersoneelslidID = loggedinpersoneelid.PersoneelslidID;
                        bestelling.LeverancierID = (int)cbLeverancier.SelectedValue;
                        ctx.Bestelling.Add(bestelling);
                        ctx.SaveChanges();
                        foreach (var item in LeverancierProductLijst)
                        {
                            BestellingProduct bestellingProduct = new BestellingProduct();
                            bestellingProduct.BestellingID = bestelling.BestellingID;
                            bestellingProduct.ProductID = item.ProductID;
                            bestellingProduct.Aantal = item.Aantal;
                            ctx.BestellingProduct.Add(bestellingProduct);
                            ctx.SaveChanges();
                        }

                    }
                    MessageBox.Show("Bestelling is Toevoegen");
                }
                TotaalPrijs();
                UpdatecbBestellingLeverancier();
                LeverancierProductLijst.Clear();
                lbLijst.ItemsSource = null;
                tbAantal.Clear();
                cbProduct.SelectedIndex = -1;
                cbCategorie.SelectedIndex = -1;
                cbLeverancier.SelectedIndex = -1;

            }
        public void TotaalPrijs()
        {
            double totaal = 0;
            if (LeverancierProductLijst.Count > 0)
            {
                foreach (var item in LeverancierProductLijst)
                {
                    double prijs = item.InKoopprijs + item.Marge;
                    totaal += prijs * item.Aantal;
                }
            }
            tbPrijs.Text = totaal.ToString();
        }
        private void cbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {

                if (cbCategorie.SelectedIndex >= 0)
                {
                    var productLijst = ctx.Product.Select(x => new
                    {
                        Naam = x.Naam + " (Prijs: " + (x.InKoopprijs + x.Marge) + ")",
                        Id = x.ProductID,
                        CategorieID = x.CategorieID
                    }).Where(p => p.CategorieID == (int)cbCategorie.SelectedValue).ToList();
                    cbProduct.DisplayMemberPath = "Naam";
                    cbProduct.SelectedValuePath = "Id";
                    cbProduct.ItemsSource = productLijst;
                }
            }
        }

        private void btnRapport_Click(object sender, RoutedEventArgs e)
        {
            string BestandsLocatie = Environment.CurrentDirectory;
            if (!Directory.Exists(BestandsLocatie))
            {
                Directory.CreateDirectory(BestandsLocatie);
            }
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                int bestellingId = Convert.ToInt32(cbBestellingLeverancier.SelectedValue);
                var LeverancierBestellingen = ctx.Bestelling.Join(ctx.Leverancier,
                   b => b.LeverancierID,
                   k => k.LeverancierID,
                   (b, k) => new { b, k, Naam = k.Contactpersoon, ID = b.BestellingID }).Where(x => x.b.BestellingID == bestellingId).FirstOrDefault();

                cbBestellingLeverancier.DisplayMemberPath = "Naam";
                cbBestellingLeverancier.SelectedValuePath = "ID";

                string SelectedItem = cbBestellingLeverancier.Text;
                var JoinedQuery = ctx.BestellingProduct.Join(ctx.Product,
                            b => b.ProductID,
                            p => p.ProductID,
                            (b, p) => new { b, p, Naam = p.Naam, ID = p.ProductID }).Where(b => b.b.BestellingID == (int)cbBestellingLeverancier.SelectedValue).ToList();

                FileStream fileW = new FileStream($"{SelectedItem}.txt", FileMode.OpenOrCreate, FileAccess.Write);
                // Create a new stream to write to the file
                StreamWriter sw = new StreamWriter(fileW);
                // Write a string to the file
                sw.WriteLine("-----------------------------------------------------------");
                sw.WriteLine($"NAAM:{LeverancierBestellingen.k.Contactpersoon}" + "\n");
                sw.WriteLine($"BESTELLINGID:{LeverancierBestellingen.b.BestellingID}" + "\n");
                sw.WriteLine($"BESTELLINGID:{LeverancierBestellingen.k.LeverancierID}" + "\n");
                sw.WriteLine($"STRAATNAAM:{LeverancierBestellingen.k.Straatnaam}" + "\n");
                sw.WriteLine($"HUISNUMMER:{LeverancierBestellingen.k.Huisnummer}" + "\n");
                sw.WriteLine($"BUS:{LeverancierBestellingen.k.Bus}" + "\n");
                sw.WriteLine($"POSTCODE:{LeverancierBestellingen.k.Postcode}" + "\n");
                sw.WriteLine($"POSTCODE:{LeverancierBestellingen.k.Gemeente}" + "\n");
                sw.WriteLine($"TELEFOON:{LeverancierBestellingen.k.Telefoonnummer}" + "\n");
                sw.WriteLine($"E-MAIL:{LeverancierBestellingen.k.Emailadres}" + "\n");
                sw.WriteLine("Product\t\t\tAantal\tPrijPerStuk\tPrij\tBTW%\tPrijsBTW");
                double totaal = 0;
                double totaalbtw = 0;
                foreach (var item in JoinedQuery)
                {
                    double prijs = (double)(item.p.InKoopprijs + item.p.Marge);

                    sw.WriteLine(item.Naam + "\t\t  " + item.b.Aantal + "\t\t" + prijs + "\t" + prijs * item.b.Aantal + "\t" + item.p.BTW + "\t" + ((item.p.BTW / 100) * prijs * item.b.Aantal + (prijs * item.b.Aantal)));

                    totaal += prijs * (double)item.b.Aantal;
                    totaalbtw += ((double)(item.p.BTW / 100) * prijs * (double)item.b.Aantal) + (double)(prijs * item.b.Aantal);
                }

                sw.WriteLine($"                                          TOTAALPRIJS:{totaal}" + "\n");

                sw.WriteLine($"                                         TOTAALPRIJSINCLUSIEFBTW:{totaalbtw}" + "\n");

                sw.WriteLine($"                       Alvast Bedankt voor Bestelling" + "\n");
                sw.WriteLine($"                           Tot Volgende Keer" + "\n");
                sw.Flush();
                // Close StreamWriter
                sw.Close();
                // Close file
                fileW.Close();
                MessageBox.Show("Bestelling is opgeslagen");
            }

        }
    }
}
    


