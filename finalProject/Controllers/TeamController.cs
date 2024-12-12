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

        [HttpPost]
        public IActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Teams.Add(team); // Add the team to the database context
                _context.SaveChanges();   // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect back to the Index view
            }

            var users = _context.Users.Include(u => u.Teams).ToList();
            var characters = _context.Characters.ToList();
            ViewBag.Users = users;
            ViewBag.Characters = characters;
            return View(team);
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
