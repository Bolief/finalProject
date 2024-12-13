using finalProject.Areas.User.Controllers;

namespace finalProject.Controllers
{
    using finalProject.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class TeamsController : Controller
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var teams = _context.Teams.Include(t => t.Characters).ToList();
            return View(teams);
        }

        public IActionResult Create()
        {
            var users = _context.Users.Include(u => u.Teams).ToList();
            var characters = _context.Characters.ToList();
            ViewBag.Users = users;
            ViewBag.Characters = characters;
            return View(new Team());
        }
        public IActionResult Details(int id)
        {
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
