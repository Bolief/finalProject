using finalProject.Models;
using System.ComponentModel.DataAnnotations;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Required(ErrorMessage = " Character strength is required")]
    [Range (50, 500, ErrorMessage = "Character strength must be between 1 and 500")]
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    public int Health { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public List<Move> Moves { get; set; } = new();

    // Ensure valid stat distribution
    public static bool ValidateStats(Character character, int maxPoints)
    {
        return (character.Strength + character.Defense + character.Speed + character.Health) <= maxPoints;
    }
}
