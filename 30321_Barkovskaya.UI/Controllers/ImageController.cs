using _30321_Barkovskaya.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _30321_Barkovskaya.UI.Controllers
{
    public class ImageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ImageController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> GetAvatar()
        {
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userManager.FindByEmailAsync(email);
            var imagePath = Path.Combine("images", "default-avatar.png");

            if (user == null) return NotFound();

            return user.Avatar != null ? File(user.Avatar, user.MimeType) : File(imagePath, "image/png");
        }
    }
}
