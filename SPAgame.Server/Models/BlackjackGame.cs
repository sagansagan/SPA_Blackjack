using SPAgame.Server.Data;
using System.ComponentModel.DataAnnotations;

namespace SPAgame.Server.Models
{
    public class BlackjackGame
    {
        [Key]
        public int Id { get; set; }
        //public int PlayerId { get; set; }
        public AppUser Player { get; set; }
        public PlayerHand PlayerHand { get; set; }
        public DealerHand DealerHand { get; set; }
        public string Status { get; set; } // "In Progress", "Win", "Lose", "Draw"
    }
}
