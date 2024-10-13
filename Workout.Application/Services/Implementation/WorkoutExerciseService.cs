using AutoMapper;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

namespace Workout.Application.Services.Implementation
{
    public class WorkoutExerciseService : IWorkoutExerciseService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public WorkoutExerciseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddWorkoutExercise(WorkoutExerciseDto model)
        {
            WorkoutExercise workoutExercise = _mapper.Map<WorkoutExercise>(model);
            await _unitOfWork.workoutExercises.Add(workoutExercise);
            await _unitOfWork.Save();

        }

        public async Task DeleteWorkoutExercise(Guid workoutExerciseId)
        {
            WorkoutExercise workoutExercise = await _unitOfWork.workoutExercises.Get(we => we.Id == workoutExerciseId);
            _unitOfWork.workoutExercises.Remove(workoutExercise);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<WorkoutExerciseDto>> GetWorkoutExerciseById(Guid workoutId)
        {
            IEnumerable<WorkoutExercise> workoutExercise = await _unitOfWork.workoutExercises.GetWorkoutExercises(workoutId);
            IEnumerable<WorkoutExerciseDto> workoutExerciseDto = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(workoutExercise);
            return workoutExerciseDto;

        }

        public async  Task UpdateWorkoutExercise(WorkoutExerciseDto model)
        {
            WorkoutExercise workoutExercise = _mapper.Map<WorkoutExercise>(model);
            _unitOfWork.workoutExercises.Update(workoutExercise);
            await _unitOfWork.Save();
        }
    }
}
