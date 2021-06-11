using System;

namespace ProjectThijsChris.Database
{
    public class Film
    {
        public int Id { get; set; }

        public string Naam { get; set; }

        public string Tijd { get; set; }

        public string Beschrijving { get; set; }
        public string Beschrijvingkort() { return this.Beschrijving.Substring(0, this.Beschrijving.IndexOf(" ", 50)); }

        public string Prijs { get; set; }

        public string Genre { get; set; }

        public string Rating { get; set; }
    }
}
