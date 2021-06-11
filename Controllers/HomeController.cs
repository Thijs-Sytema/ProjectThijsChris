using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectThijsChris.Models;
using System.Diagnostics;
using MySql.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ProjectThijsChris.Database;
using System;

namespace ProjectThijsChris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string connectionString = "Server=172.16.160.21;Port=3306;Database=110444;Uid=110444;Pwd=inf2021sql;";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // alle namen ophalen
            //var names = GetNames();
            var films = GetFilms();

            // stop de namen in de html
            return View(films);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Film/{id}")]
        public IActionResult Film(string id)
        {
            var model = GetFilm(Convert.ToInt32(id));
            // var vertoningen = GetVertoningen();

            return View(model);
        }

        [Route("Films")]
        public IActionResult Films()
        {
            return View();
        }

        [Route("Taal")]
        public IActionResult Taal()
        {
            return View();
        }

        [Route("Overons")]
        public IActionResult Overons()
        {
            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("Contact")]
        public IActionResult Contact(Persoon persoon)
        {
            if (ModelState.IsValid)
            {
                SavePerson(persoon);
                return Redirect("/succes");
            }

            return View();
        }

        [Route("succes")]
        public IActionResult Succes()
        {
            return View();
        }

        private void SavePerson(Persoon person)
        {
           

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO persoon(voornaam, achternaam, emailadres, adres, beschrijving, telefoonummer) VALUES(?voornaam, ?achternaam, ?emailadres, ?adres, ?beschrijving, ?telefoonnummer)", conn);

                cmd.Parameters.Add("?voornaam", MySqlDbType.Text).Value = person.voornaam;
                cmd.Parameters.Add("?achternaam", MySqlDbType.Text).Value = person.achternaam;
                cmd.Parameters.Add("?emailadres", MySqlDbType.Text).Value = person.email;
                cmd.Parameters.Add("?adres", MySqlDbType.Text).Value = person.adres;
                cmd.Parameters.Add("?beschrijving", MySqlDbType.Text).Value = person.beschrijving;
                cmd.Parameters.Add("?telefoonnummer", MySqlDbType.Text).Value = person.telefoonnummer;
                cmd.ExecuteNonQuery();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<Vertoning> GetVertoningen()
        {          

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Vertoning> vertoningen = new List<Vertoning>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from vertoningen", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Vertoning v = new Vertoning
                        {
                            // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["id"]),
                            Tijd = DateTime.Parse(reader["tijd"].ToString()),
                            Beschrijving = reader["beschrijving"].ToString(),
                            Prijs = Convert.ToInt32(reader["prijs"]),
                            Genre = reader["genre"].ToString(),
                            Rating = Convert.ToInt32(reader["rating"])
                        };
                        // voeg de naam toe aan de lijst met vertoningen
                        vertoningen.Add(v);
                    }

                }

                // return de lijst met vertoningen
                return vertoningen;
            }
        }

        public List<Films> GetFilms()
        {

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Films> films = new List<Films>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from films", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Films v = new Films
                        {
                            // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["id"]),
                            //Tijd = DateTime.Parse(reader["tijd"].ToString()),
                            Beschrijving = reader["beschrijving"].ToString(),
                            //Prijs = Convert.ToInt32(reader["prijs"]),
                            Genre = reader["genre"].ToString(),
                            Rating = Convert.ToInt32(reader["rating"])
                        };
                        // voeg de naam toe aan de lijst met vertoningen
                        films.Add(v);
                    }

                }

                // return de lijst met vertoningen
                return films;
            }
        }

        public Films GetFilm(int id)
        {          

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Films> vertoningen = new List<Films>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand($"select * from vertoningen where id = {id}", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Films v = new Films
                        {
                            // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["id"]),
                            Tijd = DateTime.Parse(reader["tijd"].ToString()),
                            Beschrijving = reader["beschrijving"].ToString(),
                            Prijs = Convert.ToInt32(reader["prijs"]),
                            Genre = reader["genre"].ToString(),
                            Rating = Convert.ToInt32(reader["rating"])

                        };

                        // voeg de naam toe aan de lijst met namen
                        vertoningen.Add(v);
                    }

                }

                // return de lijst met namen
                return vertoningen[0];
            }
        }
    }
}
