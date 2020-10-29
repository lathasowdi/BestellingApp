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
    /// Interaction logic for Template.xaml
    /// </summary>
    public partial class Template : Window
    {
        public Personeelslid loggedinpersoneelid { get; set; }
        public Template(Personeelslid loggedin)
        {
            InitializeComponent();
            loggedinpersoneelid = loggedin;
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                cbProduct.ItemsSource = null;
                var Productquery = ctx.Product.Select(k => k).ToList();
                //i/*nt productId = Convert.ToInt32(cbProduct.SelectedValue);*/
                //var productList = ctx.Product.Join(
                //           ctx.Categorie,
                //           p => p.CategorieID,
                //           c => c.CategorieID,
                //           (p, c) => new { p, c }).Join(
                //           ctx.Leverancier,
                //           pc => pc.p.LeverancierID,
                //           l => l.LeverancierID,
                //           (pc, l) => new { pc, l }).ToList();
                cbProduct.DisplayMemberPath = "Naam";
                cbProduct.SelectedValuePath = "ProductID";
                cbProduct.ItemsSource = Productquery;
                cbProduct.SelectedIndex = -1;

            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        { // *** Write to file ***  using (StreamWriter writer = new StreamWriter($"{txtFileName.Text}.txt"))
          // Specify file, instructions, and privelegdes
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                int productId = Convert.ToInt32(cbProduct.SelectedValue);
                var productList = ctx.Product.Join(
                          ctx.Categorie,
                          p => p.CategorieID,
                          c => c.CategorieID,
                          (p, c) => new { p, c }).Join(
                          ctx.Leverancier,
                          pc => pc.p.LeverancierID,
                          l => l.LeverancierID,
                          (pc, l) => new { pc, l }).Where(x => x.pc.p.ProductID == productId).FirstOrDefault();
                string SelectedItem = cbProduct.Text;
                
                FileStream fileW = new FileStream($"{SelectedItem}.txt", FileMode.OpenOrCreate, FileAccess.Write);
                // Create a new stream to write to the file
                StreamWriter sw = new StreamWriter(fileW);
                // Write a string to the file
                sw.WriteLine("-----------------------------------------------------------");
                sw.WriteLine($"NAAM:{productList.pc.p.Naam}" + "\n");
                sw.WriteLine($"INKOOPPRIJS:{productList.pc.p.InKoopprijs}" + "\n");
                sw.WriteLine($"MARGE:{productList.pc.p.Marge}" + "\n");
                sw.WriteLine($"EENHEID:{productList.pc.p.Eenheid}" + "\n");
                sw.WriteLine($"BTW:{productList.pc.p.BTW}" + "\n");
                sw.WriteLine($"LEVERANCIE:{productList.l.Contactpersoon}" + "\n");
                sw.WriteLine($"CATEGORIE:{productList.pc.c.CategorieNaam}" + "\n");

                sw.Flush();
                // Close StreamWriter
                sw.Close();
                // Close file
                fileW.Close();
               
            }

        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            // *** Read from file ***
            // Specify file, instructions, and privelegdes
            string SelectedItem = cbProduct.Text;
            FileStream fileR = new FileStream($"{SelectedItem}.txt", FileMode.OpenOrCreate, FileAccess.Read);
            // Create a new stream to read from a file
            StreamReader sr = new StreamReader(fileR);
            sr.DiscardBufferedData();
            // Read contents of file into a string
            string s = sr.ReadToEnd();
            // Close StreamReader
            sr.Close();
            // Close file
            fileR.Close();
            TextRange textRange = new TextRange(rtbProduct.Document.ContentStart, rtbProduct.Document.ContentEnd);
            textRange.Text = s; // Display the string in the RichTextBox.
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string SelectedItem = cbProduct.Text;
            FileStream fileW = new FileStream($"{SelectedItem}.txt", FileMode.OpenOrCreate, FileAccess.Write);
            // Create a new stream to write to the file
            StreamWriter sw = new StreamWriter(fileW);
            // Write a string to the file
            TextRange textRange = new TextRange(rtbProduct.Document.ContentStart, rtbProduct.Document.ContentEnd);
            MessageBox.Show(textRange.Text);
            sw.Write(textRange.Text);
            sw.Flush();
            // Close StreamWriter
            sw.Close();
            // Close file
            fileW.Close();
            MessageBox.Show("Bestand is opslaan");

            rtbProduct.Document.Blocks.Clear();

        }

        private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
