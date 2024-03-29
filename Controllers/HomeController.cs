﻿using ProjectThijsChris.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using ProjectThijsChris.Database;


namespace ProjectThijsChris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110444;Uid=110444;Pwd=inf2021sql;";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Film> films = new List<Film>();
            films = GetFilms();

            return View(films);
        }

        private List<Film> GetFilms()
        {
            // stel in waar de database gevonden kan worden
            string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110444;Uid=110444;Pwd=inf2021sql;";

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Film> films = new List<Film>();

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
                        Film p = new Film
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["beschrijving"].ToString(),
                            Genre = reader["genre"].ToString(),
                            Tijd = reader["tijd"].ToString(),
                            Rating = reader["rating"].ToString(),
                            Prijs = reader["prijs"].ToString(),
                        };
                        films.Add(p);
                    }
                }
            }
            // return de lijst met films
            return films;
        }

        [Route("Films")]
        public IActionResult Films()
        {
            List<Film> films = new List<Film>();
            films = GetFilms();

            return View(films);
        }

        [Route("Contact")]
        public IActionResult contact()
        {
            return View();
        }

        [Route("Contact")]
        [HttpPost]
        public IActionResult contact(PersonModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            SavePerson(model);

            return Redirect("/succes");
        }

        [Route("succes")]
        public IActionResult succes()
        {
            return View();
        }

        [Route("Overons")]
        public IActionResult Overons()
        {
            return View();
        }

        [Route("error")]
        public IActionResult error()
        {
            return View();
        }

        [Route("film/{id}")]
        public IActionResult Film(string id)
        {
            var model = GetFilm(id);

            return View(model);
        }
        private Film GetFilm(string id)
        {

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from films where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Film p = new Film
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["beschrijving"].ToString(),
                            Genre = reader["genre"].ToString(),
                            Tijd = reader["tijd"].ToString(),
                            Rating = reader["rating"].ToString(),
                            Prijs = reader["prijs"].ToString(),
                        };
                        return p;
                    }
                }
            }
            return null;
        }

        private void SavePerson(PersonModel person)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(firstname, lastname, phonenumber, email, subject) VALUES(?firstname, ?lastname, ?phonenumber, ?email, ?subject)", conn);
                cmd.Parameters.Add("?firstname", MySqlDbType.VarChar).Value = person.firstname;
                cmd.Parameters.Add("?lastname", MySqlDbType.VarChar).Value = person.lastname;
                cmd.Parameters.Add("?phonenumber", MySqlDbType.VarChar).Value = person.phonenumber;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = person.email;
                cmd.Parameters.Add("?subject", MySqlDbType.VarChar).Value = person.subject;
                cmd.ExecuteNonQuery();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}