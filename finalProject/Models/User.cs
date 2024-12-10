using System.ComponentModel.DataAnnotations;

namespace finalProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<Team> Teams { get; set; } = new();
        public int TotalWins { get; set; }
        public List<Badge> Badges { get; set; } = new();
    }


}
