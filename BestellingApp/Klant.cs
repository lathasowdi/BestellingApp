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
    
    public partial class Klant
    {
        public int KlantID { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Straatnaam { get; set; }
        public Nullable<int> Huisnummer { get; set; }
        public string Bus { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public string Telefoonnummer { get; set; }
        public string Emailadres { get; set; }
        public Nullable<System.DateTime> AangemaaktOp { get; set; }
        public string Opmerking { get; set; }
    }
}