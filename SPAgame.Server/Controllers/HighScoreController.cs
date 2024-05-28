using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPAgame.Server.Data;
using System.Security.Claims;

namespace SPAgame.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HighScoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HighScoresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("topwins")]
        public async Task<IActionResult> GetTopWinsHighScores()
        {
            var highScores = await _context.HighScores
                .Include(hs => hs.User)
                .OrderByDescending(hs => hs.Wins)
                .Take(3)
                .ToListAsync();

            return Ok(highScores);
        }

        [HttpGet("topgamesplayed")]
        public async Task<IActionResult> GetTopGamesPlayedHighScores()
        {
            var highScores = await _context.HighScores
                .Include(hs => hs.User)
                .OrderByDescending(hs => hs.GamesPlayed)
                .Take(3)
                .ToListAsync();

            return Ok(highScores);
        }

        [HttpGet("userhighscore")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new ArgumentNullException("userId");
            var highScoreRecord = await _context.HighScores.FirstOrDefaultAsync(hs => hs.UserId == userClaim);

            if (highScoreRecord == null)
            {
                return NotFound();
            }

            var highScoreData = new
            {
                highScoreRecord.GamesPlayed,
                highScoreRecord.Wins,
            };

            return Ok(highScoreData);
        }
    }
}
