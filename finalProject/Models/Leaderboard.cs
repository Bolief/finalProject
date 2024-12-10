using finalProject.Models;

public class Leaderboard
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int Rank { get; set; }
    public int TotalWins { get; set; }
    public DateTime SnapshotDate { get; set; }
}


