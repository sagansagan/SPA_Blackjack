using SPAgame.Server.Data;

namespace SPAgame.Server.Models
{
    public class HighScore
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Wins { get; set; }
        public int GamesPlayed { get; set; }
        public AppUser User { get; set; }
    }
}
