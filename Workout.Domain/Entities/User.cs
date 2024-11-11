using System.ComponentModel.DataAnnotations;
using Workout.Domain.ValueObjects;

namespace Workout.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        public static User Create(string fullName, string email, string userName, string password)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FullName = fullName,
                Email = email,
                UserName = userName,
                Password = password
            };
        }
    }
}
