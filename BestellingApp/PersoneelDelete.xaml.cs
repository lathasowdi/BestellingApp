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
    /// Interaction logic for PersoneelDelete.xaml
    /// </summary>
    public partial class PersoneelDelete : Window
    {
        public PersoneelDelete()
        {
            InitializeComponent();
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var Personeelquery = ctx.Personeelslid.Select(k => k);

                cbPersoneelslid.ItemsSource = Personeelquery.ToList();

            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            using (BestellingenEntities ctx = new BestellingenEntities())
            {
                var selectedPersoneel = (Personeelslid)cbPersoneelslid.SelectedItem;
                ctx.Personeelslid.RemoveRange(ctx.Personeelslid.Where(k => k.PersoneelslidID == selectedPersoneel.PersoneelslidID));
                ctx.SaveChanges();
            }
            MessageBox.Show("Personeelslid is verwijderen");
        }
    }
}
