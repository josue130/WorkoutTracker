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

        public User(Guid id, string fullName, string email, string userName, string password)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            UserName = userName;
            Password = password;
        }

        public static User Create(string fullName, string email, string userName, string password)
        {
            return new User(Guid.NewGuid(), fullName, email, userName, password);
        }
        private User()
        {
            
        }
    }
}
