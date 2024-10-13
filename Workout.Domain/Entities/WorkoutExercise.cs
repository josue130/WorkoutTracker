﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workout.Domain.Entities
{
    public class WorkoutExercise
    {
        public Guid Id { get; set; }
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public virtual Exercise Exercise { get; set; } = null!;
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual WorkoutPlan Workout { get; set; } = null!;
        [Required]
        public int Sets { get; set; }
        [Required]
        public int Repetitions { get; set; }
        [Required]
        public double Weight { get; set; }
    }
}