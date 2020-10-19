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
            listgrid.ItemsSource = ctx.Klant.ToList();

        }
    }
}
