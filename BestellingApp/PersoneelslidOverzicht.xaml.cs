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
    /// Interaction logic for PersoneelslidOverzicht.xaml
    /// </summary>
    public partial class PersoneelslidOverzicht : Window
    {
        public PersoneelslidOverzicht()
        {
            InitializeComponent();
            BestellingenEntities ctx = new BestellingenEntities();

            var PersoneelslidList = ctx.Personeelslid.Join(
                            ctx.Functie,
                            p => p.FunctieID,
                           f => f.FunctieID,
                            (p, f) => new { p, f }).ToList();

            listgrid.ItemsSource = PersoneelslidList;
        }
    }
}
