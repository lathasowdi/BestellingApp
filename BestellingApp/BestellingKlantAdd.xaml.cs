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
using Word = Microsoft.Office.Interop.Word;
using Xceed.Words.NET;
using System.Xml;
using Xceed.Document.NET;
using Microsoft.Office.Interop.Word;
using System.IO;


namespace BestellingApp
{
    /// <summary>
    /// Interaction logic for BestellingKlantAdd.xaml
    /// </summary>
    public partial class BestellingKlantAdd : System.Windows.Window
    {
        public Personeelslid loggedinpersoneelid { get; set; }


        public BestellingKlantAdd(Personeelslid loggedin)
        {
            InitializeComponent();
            loggedinpersoneelid = loggedin;
            UpdatecbCategorie();
            Updatecbklant();
            UpdatecbProduct();
            UpdatecbBestellingKlant();
        }

        private void Updatecbklant()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                cbKlant.ItemsSource = null;
                var Klantquery = ctx.Klant.Select(x => new { Naam = x.Voornaam + " " + x.Achternaam, Id = x.KlantID }).ToList();
                cbKlant.DisplayMemberPath = "Naam";
                cbKlant.SelectedValuePath = "Id";
                cbKlant.ItemsSource = Klantquery;
                cbKlant.SelectedIndex = -1;
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
        private void UpdatecbProduct()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Productquery = ctx.Product.Select(k => k).ToList();
                cbProduct.DisplayMemberPath = "Naam";
                cbProduct.SelectedValuePath = "ProductID";
                cbProduct.ItemsSource = Productquery;
                cbProduct.SelectedIndex = -1;
            }
        }
        public List<klantBesteling> KlantProductLijst = new List<klantBesteling>();

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ErrorHandling();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedProduct = ctx.Product.Single(p => p.ProductID == (int)cbProduct.SelectedValue);
                klantBesteling product = new klantBesteling(selectedProduct.ProductID, selectedProduct.Naam, Convert.ToInt32(tbAantal.Text), (double)selectedProduct.InKoopprijs, (double)selectedProduct.Marge);
                KlantProductLijst.Add(product);
                UpdatelbLijst();
            }
        }
        public void UpdatelbLijst()
        {
            lbLijst.ItemsSource = null;
            lbLijst.ItemsSource = KlantProductLijst;
            lbLijst.DisplayMemberPath = "Naam";
            lbLijst.SelectedValuePath = "ProductID";

        }
        public class klantBesteling
        {
            public int ProductID { get; set; }
            public string Naam { get; set; }
            public int Aantal { get; set; }
            public double InKoopprijs { get; set; }
            public double Marge { get; set; }

            public klantBesteling(int productID, string naam, int aantal, double inKoopprijs, double marge)
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


            if (cbKlant.SelectedIndex < 0)
            {
                errorshow += "\r\n" + "Select een Klant a.u.b";
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

        private void btnBestel_Click(object sender, RoutedEventArgs e)
        {
            if (KlantProductLijst.Count > 0)
            {
                using (BestellingenEntities ctx = new BestellingenEntities())
                {
                    Bestelling bestelling = new Bestelling();
                    bestelling.DatumOpgemaakt = DateTime.Now;
                    bestelling.PersoneelslidID = loggedinpersoneelid.PersoneelslidID;
                    bestelling.KlantID = (int)cbKlant.SelectedValue;
                    ctx.Bestelling.Add(bestelling);
                    ctx.SaveChanges();
                    foreach (var item in KlantProductLijst)
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
            UpdatecbBestellingKlant();
            KlantProductLijst.Clear();
            lbLijst.ItemsSource = null;
            tbAantal.Clear();
            cbProduct.SelectedIndex = -1;
            cbCategorie.SelectedIndex = -1;
            cbKlant.SelectedIndex = -1;

        }


        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lbLijst.SelectedIndex != -1)
                KlantProductLijst.RemoveAt(lbLijst.SelectedIndex);
            UpdatelbLijst();
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
        public void TotaalPrijs()
        {
            double totaal = 0;
            if (KlantProductLijst.Count > 0)
            {
                foreach (var item in KlantProductLijst)
                {
                    double prijs = item.InKoopprijs + item.Marge;
                    totaal += prijs * item.Aantal;
                }
            }
            tbPrijs.Text = totaal.ToString();
        }
        private void UpdatecbBestellingKlant()
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                cbBestellingKlant.ItemsSource = null;
                var klantBestellingen = ctx.Bestelling.Join(ctx.Klant,
                    b => b.KlantID,
                    k => k.KlantID,
                    (b, k) => new { b, k, Naam = k.Voornaam + "" + k.Achternaam, ID = b.BestellingID });

                cbBestellingKlant.DisplayMemberPath = "Naam";
                cbBestellingKlant.SelectedValuePath = "ID";
                cbBestellingKlant.ItemsSource = klantBestellingen.ToList();
                cbBestellingKlant.SelectedIndex = -1;
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
                int bestellingId = Convert.ToInt32(cbBestellingKlant.SelectedValue);
                var klantBestellingen = ctx.Bestelling.Join(ctx.Klant,
                   b => b.KlantID,
                   k => k.KlantID,
                   (b, k) => new { b, k, Naam = k.Voornaam + "" + k.Achternaam, ID = b.BestellingID }).Where(x => x.b.BestellingID == bestellingId).FirstOrDefault();

                cbBestellingKlant.DisplayMemberPath = "Naam";
                cbBestellingKlant.SelectedValuePath = "ID";
                
                    string SelectedItem = cbBestellingKlant.Text;
                var JoinedQuery = ctx.BestellingProduct.Join(ctx.Product,
                            b => b.ProductID,
                            p => p.ProductID,
                            (b, p) => new { b, p, Naam = p.Naam, ID = p.ProductID }).Where(b => b.b.BestellingID == (int)cbBestellingKlant.SelectedValue).ToList();

                FileStream fileW = new FileStream($"{SelectedItem}.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    // Create a new stream to write to the file
                    StreamWriter sw = new StreamWriter(fileW);
                    // Write a string to the file
                    sw.WriteLine("-----------------------------------------------------------");
                    sw.WriteLine($"NAAM:{klantBestellingen.k.Voornaam+""+ klantBestellingen.k.Achternaam}" + "\n");
                sw.WriteLine($"BESTELLINGID:{klantBestellingen.b.BestellingID}" + "\n");
                sw.WriteLine($"BESTELLINGID:{klantBestellingen.k.KlantID}" + "\n");
                sw.WriteLine($"STRAATNAAM:{klantBestellingen.k.Straatnaam}" + "\n");
                    sw.WriteLine($"HUISNUMMER:{klantBestellingen.k.Huisnummer}" + "\n");
                    sw.WriteLine($"BUS:{klantBestellingen.k.Bus}" + "\n");
                    sw.WriteLine($"POSTCODE:{klantBestellingen.k.Postcode}" + "\n");
                sw.WriteLine($"POSTCODE:{klantBestellingen.k.Gemeente}" + "\n");
                sw.WriteLine($"TELEFOON:{klantBestellingen.k.Telefoonnummer}" + "\n");
                    sw.WriteLine($"E-MAIL:{klantBestellingen.k.Emailadres}" + "\n");
                sw.WriteLine("Product\t\t\tAantal\tPrijPerStuk\tPrij\tBTW%\tPrijsBTW");
                double totaal = 0;
                double totaalbtw = 0;
                foreach (var item in JoinedQuery)
                {
                    double prijs = (double)(item.p.InKoopprijs + item.p.Marge);
                    
                 sw.WriteLine(item.Naam +"\t\t  "+ item.b.Aantal+ "\t\t"+ prijs+"\t"+ prijs*item.b.Aantal+"\t"+item.p.BTW+"\t" +(( item.p.BTW/100)* prijs * item.b.Aantal + (prijs * item.b.Aantal)));
                    
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