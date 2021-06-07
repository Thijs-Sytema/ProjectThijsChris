using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectThijsChris.Models;
using System.Diagnostics;
using MySql.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ProjectThijsChris.Controllers.Database;
using System;

namespace ProjectThijsChris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // alle namen ophalen
            //var names = GetNames();
            var products = GetProducts();

            // stop de namen in de html
            return View(products);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Film/{id}")]
        public IActionResult Film(string id)
        {
            var model = GetFilm(Convert.ToInt32(id));

            return View(model);
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
                return Redirect("/succes");

            return View(persoon);
        }

        [Route("succes")]
        public IActionResult Succes()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<string> GetNames()
        {
            // stel in waar de database gevonden kan worden
            string connectionString = "Server=172.16.160.21;Port=3306;Database=110444;Uid=110444;Pwd=inf2021sql;";

            // maak een lege lijst waar we de namen in gaan opslaan
            List<string> names = new List<string>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from product", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                        string Name = reader["Naam"].ToString();

                        // voeg de naam toe aan de lijst met namen
                        names.Add(Name);
                    }
                }
            }

            // return de lijst met namen
            return names;
        }


        //public List<string> GetProducts()
        //{
        //    // stel in waar de database gevonden kan worden
        //    string connectionString = "Server=172.16.160.21;Port=3306;Database=110444;Uid=110444;Pwd=inf2021sql;";

        //    // maak een lege lijst waar we de namen in gaan opslaan
        //    List<Product> products = new List<Product>();

        //    // verbinding maken met de database
        //    using (MySqlConnection conn = new MySqlConnection(connectionString))
        //    {
        //        // verbinding openen
        //        conn.Open();

        //        // SQL query die we willen uitvoeren
        //        MySqlCommand cmd = new MySqlCommand("select * from product", conn);

        //        // resultaat van de query lezen
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            // elke keer een regel (of eigenlijk: database rij) lezen
        //            while (reader.Read())
        //            {
        //                Product p = new Product
        //                {
        //                    // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
        //                    Id = Convert.ToInt32(reader["Id"]),
        //                    Beschikbaarheid = Convert.ToInt32(reader["Beschikbaarheid"]),
        //                    Naam = reader["Naam"].ToString(),
        //                    Prijs = reader["Prijs"].ToString(),
        //                    string Name = reader["Naam"].ToString();
        //            };

        //            // voeg de naam toe aan de lijst met namen
        //            products.Add(p);
        //        }
                
        //    }

        //    // return de lijst met namen
        //    return products;
        //}

        public List<Product> GetProducts()
        {
            // stel in waar de database gevonden kan worden
            string connectionString = "Server=172.16.160.21;Port=3306;Database=110444;Uid=110444;Pwd=inf2021sql;";

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Product> products = new List<Product>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from product", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Product p = new Product
                        {
                            // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["Id"]),
                            Beschikbaarheid = Convert.ToInt32(reader["Beschikbaarheid"]),
                            Naam = reader["Naam"].ToString(),
                            Prijs = reader["Prijs"].ToString()

                        };

                        // voeg de naam toe aan de lijst met namen
                        products.Add(p);
                    }

                }

                // return de lijst met namen
                return products;
            }
        }

        public Product GetFilm(int id)
        {
            // stel in waar de database gevonden kan worden
            string connectionString = "Server=172.16.160.21;Port=3306;Database=110444;Uid=110444;Pwd=inf2021sql;";

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Product> products = new List<Product>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand($"select * from product where id = {id}", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Product p = new Product
                        {
                            // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["Id"]),
                            Beschikbaarheid = Convert.ToInt32(reader["Beschikbaarheid"]),
                            Naam = reader["Naam"].ToString(),
                            Prijs = reader["Prijs"].ToString()

                        };

                        // voeg de naam toe aan de lijst met namen
                        products.Add(p);
                    }

                }

                // return de lijst met namen
                return products[0];
            }
        }
    }
}
