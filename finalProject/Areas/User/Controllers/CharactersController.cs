﻿using finalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finalProject.Areas.User.Controllers
{
    [Area("User")]
    public class CharactersController : Controller
    {
        private readonly AppDbContext _context;

        public CharactersController(AppDbContext context)
        {
            _context = context;
        }

        // Displays the Add Character page for a specific team
        public IActionResult AddCharacter(int teamId)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == teamId);

            if (team == null)
            {
                return NotFound();
            }

            ViewBag.TeamName = team.Name;
            ViewBag.TeamId = teamId;

            return View();
        }

        // Saves a new character to a specific team
        [HttpPost]
        public IActionResult SaveCharacter(int teamId, string name, int health, int strength, int defense, int speed)
        {
            var team = _context.Teams.Include(t => t.Characters).FirstOrDefault(t => t.Id == teamId);

            if (team == null)
            {
                return NotFound();
            }

            var character = new Character
            {
                Name = name,
                Health = health,
                Strength = strength,
                Defense = defense,
                Speed = speed
            };

            team.Characters.Add(character);
            _context.SaveChanges();

            return RedirectToAction("Details", "Teams", new { area = "User", id = teamId });
        }

        // Displays the customize page for a team's characters
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

        // Updates the stats of characters in a specific team
        [HttpPost]
        public IActionResult Customize(int teamId, List<Character> updatedCharacters)
        {
            var team = _context.Teams
                .Include(t => t.Characters)
                .FirstOrDefault(t => t.Id == teamId);

            if (team == null)
            {
                TempData["ErrorMessage"] = "Team not found.";
                return RedirectToAction("Index", "Teams", new { area = "User" });
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
                else
                {
                    TempData["ErrorMessage"] = $"Invalid stats for character {updatedCharacter.Name}.";
                    return RedirectToAction("Customize", new { area = "User", teamId = teamId });
                }
            }

            _context.SaveChanges();
            TempData["SuccessMessage"] = "Character stats updated successfully!";
            return RedirectToAction("Index", "Teams", new { area = "User" });
        }

        // Delete Confirmation Page
        public IActionResult DeleteCharacterConfirmation(int id)
        {
            var character = _context.Characters.FirstOrDefault(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        [HttpPost]
        public IActionResult ConfirmDeleteCharacter(int id)
        {
            var character = _context.Characters.FirstOrDefault(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            _context.SaveChanges();

            TempData["SuccessMessage"] = $"Character '{character.Name}' was deleted successfully.";
            return RedirectToAction("Details", "Teams", new { area = "User", id = character.TeamId });
        }

        // Edit Character Page
        public IActionResult EditCharacter(int id)
        {
            var character = _context.Characters.FirstOrDefault(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        [HttpPost]
        public IActionResult EditCharacter(Character updatedCharacter)
        {
            var existingCharacter = _context.Characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

            if (existingCharacter == null)
            {
                return NotFound();
            }

            if (Character.ValidateStats(updatedCharacter, 100))
            {
                existingCharacter.Name = updatedCharacter.Name;
                existingCharacter.Strength = updatedCharacter.Strength;
                existingCharacter.Defense = updatedCharacter.Defense;
                existingCharacter.Speed = updatedCharacter.Speed;
                existingCharacter.Health = updatedCharacter.Health;

                _context.SaveChanges();

                TempData["SuccessMessage"] = $"Character '{updatedCharacter.Name}' was updated successfully.";
                return RedirectToAction("Details", "Teams", new { area = "User", id = existingCharacter.TeamId });
            }

            TempData["ErrorMessage"] = "Invalid character stats. Please ensure all stats are within valid limits.";
            return View(updatedCharacter);
        }
    }
}
