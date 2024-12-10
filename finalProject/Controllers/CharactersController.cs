using finalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finalProject.Controllers
{
    public class CharactersController : Controller
    {
        private readonly AppDbContext _context;

        public CharactersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Customize(int teamId)
        {
            var team = _context.Teams
                .Include(t => t.Characters)
                .FirstOrDefault(t => t.Id == teamId);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        [HttpPost]
        public IActionResult Customize(int teamId, List<Character> updatedCharacters)
        {
            var team = _context.Teams
                .Include(t => t.Characters)
                .FirstOrDefault(t => t.Id == teamId);

            if (team == null)
            {
                return NotFound();
            }

            foreach (var updatedCharacter in updatedCharacters)
            {
                var existingCharacter = team.Characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                if (existingCharacter != null && Character.ValidateStats(updatedCharacter, 100))
                {
                    existingCharacter.Strength = updatedCharacter.Strength;
                    existingCharacter.Defense = updatedCharacter.Defense;
                    existingCharacter.Speed = updatedCharacter.Speed;
                    existingCharacter.Health = updatedCharacter.Health;
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Teams");
        }
    }


}
