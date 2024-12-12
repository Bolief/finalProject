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

            // Calculate team combat scores
            double CalculateTeamScore(Team team)
            {
                return team.Characters.Sum(c =>
                    (c.Strength * 0.4) +  // Weight strength
                    (c.Defense * 0.3) +  // Weight defense
                    (c.Speed * 0.2) +    // Weight speed
                    (c.Health * 0.1)     // Weight health
                );
            }

            var team1Score = CalculateTeamScore(team1);
            var team2Score = CalculateTeamScore(team2);

            Team? winner = null;
            if (team1Score > team2Score)
            {
                winner = team1;
            }
            else if (team2Score > team1Score)
            {
                winner = team2;
            }
            // winner remains null if it's a tie

            // Save the battle result to the database
            var battle = new Battle
            {
                Team1Id = team1.Id,
                Team2Id = team2.Id,
                WinnerTeamId = winner?.Id, // Allow null if tied
                BattleDate = DateTime.Now
            };
            _context.Battles.Add(battle);
            _context.SaveChanges();

            ViewBag.Winner = winner;
            ViewBag.Team1Score = team1Score;
            ViewBag.Team2Score = team2Score;
            return View(battle);
        }


    }

}
