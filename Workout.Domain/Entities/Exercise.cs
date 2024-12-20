﻿using System.ComponentModel.DataAnnotations;


namespace Workout.Domain.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
    }
}
