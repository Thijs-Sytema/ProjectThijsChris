using System;

namespace ProjectThijsChris.Database
{
    public class Vertoning
    {
        public int Id { get; set; }

        public DateTime Tijd { get; set; }

        public string Beschrijving { get; set; }

        public int Prijs { get; set; }

        public string Genre { get; set; }

        public int Rating { get; set; }
    }
}
