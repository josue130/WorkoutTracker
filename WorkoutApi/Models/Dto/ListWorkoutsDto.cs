namespace WorkoutApi.Models.Dto
{
    public class ListWorkoutsDto
    {
        public Guid Id { get; set; }
        public Guid WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}
