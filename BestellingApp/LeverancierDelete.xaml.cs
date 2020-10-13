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
    /// Interaction logic for LeverancierDelete.xaml
    /// </summary>
    public partial class LeverancierDelete : Window
    {
        public LeverancierDelete()
        {
            InitializeComponent();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Leverancierquery = ctx.Leverancier.Select(k => k);

                cbLeverancier.ItemsSource = Leverancierquery.ToList();

            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedLeverancier = (Leverancier)cbLeverancier.SelectedItem;
                ctx.Leverancier.RemoveRange(ctx.Leverancier.Where(k => k.LeverancierID == selectedLeverancier.LeverancierID));
                ctx.SaveChanges();
            }
            MessageBox.Show("Leverancier is verwijderen");
        }
    }
}
