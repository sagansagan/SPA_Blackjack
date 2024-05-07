using Microsoft.AspNetCore.Identity;

namespace SPAgame.Server.Data
{
    public class AppUser : IdentityUser
    {
        public int HighScore { get; set; }
    }
}
