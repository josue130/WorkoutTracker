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
        public WorkoutPlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> AddWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user)
        {
            if (string.IsNullOrWhiteSpace(model.Description) || string.IsNullOrWhiteSpace(model.Name))
            {
                return Result.Failure(WorkoutPlanError.InvalidInputs);
            }
            

            Guid userId = CheckUserId(user);
            var existingPlan = await CheckName(model.Name, userId);
            if (existingPlan != null)
            {
                return Result.Failure(WorkoutPlanError.WorkoutPlanNameAlreadyExists);
            }

            WorkoutPlan workoutPlan = WorkoutPlan.Create(model.Name, model.Description, userId);
            await _unitOfWork.workoutPlans.Add(workoutPlan);
            await _unitOfWork.Save();

            return Result.Success("WorkoutPlan added successfully.");
        }

        public async Task<Result> DeleteWorkoutPlan(Guid workoutPlanId, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            WorkoutPlan workoutPlan = await CheckAccess(workoutPlanId, userId);
            _unitOfWork.workoutPlans.Remove(workoutPlan);
            await _unitOfWork.Save();
            return Result.Success("WorkoutPlan deleted successfully");

        }

        public async Task<Result> GenerateReport(ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            IEnumerable<WorkoutPlanResponseDto> workoutPlanResponseDtos = await _unitOfWork.workoutPlans.GenerateReport(userId);
            return Result.Success(workoutPlanResponseDtos);
        }

        public async Task<Result> GetByWorkouPlanId(Guid workoutPlanId, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            WorkoutPlan workoutPlan = await CheckAccess(workoutPlanId, userId);
            WorkoutPlanDto workoutPlanDto = _mapper.Map<WorkoutPlanDto>(workoutPlan);
            return Result.Success(workoutPlanDto);
        }

        public async Task<Result> GetWorkoutsbyUserId(ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            IEnumerable<WorkoutPlanResponseDto> workoutPlanResponseDtos = await _unitOfWork.workoutPlans.GetAllWorkoutsByUserId(userId);
            return Result.Success(workoutPlanResponseDtos);
        }

        public async Task<Result> UpdateWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user)
        {
            if (string.IsNullOrWhiteSpace(model.Description) || string.IsNullOrWhiteSpace(model.Name))
            {
                return Result.Failure(WorkoutPlanError.InvalidInputs);
            }
            Guid userId = CheckUserId(user);
            WorkoutPlan data = await CheckAccess(model.Id, userId);
            if (data.Name != model.Name)
            {
                var existingPlan = await CheckName(model.Name, userId);
                if (existingPlan != null)
                {
                    return Result.Failure(WorkoutPlanError.WorkoutPlanNameAlreadyExists);
                }
            }
            WorkoutPlan workoutPlan = WorkoutPlan.Update((Guid)model.Id,model.Name, model.Description, userId);
            _unitOfWork.workoutPlans.Update(workoutPlan);
            await _unitOfWork.Save();
            return Result.Success("WorkoutPlan updated successfully");
        }
        private async Task<WorkoutPlan> CheckAccess(Guid? workoutPlanId, Guid userId) 
        {
            WorkoutPlan workoutPlan = await _unitOfWork.workoutPlans.Get(wp => wp.Id == workoutPlanId && wp.UserId == userId);
            if (workoutPlan == null)
            {
                throw new UnauthorizedAccessException();
            }
            return workoutPlan;
        }
        private async Task<WorkoutPlan?> CheckName(string name, Guid userId)
        {
            return await _unitOfWork.workoutPlans.Get(wp => wp.Name.ToLower() == name.ToLower() && wp.UserId == userId);
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
