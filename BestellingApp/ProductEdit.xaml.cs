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
    /// Interaction logic for ProductEdit.xaml
    /// </summary>
    public partial class ProductEdit : Window
    {
        public ProductEdit()
        {
            InitializeComponent();
        }

        private void btnBewerken_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                string naam = "";
                if (tbNaam.Text.Trim() != "")
                {
                    naam = tbNaam.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Naam a.u.b");
                }
                float inkoopprijs = 0;
                if (tbInkoopprijs.Text.Trim() != "")
                {
                    inkoopprijs =(float) Convert.ToDouble(tbInkoopprijs.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Geef Inkoopprijs a.u.b");
                }
                double Marge = 0;
                if (tbMarge.Text.Trim() != "")
                {
                    Marge = Convert.ToDouble(tbMarge.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Geef Marge a.u.b");
                }
                string eenheeid = "";
                if (tbEenheid.Text.Trim() != "")
                {
                    eenheeid = tbEenheid.Text.Trim();
                }
                else
                {
                    MessageBox.Show("Geef Eenheid a.u.b");
                }
                double btw = 0;
                if (tbBtw.Text.Trim() != "")
                {
                    btw = Convert.ToDouble(tbBtw.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Geef BTW a.u.b");
                }
                int leverancierID = (int)LbLeverancier.SelectedValue;
                int categorieID = (int)cbCategorie.SelectedValue;
            }
        }


            private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                //using (BestellingenEntities ctx = new BestellingenEntities())
                //{
                //    var selectedProduct = (Product)cbProduct.SelectedItem;
                //   tbNaam.Text = selectedProduct.Naam;
                //tbInkoopprijs.Text= selectedProduct.InKoopprijs;
                //    tbMarge.Text = selectedProduct.Marge;
                //    tbEenheid.Text = selectedProduct.Eenheid;
                //    tbBtw.Text = selectedProduct.BTW;
                //LbLeverancier.SelectedValue = selectedProduct.LeverancierID;
                //cbCategorie.SelectedValue = selectedProduct.CategorieID;
                //}
            }
       
    }
}
