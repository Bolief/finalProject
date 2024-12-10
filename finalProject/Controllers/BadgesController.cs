using finalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.Controllers
{
    public class BadgesController : Controller
    {
        private readonly AppDbContext _context;

        public BadgesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int userId)
        {
            var badges = _context.Badges
                .Where(b => b.UserId == userId)
                .Select(b => new
                {
                    Name = b.Name,
                    Description = b.Description
                })
                .ToList();

            return View(badges);
        }
    }

}
