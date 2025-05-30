using Microsoft.AspNetCore.Mvc;
using LoginApp.Models;
using Microsoft.AspNetCore.Http;
using LoginApp.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.Controllers
{
    public class IOController : Controller
    {
        private const string SessionKeyIsLoggedIn = "IsLoggedIn";
        private readonly AppDbContext _context;

        public IOController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Logowanie()
        {
            if (HttpContext.Session.GetString(SessionKeyIsLoggedIn) == "true")
            {
                return RedirectToAction("Witaj");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logowanie(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var login = _context.Logins?.FirstOrDefault(l => l.Username == model.Login);
                if (login != null && BCrypt.Net.BCrypt.Verify(model.Password, login.PasswordHash))
                {
                    HttpContext.Session.SetString(SessionKeyIsLoggedIn, "true");
                    return RedirectToAction("Witaj");
                }
                ModelState.AddModelError("", "Nieprawidłowy login lub hasło");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Witaj()
        {
            if (HttpContext.Session.GetString(SessionKeyIsLoggedIn) != "true")
            {
                return RedirectToAction("Logowanie");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Gallery()
        {
            if (HttpContext.Session.GetString(SessionKeyIsLoggedIn) != "true")
            {
                return RedirectToAction("Logowanie");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString(SessionKeyIsLoggedIn) != "true")
            {
                return RedirectToAction("Logowanie");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Chat()
        {
            if (HttpContext.Session.GetString(SessionKeyIsLoggedIn) != "true")
            {
                return RedirectToAction("Logowanie");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Wyloguj()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Logowanie");
        }

        [HttpGet]
        public IActionResult Players(string sort, string team)
        {
            if (HttpContext.Session.GetString(SessionKeyIsLoggedIn) != "true")
            {
                return RedirectToAction("Logowanie");
            }

            var players = _context.Players.AsQueryable();
            if (!string.IsNullOrEmpty(team))
            {
                players = players.Where(p => p.Team == team);
            }
            switch (sort)
            {
                case "name":
                    players = players.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    players = players.OrderByDescending(p => p.Name);
                    break;
                case "goals":
                    players = players.OrderBy(p => p.Goals);
                    break;
                case "goals_desc":
                    players = players.OrderByDescending(p => p.Goals);
                    break;
                default:
                    players = players.OrderBy(p => p.Id);
                    break;
            }
            ViewBag.Teams = _context.Teams.Select(t => t.Name).ToList();
            ViewBag.CurrentTeam = team;
            return View(players.ToList());
        }

        [HttpGet]
        public IActionResult Matches(string sort, string team)
        {
            if (HttpContext.Session.GetString(SessionKeyIsLoggedIn) != "true")
            {
                return RedirectToAction("Logowanie");
            }
            var matches = _context.Match.AsQueryable();
            if (!string.IsNullOrEmpty(team))
            {
                matches = matches.Where(m => m.Team1 == team || m.Team2 == team);
            }
            switch (sort)
            {
                case "date":
                    matches = matches.OrderBy(m => m.Date);
                    break;
                case "date_desc":
                    matches = matches.OrderByDescending(m => m.Date);
                    break;
                default:
                    matches = matches.OrderBy(m => m.Id);
                    break;
            }
            ViewBag.Teams = _context.Teams.Select(t => t.Name).ToList();
            ViewBag.CurrentTeam = team;
            return View(matches.ToList());
        }

        [HttpGet]
        public IActionResult Teams(string sort, string country)
        {
            if (HttpContext.Session.GetString(SessionKeyIsLoggedIn) != "true")
            {
                return RedirectToAction("Logowanie");
            }
            var teams = _context.Teams.AsQueryable();
            if (!string.IsNullOrEmpty(country))
            {
                teams = teams.Where(t => t.Country == country);
            }
            switch (sort)
            {
                case "name":
                    teams = teams.OrderBy(t => t.Name);
                    break;
                case "name_desc":
                    teams = teams.OrderByDescending(t => t.Name);
                    break;
                case "points":
                    teams = teams.OrderBy(t => t.Points);
                    break;
                case "points_desc":
                    teams = teams.OrderByDescending(t => t.Points);
                    break;
                default:
                    teams = teams.OrderBy(t => t.Id);
                    break;
            }
            ViewBag.Countries = _context.Teams.Select(t => t.Country).Distinct().ToList();
            ViewBag.CurrentCountry = country;
            return View(teams.ToList());
        }

        [HttpGet]
        public IActionResult Coaches(string sort, string team)
        {
            if (HttpContext.Session.GetString(SessionKeyIsLoggedIn) != "true")
            {
                return RedirectToAction("Logowanie");
            }
            var coaches = _context.Coach.AsQueryable();
            if (!string.IsNullOrEmpty(team))
            {
                coaches = coaches.Where(c => c.Team == team);
            }
            switch (sort)
            {
                case "name":
                    coaches = coaches.OrderBy(c => c.Name);
                    break;
                case "name_desc":
                    coaches = coaches.OrderByDescending(c => c.Name);
                    break;
                default:
                    coaches = coaches.OrderBy(c => c.Id);
                    break;
            }
            ViewBag.Teams = _context.Teams.Select(t => t.Name).ToList();
            ViewBag.CurrentTeam = team;
            return View(coaches.ToList());
        }
    }
}