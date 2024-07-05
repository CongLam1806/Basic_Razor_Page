using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace ShoppingCart.Utilities
{
    public class SystemHelper
    {
        public static bool IsAdmin(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var emailAdmin = config["AccountAdmin:Email"]!;
            var passwordAdmin = config["AccountAdmin:Password"]!;

            if (emailAdmin.Equals(email) && passwordAdmin.Equals(password))
            {
                return true;
            }

            return false;
        }

        

    }
}
