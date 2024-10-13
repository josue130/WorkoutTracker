using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Domain.Entities;

namespace Workout.Application.Services.Interface
{
    public interface IWorkoutCommentsService
    {
        Task<IEnumerable<WorkoutCommentsDto>> GetWorkoutCommentsByWorkoutId(Guid workoutId);
        Task AddWorkoutComment(WorkoutCommentsDto model);
        Task UpdateWorkoutComment(WorkoutCommentsDto model);
        Task DeleteWorkoutComment(Guid workoutCoomentId);
    }
}
