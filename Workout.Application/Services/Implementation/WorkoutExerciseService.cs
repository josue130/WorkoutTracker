using AutoMapper;
using System.Security.Claims;
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
        public async Task AddWorkoutExercise(WorkoutExerciseDto model, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            await CheckAccessToWorkout(model.WorkoutId, userId);
            WorkoutExercise workoutExercise = WorkoutExercise.Create(model.ExerciseId, model.WorkoutId,model.Sets, model.Repetitions,model.Weight);
            await _unitOfWork.workoutExercises.Add(workoutExercise);
            await _unitOfWork.Save();

        }

        public async Task DeleteWorkoutExercise(Guid workoutExerciseId, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            WorkoutExercise workoutExercise = await CheckAccess(workoutExerciseId, userId);
            _unitOfWork.workoutExercises.Remove(workoutExercise);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<WorkoutExerciseDto>> GetWorkoutExerciseById(Guid workoutId, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            IEnumerable<WorkoutExercise> workoutExercise = await _unitOfWork.workoutExercises.GetWorkoutExercises(workoutId, userId);
            IEnumerable<WorkoutExerciseDto> workoutExerciseDto = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(workoutExercise);
            return workoutExerciseDto;

        }

        public async  Task UpdateWorkoutExercise(WorkoutExerciseDto model, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            await CheckAccess(model.Id, userId);
            WorkoutExercise workoutExercise = WorkoutExercise.Create(model.ExerciseId, model.WorkoutId, model.Sets, model.Repetitions, model.Weight);
            workoutExercise.Id = (Guid)model.Id;
            _unitOfWork.workoutExercises.Update(workoutExercise);
            await _unitOfWork.Save();
        }
        private async Task<WorkoutPlan> CheckAccessToWorkout(Guid? workoutPlanId, Guid userId)
        {
            WorkoutPlan workoutPlan = await _unitOfWork.workoutPlans.Get(wp => wp.Id == workoutPlanId && wp.UserId == userId);
            if (workoutPlan == null)
            {
                throw new UnauthorizedAccessException();
            }
            return workoutPlan;
        }
        private async Task<WorkoutExercise> CheckAccess(Guid? workoutExerciseId, Guid userId)
        {
            WorkoutExercise workoutExercise = await _unitOfWork.workoutExercises.Get(wp => wp.Id == workoutExerciseId && wp.Workout.UserId == userId);
            if (workoutExercise == null)
            {
                throw new UnauthorizedAccessException();
            }
            return workoutExercise;
        }
        private Guid CheckUserId(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException();
            }
            return Guid.Parse(userId);
        }
    }
}
