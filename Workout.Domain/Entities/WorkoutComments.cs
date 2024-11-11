using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Workout.Domain.Entities
{
    public class WorkoutComments
    {
        public Guid Id { get; set; }
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual WorkoutPlan Workout { get; set; } = null!;
        [Required]
        public string Comment { get; set; } = string.Empty;
        [Required]
        public DateTime Date { get; set; }

        public static WorkoutComments Create(Guid workoutId, string comment)
        {
            return new WorkoutComments
            {
                Id = Guid.NewGuid(),
                WorkoutId = workoutId,
                Comment = comment,
                Date = DateTime.Now
            };

        }
        public static WorkoutComments Update(Guid id,Guid workoutId, string comment)
        {
            return new WorkoutComments
            {
                Id = id,
                WorkoutId = workoutId,
                Comment = comment,
                Date = DateTime.Now
            };

        }
        public WorkoutComments(Guid id, Guid workoutId, string comment,DateTime date)
        {
            Id = id;
            WorkoutId = workoutId;
            Comment = comment;
            Date = date;
        }
        private WorkoutComments()
        {
            
        }
    }
}
