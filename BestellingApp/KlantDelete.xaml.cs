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
    /// Interaction logic for KlantDelete.xaml
    /// </summary>
    public partial class KlantDelete : Window
    {
        public KlantDelete()
        {
            InitializeComponent();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var klantquery = ctx.Klant.Select(k => k);

                cbklant.ItemsSource = klantquery.ToList();

            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedklant = (Klant)cbklant.SelectedItem;
                ctx.Klant.RemoveRange(ctx.Klant.Where(k => k.KlantID == selectedklant.KlantID));
                ctx.SaveChanges();
            }
            MessageBox.Show("klant is verwijderen");
        }
    }
}
