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

        private bool IsLoggedIn() => HttpContext.Session.GetString(SessionKeyIsLoggedIn) == "true";

        private bool IsAdmin() => HttpContext.Session.GetString("Role") == "Admin";

        private string? GetUsername() => HttpContext.Session.GetString("Username");

        private IActionResult? EnsureAdmin()
        {
            if (!IsLoggedIn() || !IsAdmin())
            {
                return RedirectToAction("Logowanie");
            }
            return null;
        }

        public IActionResult Logowanie()
        {
            if (IsLoggedIn())
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
                var user = _context.Users.FirstOrDefault(u => u.Username == model.Login);
                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    HttpContext.Session.SetString(SessionKeyIsLoggedIn, "true");
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("Role", user.Role);
                    return RedirectToAction("Witaj");
                }
                ModelState.AddModelError("", "Nieprawidłowy login lub hasło");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Witaj()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Logowanie");
            }
            ViewBag.Username = GetUsername();
            ViewBag.IsAdmin = IsAdmin();
            return View();
        }

        [HttpGet]
        public IActionResult Gallery()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Logowanie");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Logowanie");
            }
            ViewBag.Username = GetUsername();
            return View();
        }

        [HttpGet]
        public IActionResult Chat()
        {
            if (!IsLoggedIn())
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
            if (!IsLoggedIn())
            {
                return RedirectToAction("Logowanie");
            }

            var players = _context.Players.Include(p => p.Team).AsQueryable();

            if (!string.IsNullOrEmpty(team))
            {
                players = players.Where(p => p.Team != null && p.Team.Name == team);
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
            if (!IsLoggedIn())
            {
                return RedirectToAction("Logowanie");
            }

            var matches = _context.Match
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .AsQueryable();

            if (!string.IsNullOrEmpty(team))
            {
                matches = matches.Where(m =>
                    (m.Team1 != null && m.Team1.Name == team) ||
                    (m.Team2 != null && m.Team2.Name == team));
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
        public IActionResult Coaches(string sort, string team)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Logowanie");
            }

            var coaches = _context.Coach
                .Include(c => c.Team)
                .AsQueryable();

            if (!string.IsNullOrEmpty(team))
            {
                coaches = coaches.Where(c => c.Team != null && c.Team.Name == team);
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


        [HttpGet]
        public IActionResult Teams(string sort, string country)
        {
            if (!IsLoggedIn())
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
        public IActionResult Users()
        {
            var adminCheck = EnsureAdmin();
            if (adminCheck != null)
                return adminCheck;

            var users = _context.Users.OrderBy(u => u.Username).ToList();
            return View(users);
        }
        
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Logins.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Nazwa użytkownika już istnieje.");
                return View(model);
            }

            var newUser = new Login
            {
                Username = model.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "User" // domyślnie nowi użytkownicy nie są adminami
            };

            _context.Logins.Add(newUser);
            _context.SaveChanges();

            TempData["Success"] = "Konto utworzone! Możesz się zalogować.";
            return RedirectToAction("Logowanie");
        }

    }
}
