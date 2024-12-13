using finalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finalProject.Areas.User.Controllers
{
    [Area("User")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch all teams and include their associated characters
            var teams = _context.Teams.Include(t => t.Characters).ToList();
            return View(teams);
        }

        public IActionResult Create()
        {
            // Fetch users and characters for dropdowns or selection in the view
            var users = _context.Users.Include(u => u.Teams).ToList();
            var characters = _context.Characters.ToList();

            ViewBag.Users = users;
            ViewBag.Characters = characters;

            return View(new Team());
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                // Add the new team to the database
                _context.Teams.Add(team);
                _context.SaveChanges();

                // Redirect to the Index action in the same area
                return RedirectToAction(nameof(Index), new { area = "User" });
            }

            // Reload data for the form in case of validation errors
            var users = _context.Users.Include(u => u.Teams).ToList();
            var characters = _context.Characters.ToList();

            ViewBag.Users = users;
            ViewBag.Characters = characters;

            return View(team);
        }

        public IActionResult Details(int id)
        {
            // Fetch the team by ID, including related characters and the associated user
            var team = _context.Teams
                .Include(t => t.Characters)
                .Include(t => t.User)
                .FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }
    }
}
