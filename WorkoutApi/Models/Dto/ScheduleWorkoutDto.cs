
namespace WorkoutApi.Models.Dto
{
    public class ScheduleWorkoutDto
    {
        public Guid Id { get; set; }
        public Guid WorkoutId { get; set; }
        public DateTime ScheduledDate { get; set; }

    }
}
