//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BestellingApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int ProductID { get; set; }
        public string Naam { get; set; }
        public Nullable<double> InKoopprijs { get; set; }
        public Nullable<double> Marge { get; set; }
        public string Eenheid { get; set; }
        public Nullable<double> BTW { get; set; }
        public Nullable<int> LeverancierID { get; set; }
        public Nullable<int> CategorieID { get; set; }
    }
}
