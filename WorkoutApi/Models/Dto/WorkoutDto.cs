

namespace WorkoutApi.Models.Dto
{
    public class WorkoutDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
