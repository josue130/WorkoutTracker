using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workout.Domain.ValueObjects;

namespace Workout.Domain.Entities
{
    public class WorkoutPlan
    {
        public Guid Id { get; set; }
        [Required]
        public  string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();

        public static WorkoutPlan Create(string name, string description, Guid userId)
        {
           
            return new WorkoutPlan
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                UserId = userId
            };
        }
        public static WorkoutPlan Update(Guid id,string name, string description, Guid userId)
        {

            return new WorkoutPlan
            {
                Id = id,
                Name = name,
                Description = description,
                UserId = userId
            };
        }

        public WorkoutPlan(Guid id, string name, string description, Guid userId)
        {
            Id = id;
            Name = name;
            Description = description;
            UserId = userId;
        }
        private WorkoutPlan()
        {
            
        }

    }
}
