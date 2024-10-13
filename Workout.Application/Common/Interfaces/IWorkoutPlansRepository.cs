using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Domain.Entities;
namespace Workout.Application.Common.Interfaces
{
    public interface IWorkoutPlansRepository : IRepository<WorkoutPlan>
    {
        void Update(WorkoutPlan model);

        Task<IEnumerable<WorkoutPlanResponseDto>> GetAllWorkoutsByUserId(Guid userId);
        Task<IEnumerable<WorkoutPlanResponseDto>> GenerateReport(Guid userId);
    }

}
