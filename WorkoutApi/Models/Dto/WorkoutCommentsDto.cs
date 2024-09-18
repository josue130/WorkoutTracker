using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WorkoutApi.Models.Dto
{
    public class WorkoutCommentsDto
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
