
using Workout.Application.Common.Interfaces;
using Workout.Application.Common.Result;
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
        public async Task<Result<IEnumerable<Exercise>>> GetAllExercises()
        {
            return Result<IEnumerable<Exercise>>.Success(await _unitOfWork.exercises.GetAll()); 
                
        }
    }
}
