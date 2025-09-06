using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WEB_153503_IdentityServer.Models;

namespace WEB_153503_IdentityServer.Controllers;

[Route("[controller]")]
[Authorize]
[ApiController]
public class AvatarController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly UserManager<ApplicationUser> _userMgr;

    public AvatarController(IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userMgr)
    {
        _webHostEnvironment = webHostEnvironment;
        _userMgr = userMgr;
    }

    [HttpGet]
    public async Task<IActionResult> GetAvatar()    
    {
        var userId = _userMgr.GetUserId(User);

        if (string.IsNullOrEmpty(userId))
        {
            return NotFound("User not found");
        }

        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", userId);
        var possibleExtensions = new[] { ".jpg", ".png", ".gif" }; // Здесь перечислите возможные расширения
        string mimeType = "application/octet-stream"; // MIME-тип по умолчанию

        var provider = new FileExtensionContentTypeProvider();
        var fileExt = ".png";

        foreach (var ext in possibleExtensions)
        {
            var filePath = imagePath + ext;
            if (System.IO.File.Exists(filePath))
            {
                fileExt = ext;
                if (provider.TryGetContentType(ext, out mimeType))
                {
                    break; // MIME-тип найден, можно завершить цикл
                }
            }
        }

        imagePath = imagePath + fileExt;

        byte[] imageBytes;
        using (var stream = System.IO.File.OpenRead(imagePath))
        using (var memoryStream = new MemoryStream())
        {
            stream.CopyTo(memoryStream);
            imageBytes = memoryStream.ToArray();
        }

        return File(imageBytes, mimeType);
    }

}
