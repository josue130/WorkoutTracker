using System.ComponentModel.DataAnnotations;

namespace WorkoutApi.Models.Dto
{
    public class UserDto
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
