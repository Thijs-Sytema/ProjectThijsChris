using System.ComponentModel.DataAnnotations;

namespace ProjectThijsChris.Models
{
    public class Persoon
    {
        [Required(ErrorMessage = "Gelieve uw voornaam in te vullen")]
        public string voornaam { get; set; }
        [Required(ErrorMessage = "Gelieve uw achternaam in te vullen")]
        public string achternaam { get; set; }
        [Required(ErrorMessage = "Gelieve uw emailadres in te vullen")]
        public string email { get; set; }
        public string telefoonnummer { get; set; }
        public string adres { get; set; }
        public string beschrijving { get; set; }
    }
}
