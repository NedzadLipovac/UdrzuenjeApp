using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UdruzenjeApp.Data;
using UdruzenjeApp.Models;

namespace UdruzenjeApp_UnitTest
{
   public class ContextHelper
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public static IConfiguration Configuration { get; }
        public  UserManager<ApplicationUser> _userManager{get;}

        public UserManager<ApplicationUser> GetUserManager() { return _userManager; }


    }
}
