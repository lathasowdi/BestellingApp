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
    /// Interaction logic for Template.xaml
    /// </summary>
    public partial class Template : Window
    {
        public Personeelslid loggedinpersoneelid { get; set; }
            public Template(Personeelslid loggedin)
        {
         InitializeComponent();
         loggedinpersoneelid = loggedin;
            BestellingenEntities ctx = new BestellingenEntities();
            var productList = ctx.Product.Join(
                           ctx.Categorie,
                           p => p.CategorieID,
                           c => c.CategorieID,
                           (p, c) => new { p, c }).Join(
                           ctx.Leverancier,
                           pc => pc.p.LeverancierID,
                           l => l.LeverancierID,
                           (pc, l) => new { pc, l }).Where(x => x.l.LeverancierID == productId).FirstOrDefault(); ;
            string beschrijf = "";
        }
    }
}
