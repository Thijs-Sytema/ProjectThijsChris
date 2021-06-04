using System.ComponentModel.DataAnnotations;

namespace ProjectThijsChris.Models
{
    public class Persoon
    {
        [Required]
        public string voornaam { get; set; }
        [Required]
        public string achternaam { get; set; }
        [Required]
        public string email { get; set; }
        public string telefoonnummer { get; set; }
        public string adres { get; set; }
        public string beschrijving { get; set; }
    }
}
