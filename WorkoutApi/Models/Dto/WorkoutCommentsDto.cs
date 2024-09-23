using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WorkoutApi.Models.Dto
{
    public class WorkoutCommentsDto
    {
        public Guid Id { get; set; }
        public Guid WorkoutId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
