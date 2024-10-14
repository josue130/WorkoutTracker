
using System.ComponentModel.DataAnnotations;

namespace Workout.Application.Common.Dto
{
    public class WorkoutPlanDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? UserId { get; set; }

    }
}
