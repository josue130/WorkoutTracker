using AutoMapper;
using System.Security.Claims;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Common.Result;
using Workout.Application.Errors;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

namespace Workout.Application.Services.Implementation
{
    public class WorkoutExerciseService : IWorkoutExerciseService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IWorkoutPlanService _workoutPlanService;
        public WorkoutExerciseService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService, IWorkoutPlanService workoutPlanService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
            _workoutPlanService = workoutPlanService;
        }
        public async Task<Result<string>> AddWorkoutExercise(WorkoutExerciseDto model, ClaimsPrincipal user)
        {
            Result<Guid> getUserResult = _authService.GetUserId(user);
            if (getUserResult.IsFailure)
            {
                return Result<string>.Failure(getUserResult.Error);
            }

            Result<WorkoutPlan> accessResult = await _workoutPlanService.CheckAccess(model.WorkoutId, getUserResult.Values);

            if (accessResult.IsFailure)
            {
                return Result<string>.Failure(accessResult.Error);
            }

            if (model.Sets <= 0) return Result<string>.Failure(WorkoutExerciseError.InvalidSets);
            if (model.Repetitions <= 0) return Result<string>.Failure(WorkoutExerciseError.InvalidRepetitions);
            if (model.Weight < 0) return Result<string>.Failure(WorkoutExerciseError.InvalidWeight);

            WorkoutExercise workoutExercise = WorkoutExercise.Create(model.ExerciseId, model.WorkoutId,model.Sets, model.Repetitions,model.Weight);
            await _unitOfWork.workoutExercises.Add(workoutExercise);
            await _unitOfWork.Save();

            return Result<string>.Success("Exercise added successfully.");

        }

        public async Task<Result<string>> DeleteWorkoutExercise(Guid workoutExerciseId, ClaimsPrincipal user)
        {
            Result<Guid> getUserResult = _authService.GetUserId(user);
            if (getUserResult.IsFailure)
            {
                return Result<string>.Failure(getUserResult.Error);
            }

            Result<WorkoutExercise> deleteResult = await CheckAccess(workoutExerciseId, (Guid)getUserResult.Values);

            if (deleteResult.IsFailure)
            {
                return Result<string>.Failure(deleteResult.Error);
            }
            _unitOfWork.workoutExercises.Remove(deleteResult.Values);
            await _unitOfWork.Save();

            return Result<string>.Success("Workout exercise deleted successfully");
        }

        public async Task<Result<IEnumerable<WorkoutExerciseDto>>> GetWorkoutExerciseById(Guid workoutId, ClaimsPrincipal user)
        {
            Result<Guid> getUserResult = _authService.GetUserId(user);

            if (getUserResult.IsFailure)
            {
                return Result<IEnumerable<WorkoutExerciseDto>>.Failure(getUserResult.Error);
            }
            IEnumerable<WorkoutExercise> workoutExercise = await _unitOfWork.workoutExercises.GetWorkoutExercises(workoutId, getUserResult.Values);
            IEnumerable<WorkoutExerciseDto> workoutExerciseDto = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(workoutExercise);
            return Result<IEnumerable<WorkoutExerciseDto>>.Success(workoutExerciseDto);

        }

        public async Task<Result<string>> UpdateWorkoutExercise(WorkoutExerciseDto model, ClaimsPrincipal user)
        {
            Result<Guid> getUserResult = _authService.GetUserId(user);
            if (getUserResult.IsFailure)
            {
                return Result<string>.Failure(getUserResult.Error);
            }
            Result<WorkoutExercise> updateResult = await CheckAccess(model.Id, (Guid)getUserResult.Values);
            if (updateResult.IsFailure)
            {
                return Result<string>.Failure(updateResult.Error);
            }

            WorkoutExercise workoutExercise = WorkoutExercise.Update((Guid)model.Id,model.ExerciseId, model.WorkoutId, model.Sets, model.Repetitions, model.Weight);
            _unitOfWork.workoutExercises.Update(workoutExercise);
            await _unitOfWork.Save();
            return Result<string>.Success("Workout exercise updated successfully");
        }

        private async Task<Result<WorkoutExercise>> CheckAccess(Guid? workoutExerciseId, Guid userId)
        {
            WorkoutExercise workoutExercise = await _unitOfWork.workoutExercises.Get(wp => wp.Id == workoutExerciseId && wp.Workout.UserId == userId);
            if (workoutExercise == null)
            {
                return Result<WorkoutExercise>.Failure(WorkoutExerciseError.WorkoutExerciseNotFound);
            }
            return Result<WorkoutExercise>.Success(workoutExercise);
        }
        
    }
}
