using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPAgame.Server.Data;
using SPAgame.Server.Models;
using SPAgame.Server.Services;
using System.Security.Claims;

namespace SPAgame.Server.Controllers
{
    [ApiController]
    [Route("api/blackjack")]

    public class BlackjackController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly DeckService _deckService;

        public BlackjackController(AppDbContext context, UserManager<AppUser> userManager, DeckService deckService)
        {
            _context = context;
            _userManager = userManager;
            _deckService = deckService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartGame()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return NotFound();

            var game = new BlackjackGame
            {
                Player = user,
                Status = "In Progress",
                PlayerHand = new PlayerHand(),
                DealerHand = new DealerHand(),
            };

            //game.PlayerHand = new PlayerHand { BlackjackGame = game};
            //game.DealerHand = new DealerHand { BlackjackGame = game };

            // Lägg till kort till spelaren och dealerns händer
            game.PlayerHand.Cards.Add(_deckService.DrawCard());
            game.PlayerHand.Cards.Add(_deckService.DrawCard());
            game.DealerHand.Cards.Add(_deckService.DrawCard());
            game.DealerHand.Cards.Add(_deckService.DrawCard());

            await _context.BlackjackGames.AddAsync(game);
            await _context.SaveChangesAsync();

            return Ok(game);


        }

        //private async Task<BlackjackGame> GetInProgressGameForUser(int userId)
        //{
        //    return await _context.BlackjackGames
        //        .Include(bg => bg.PlayerHand)
        //        .ThenInclude(ph => ph.Cards)
        //        .Include(bg => bg.DealerHand)
        //        .ThenInclude(dh => dh.Cards)
        //        .FirstOrDefaultAsync(bg => bg.Player == userId && bg.Status == "In Progress");
        //}

        private int CalculateHandValue(ICollection<Card> hand)
        {
            int value = 0;
            int aceCount = 0;

            foreach (var card in hand)
            {
                switch (card.Rank)
                {
                    case "Jack":
                    case "Queen":
                    case "King":
                        value += 10;
                        break;
                    case "Ace":
                        aceCount++;
                        value += 11;
                        break;
                    default:
                        value += int.Parse(card.Rank);
                        break;
                }
            }

            while (value > 21 && aceCount > 0)
            {
                value -= 10;
                aceCount--;
            }

            return value;
        }

        private void CheckGameResult(BlackjackGame game)
        {
            int playerValue = CalculateHandValue(game.PlayerHand.Cards);
            int dealerValue = CalculateHandValue(game.DealerHand.Cards);

            if (playerValue > 21)
            {
                game.Status = "Lose";
            }
            else if (dealerValue > 21 || playerValue > dealerValue)
            {
                game.Status = "Win";
                UpdateHighScore(game);
            }
            else if (playerValue == dealerValue)
            {
                game.Status = "Draw";
            }
            else
            {
                game.Status = "Lose";
            }
        }

        private void UpdateHighScore(BlackjackGame game)
        {
            var player = _context.Users.Find(game.Player);
            if (player != null && game.Status == "Win")
            {
                player.HighScore++;
                _context.SaveChanges();
            }
        }


    }

}
