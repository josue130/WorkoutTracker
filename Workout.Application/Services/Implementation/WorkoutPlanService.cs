using AutoMapper;
using System.Reflection;
using System.Security.Claims;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Common.Result;
using Workout.Application.Errors;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;


namespace Workout.Application.Services.Implementation
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public WorkoutPlanService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<Result<string>> AddWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user)
        {

            Result<Guid> result = _authService.GetUserId(user);
            if (result.IsFailure)
            {
                return Result<string>.Failure(result.Error);
            }

            if (string.IsNullOrWhiteSpace(model.Description) || string.IsNullOrWhiteSpace(model.Name))
            {
                return Result<string>.Failure(WorkoutPlanError.InvalidInputs);
            }
            
            var existingPlan = await CheckName(model.Name, result.Values);
            if (existingPlan != null)
            {
                return Result<string>.Failure(WorkoutPlanError.WorkoutPlanNameAlreadyExists);
            }

            WorkoutPlan workoutPlan = WorkoutPlan.Create(model.Name, model.Description, result.Values);
            await _unitOfWork.workoutPlans.Add(workoutPlan);
            await _unitOfWork.Save();

            return Result<string>.Success("WorkoutPlan added successfully.");
        }

        public async Task<Result<string>> DeleteWorkoutPlan(Guid workoutPlanId, ClaimsPrincipal user)
        {
            Result<Guid> result = _authService.GetUserId(user);
            if (result.IsFailure)
            {
                return Result<string>.Failure(result.Error);
            }
            Result<WorkoutPlan> accessResult = await CheckAccess(workoutPlanId, result.Values);
            if (accessResult.IsFailure)
            {
                return Result<string>.Failure(accessResult.Error);
            }
            WorkoutPlan workoutPlan = accessResult.Values;
            _unitOfWork.workoutPlans.Remove(workoutPlan);
            await _unitOfWork.Save();
            return Result<string>.Success("WorkoutPlan deleted successfully");

        }

        public async Task<Result<IEnumerable<WorkoutPlanResponseDto>>> GenerateReport(ClaimsPrincipal user)
        {
            Result<Guid> result = _authService.GetUserId(user);
            if (result.IsFailure)
            {
                return Result<IEnumerable<WorkoutPlanResponseDto>>.Failure(result.Error);
            }
            IEnumerable<WorkoutPlanResponseDto> workoutPlanResponseDtos = await _unitOfWork.workoutPlans.GenerateReport(result.Values);
            return Result<IEnumerable<WorkoutPlanResponseDto>>.Success(workoutPlanResponseDtos);
        }

        public async Task<Result<WorkoutPlanDto>> GetByWorkouPlanId(Guid workoutPlanId, ClaimsPrincipal user)
        {
            Result<Guid> result = _authService.GetUserId(user);
            if (result.IsFailure)
            {
                return Result<WorkoutPlanDto>.Failure(result.Error);
            }
            Result<WorkoutPlan> accessResult = await CheckAccess(workoutPlanId, result.Values);
            if (accessResult.IsFailure)
            {
                return Result<WorkoutPlanDto>.Failure(accessResult.Error);
            }

            WorkoutPlan workoutPlan = accessResult.Values;
            WorkoutPlanDto workoutPlanDto = _mapper.Map<WorkoutPlanDto>(workoutPlan);
            return Result<WorkoutPlanDto>.Success(workoutPlanDto);
        }

        public async Task<Result<IEnumerable<WorkoutPlanResponseDto>>> GetWorkoutsbyUserId(ClaimsPrincipal user)
        {
            Result<Guid> result = _authService.GetUserId(user);
            if (result.IsFailure)
            {
                return Result<IEnumerable<WorkoutPlanResponseDto>>.Failure(result.Error);
            }
            IEnumerable<WorkoutPlanResponseDto> workoutPlanResponseDtos = await _unitOfWork.workoutPlans.GetAllWorkoutsByUserId(result.Values);
            return Result<IEnumerable<WorkoutPlanResponseDto>>.Success(workoutPlanResponseDtos);
        }

        public async Task<Result<string>> UpdateWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user)
        {
            if (string.IsNullOrWhiteSpace(model.Description) || string.IsNullOrWhiteSpace(model.Name))
            {
                return Result<string>.Failure(WorkoutPlanError.InvalidInputs);
            }

            Result<Guid> result = _authService.GetUserId(user);
            if (result.IsFailure)
            {
                return Result<string>.Failure(result.Error);
            }

            Result<WorkoutPlan> accessResult = await CheckAccess(model.Id, result.Values);
            WorkoutPlan workoutPlan = accessResult.Values;
            if (accessResult.IsFailure)
            {
                return Result<string>.Failure(accessResult.Error);
            }

            if (workoutPlan.Name != model.Name)
            {
                var existingPlan = await CheckName(model.Name, result.Values);
                if (existingPlan != null)
                {
                    return Result<string>.Failure(WorkoutPlanError.WorkoutPlanNameAlreadyExists);
                }
            }
            WorkoutPlan data = WorkoutPlan.Update((Guid)model.Id,model.Name, model.Description, result.Values);
            _unitOfWork.workoutPlans.Update(data);
            await _unitOfWork.Save();
            return Result<string>.Success("WorkoutPlan updated successfully");
        }

        public async Task<Result<WorkoutPlan>> CheckAccess(Guid? workoutPlanId, Guid userId) 
        {
            WorkoutPlan workoutPlan = await _unitOfWork.workoutPlans.Get(wp => wp.Id == workoutPlanId && wp.UserId == userId);
            if (workoutPlan == null)
            {
                return Result<WorkoutPlan>.Failure(WorkoutPlanError.WorkoutPlanNotFound);
            }
            return Result<WorkoutPlan>.Success(workoutPlan);
        }
        public async Task<WorkoutPlan?> CheckName(string name, Guid userId)
        {
            return await _unitOfWork.workoutPlans.Get(wp => wp.Name.ToLower() == name.ToLower() && wp.UserId == userId);
        }

        
    }
}
