using finalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finalProject.Controllers
{
    public class BattleController : Controller
    {
        private readonly AppDbContext _context;

        public BattleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var teams = _context.Teams.Include(t => t.Characters).ToList();
            return View(teams);
        }

        public IActionResult Battle(int team1Id, int team2Id)
        {
            var team1 = _context.Teams.Include(t => t.Characters).FirstOrDefault(t => t.Id == team1Id);
            var team2 = _context.Teams.Include(t => t.Characters).FirstOrDefault(t => t.Id == team2Id);

            if (team1 == null || team2 == null)
            {
                TempData["ErrorMessage"] = "One or both teams could not be found.";
                return RedirectToAction("Index");
            }

            // Simple battle logic (you can enhance this)
            var winner = (team1.TotalWins > team2.TotalWins) ? team1 : team2;

            // Save battle to database
            var battle = new Battle
            {
                Team1Id = team1.Id,
                Team2Id = team2.Id,
                WinnerTeamId = winner.Id,
                BattleDate = DateTime.Now
            };
            _context.Battles.Add(battle);
            _context.SaveChanges();

            ViewBag.Winner = winner;
            return View(battle);
        }

    }

}
