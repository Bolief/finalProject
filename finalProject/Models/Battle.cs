namespace finalProject.Models
{
    public class Battle
    {
        public int Id { get; set; }
        public int Team1Id { get; set; }
        public Team Team1 { get; set; }
        public int Team2Id { get; set; }
        public Team Team2 { get; set; }
        public int? WinnerTeamId { get; set; }
        public Team Winner { get; set; }
        public DateTime BattleDate { get; set; }
    }

}
