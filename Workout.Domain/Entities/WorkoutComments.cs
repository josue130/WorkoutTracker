using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workout.Domain.Constants;

namespace Workout.Domain.Entities
{
    public class WorkoutComments
    {
        public Guid Id { get; set; }
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual WorkoutPlan Workout { get; set; } = null!;
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public static WorkoutComments Create(Guid workoutId, string comment)
        {
  
            if (string.IsNullOrWhiteSpace(comment))
            {
                throw new ArgumentException(ErrorMessages.CommentsCannotBeEmpty);
            }
            return new WorkoutComments
            {
                Id = Guid.NewGuid(),
                WorkoutId = workoutId,
                Comment = comment,
                Date = DateTime.Now
            };

        }
    }
}
