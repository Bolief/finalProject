using finalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly AppDbContext _context;

        public LeaderboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var leaderboard = _context.Users
                .OrderByDescending(u => u.TotalWins)
                .Select(u => new
                {
                    Username = u.Username,
                    TotalWins = u.TotalWins
                })
                .ToList();

            return View(leaderboard);
        }
    }

}
