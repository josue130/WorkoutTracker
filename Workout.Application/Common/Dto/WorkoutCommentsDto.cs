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
    public class WorkoutCommentsDto
    {
        public Guid? Id { get; set; }
        public Guid WorkoutId { get; set; }
        public string Comment { get; set; }
        public DateTime? Date { get; set; }
    }
}
