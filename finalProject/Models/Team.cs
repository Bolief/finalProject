using System.ComponentModel.DataAnnotations;

namespace finalProject.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; } = "A regular team";
        public List<Character> Characters { get; set; } = new();
        public int TotalWins { get; set; } = 0;
    }
}