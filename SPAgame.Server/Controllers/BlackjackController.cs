using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPAgame.Server.Data;
using SPAgame.Server.Models;
using SPAgame.Server.Services;
using System.Security.Claims;
using System.Threading.Tasks.Dataflow;

namespace SPAgame.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BlackjackController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly DeckService _deckService;

        public BlackjackController(AppDbContext context, DeckService deckService)
        {
            _context = context;
            _deckService = deckService;
        }

        [HttpPost("start")]
        [Authorize]
        public async Task<IActionResult> StartGame()
        {

            var userId = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new ArgumentNullException("userId");
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return NotFound();

            var game = new BlackjackGame
            {
                Player = user,
                Status = "In Progress",
                PlayerHand = new PlayerHand(),
                DealerHand = new DealerHand(),
            };

            // Lägg till kort till spelaren och dealerns händer
            game.PlayerHand.Cards.Add(_deckService.DrawCard());
            game.PlayerHand.Cards.Add(_deckService.DrawCard());
            game.DealerHand.Cards.Add(_deckService.DrawCard());
            game.DealerHand.Cards.Add(_deckService.DrawCard());

            await _context.BlackjackGames.AddAsync(game);
            await _context.SaveChangesAsync();

            return Ok(game);


        }

        [HttpPost("hit/{gameId}")]
        [Authorize]
        public async Task<IActionResult> Hit(int gameId)
        {
            var userClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new ArgumentNullException("userId");
            var user = await _context.Users.FindAsync(userClaim);

            if (user == null) return NotFound();

            var game = await GetInProgressGame(gameId);

            if (game == null) return NotFound("No in-progress game found.");

            // Dra ett kort till spelarens hand
            var drawnCard = _deckService.DrawCard();
            game.PlayerHand.Cards.Add(drawnCard);

            // Kontrollera spelets status
            int playerValue = CalculateHandValue(game.PlayerHand.Cards);

            if (playerValue > 21)
            {
                game.Status = "Lose";
                await UpdateHighScore(user, false);
            }
            else if (playerValue == 21)
            {
                game.Status = "Win";
                //UpdateHighScore(game);
                await UpdateHighScore(user, true);
            }

            // Spara ändringar i databasen
            await _context.SaveChangesAsync();

            return Ok(game);
        }

        [HttpPost("stand/{gameId}")]
        [Authorize]
        public async Task<IActionResult> Stand(int gameId)
        {
            var userClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new ArgumentNullException("userId");
            var user = await _context.Users.FindAsync(userClaim);

            if (user == null) return NotFound();

            var game = await GetInProgressGame(gameId);

            if (game == null) return NotFound("No in-progress game found.");

            // Dealern drar kort enligt reglerna tills handvärdet är minst 17
            int dealerValue = CalculateHandValue(game.DealerHand.Cards);
            while (dealerValue < 17)
            {
                var drawnCard = _deckService.DrawCard();
                game.DealerHand.Cards.Add(drawnCard);
                dealerValue = CalculateHandValue(game.DealerHand.Cards);
            }

            // Kontrollera spelets resultat
            int playerValue = CalculateHandValue(game.PlayerHand.Cards);

            if (dealerValue > 21 || playerValue > dealerValue)
            {
                game.Status = "Win";
                //UpdateHighScore(game);
                await UpdateHighScore(user, true);
            }
            else if (playerValue == dealerValue)
            {
                game.Status = "Draw";
                await UpdateHighScore(user, false);
            }
            else
            {
                game.Status = "Lose";
                await UpdateHighScore(user, false);
            }

            // Spara ändringar i databasen
            await _context.SaveChangesAsync();

            return Ok(game);
        }

        [HttpGet("inprogress")]
        [Authorize]
        public async Task<IActionResult> GetInProgressGames()
        {
            var userId = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new ArgumentNullException("userId");

            var games = await _context.BlackjackGames
                .Where(bg => bg.Player.Id == userId && bg.Status == "In Progress")
                .Include(bg => bg.PlayerHand).ThenInclude(ph => ph.Cards)
                .Include(bg => bg.DealerHand).ThenInclude(dh => dh.Cards)
                .ToListAsync();

            return Ok(games);
        }

        [HttpGet("{gameId}")]
        [Authorize]
        public async Task<IActionResult> GetGameById(int gameId)
        {
            var userId = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new ArgumentNullException("userId");

            var game = await _context.BlackjackGames
                .Include(bg => bg.PlayerHand).ThenInclude(ph => ph.Cards)
                .Include(bg => bg.DealerHand).ThenInclude(dh => dh.Cards)
                .Include(bg => bg.Player)
                .FirstOrDefaultAsync(bg => bg.Id == gameId && bg.Player.Id == userId);

            if (game == null) return NotFound("Game not found.");

            return Ok(game);
        }

        private async Task<BlackjackGame> GetInProgressGame(int gameId)
        {
            return await _context.BlackjackGames
                .Include(bg => bg.PlayerHand)
                .ThenInclude(ph => ph.Cards)
                .Include(bg => bg.DealerHand)
                .ThenInclude(dh => dh.Cards)
                .Include(bg => bg.Player)
                .FirstOrDefaultAsync(bg => bg.Id == gameId && bg.Status == "In Progress");
        }

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
        private async Task UpdateHighScore(AppUser user, bool isWin)
        {
            var highScore = await _context.HighScores.FirstOrDefaultAsync(hs => hs.UserId == user.Id);
            if (highScore == null)
            {
                highScore = new HighScore
                {
                    UserId = user.Id,
                    Wins = 0,
                    GamesPlayed = 0
                };
                _context.HighScores.Add(highScore);
            }

            highScore.GamesPlayed += 1;
            if (isWin)
            {
                highScore.Wins += 1;
            }

            await _context.SaveChangesAsync();
        }

    }

}
