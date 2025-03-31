using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Services.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : Controller
{
    protected Guid UID
    {
        get
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? Guid.Parse(userId) : Guid.Empty;
        }
    }
    protected string Role
    {
        get
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            return userRole != null ? userRole : string.Empty;
        }
    }
    protected string Email
    {
        get
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            return userEmail != null ? userEmail : string.Empty;
        }
    }
}
