namespace finalProject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Character
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Character name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Character strength is required")]
        [Range(50, 500, ErrorMessage = "Character strength must be between 50 and 500")]
        public int Strength { get; set; }

        [Required(ErrorMessage = "Character defense is required")]
        [Range(50, 500, ErrorMessage = "Character defense must be between 50 and 500")]
        public int Defense { get; set; }

        [Required(ErrorMessage = "Character speed is required")]
        [Range(50, 500, ErrorMessage = "Character speed must be between 50 and 500")]
        public int Speed { get; set; }

        [Required(ErrorMessage = "Character health is required")]
        [Range(50, 500, ErrorMessage = "Character health must be between 50 and 500")]
        public int Health { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        // Define a collection for Moves
        public List<Move> Moves { get; set; } = new List<Move>();

        // ValidateStats method to ensure the sum of stats does not exceed the maximum limit
        public static bool ValidateStats(Character character, int maxPoints)
        {
            return (character.Strength + character.Defense + character.Speed + character.Health) <= maxPoints;
        }
    }



}
