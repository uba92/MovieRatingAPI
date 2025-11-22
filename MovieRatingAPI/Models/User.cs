using MovieRatingAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace MovieRatingAPI.Models
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
    }
}
