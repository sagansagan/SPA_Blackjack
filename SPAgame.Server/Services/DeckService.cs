using SPAgame.Server.Models;

namespace SPAgame.Server.Services
{
    public class DeckService
    {
        private List<Card> deck;
        private readonly Random random = new Random();

        public DeckService()
        {
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            deck = new List<Card>();
            var suits = new[] { "Hearts", "Diamonds", "Clubs", "Spades" };
            var ranks = new[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            // Skapar fyra kortlekar
            for (int i = 0; i < 4; i++)
            {
                foreach (var suit in suits)
                {
                    foreach (var rank in ranks)
                    {
                        deck.Add(new Card { Suit = suit, Rank = rank });
                    }
                }
            }

            ShuffleDeck();
        }
        private void ShuffleDeck()
        {
            int n = deck.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                var temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }
        public Card DrawCard()
        {
            if (deck == null || deck.Count == 0)
            {
                InitializeDeck();
            }

            // Drar ett kort och ta bort det från kortleken
            var index = random.Next(deck.Count);
            Card drawnCard = deck[index];
            deck.RemoveAt(index);
            return drawnCard;

        }

    }
}
