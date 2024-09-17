
namespace WorkoutApi.Models.Dto
{
    public class ScheduleWorkoutDto
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public DateTime ScheduledDate { get; set; }

    }
}
