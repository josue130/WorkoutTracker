using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Entities;

namespace Workout.Application.Common.Dto
{
    public class WorkoutExerciseDto
    {
        public Guid? Id { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public double Weight { get; set; }
        public int ExerciseId { get; set; }
        public Guid WorkoutId { get; set; }
    }
}
