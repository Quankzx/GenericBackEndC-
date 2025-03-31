using Services.Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace Services.Authen.Domain.Entities
{
   public class User:BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // Mặc định User
        [MaxLength(2000)]
        public string? RefeshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
