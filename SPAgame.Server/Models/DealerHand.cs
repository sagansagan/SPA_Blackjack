using System.Text.Json.Serialization;

namespace SPAgame.Server.Models
{
    public class DealerHand
    {
        public int Id { get; set; }
        public ICollection<Card> Cards { get; set; } = new List<Card>();
        public int BlackjackGameId { get; set; }
        public BlackjackGame BlackjackGame { get; set; }
    }
}
