namespace WorkoutApi.Models.Dto
{
    public class ReportDetailsDto
    {
        public string ExerciseName { get; set; } = string.Empty; 
        public int Sets { get; set; } 
        public int Repetitions { get; set; } 
        public double Weight { get; set; } 
    }
}
