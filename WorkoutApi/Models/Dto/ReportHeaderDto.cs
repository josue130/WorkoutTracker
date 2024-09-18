namespace WorkoutApi.Models.Dto
{
    public class ReportHeaderDto
    {
        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public List<ReportDetailsDto>? ReportDetails { get; set; }

        /*
        public static ReportDto FromEntity(ScheduleWorkout entity )
        {

        }
        */

    }
}
