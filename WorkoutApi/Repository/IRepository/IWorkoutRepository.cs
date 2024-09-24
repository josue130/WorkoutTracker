using WorkoutApi.Models;
using WorkoutApi.Models.Dto;

namespace WorkoutApi.Repository.IRepository
{
    public interface IWorkoutRepository : IRepository<Workout>
    {
        void Update(Workout model);

        Task<IEnumerable<ListWorkoutsDto>> ListWorkouts(Guid UserId);

        Task<List<ReportHeaderDto>> GenerateReport(Guid UserId);

    }
}
