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
            FullNameObject FullNameObj = new FullNameObject(fullName);
            EmailObject EmailObj = new EmailObject(email);
            UserNameObject UserNameObj = new UserNameObject(userName);
            return new User
            {
                Id = Guid.NewGuid(),
                FullName = FullNameObj.Value,
                Email = EmailObj.Value,
                UserName = UserNameObj.Value,
                Password = password
            };
        }
    }
}
