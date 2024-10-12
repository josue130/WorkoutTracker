using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Entities;
namespace Workout.Application.Common.Interfaces
{
    public interface IWorkoutPlansRepository : IRepository<WorkoutPlan>
    {
        void Update(WorkoutPlan model);

        //Task<IEnumerable<ListWorkoutsDto>> ListWorkouts(Guid UserId);

        Task<List<WorkoutPlan>> GenerateReport(Guid UserId);

    }

}
