using Microsoft.AspNetCore.Identity;
using System.Security;

namespace _30321_Barkovskaya.UI.Data
{
    public class AppUser : IdentityUser
    {
        public byte[]? Avatar { get; set; } = null;
        public string MimeType { get; set; } = string.Empty;
    }
}
