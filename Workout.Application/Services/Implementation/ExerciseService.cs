
using Workout.Application.Common.Interfaces;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

namespace Workout.Application.Services.Implementation
{
    public class ExerciseService : IExerciseService
    {

        private readonly IUnitOfWork _unitOfWork;
        public ExerciseService(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Exercise>> GetAllExercises()
        {
            return await _unitOfWork.exercises.GetAll();
        }
    }
}
