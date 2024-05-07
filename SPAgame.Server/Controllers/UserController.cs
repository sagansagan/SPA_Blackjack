using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPAgame.Server.Data;
using System.Security.Claims;

namespace SPAgame.Server.Controllers
{
    [ApiController]
    [Route("api/user")]

    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet("/users")]
        public async Task<List<AppUser>> GetUsersAsync()
        {
            try 
            {
                var user = await _userManager.Users.ToListAsync();
                if (user == null)
                {
                    throw new Exception("Error fetching User");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }
            
        }
    }
    
}
