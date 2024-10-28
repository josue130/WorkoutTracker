using AutoMapper;
using System.Reflection;
using System.Security.Claims;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;
using Workout.Domain.Exceptions;

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
        public async Task AddWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user)
        {
            
            Guid userId = CheckUserId(user);
            var existingPlan = await CheckName(model.Name, userId);
            if (existingPlan != null)
            {
                throw new WorkoutPlanNameAlreadyExistsException();
            }

            WorkoutPlan workoutPlan = WorkoutPlan.Create(model.Name, model.Description, userId);
            await _unitOfWork.workoutPlans.Add(workoutPlan);
            await _unitOfWork.Save();
        }

        public async Task DeleteWorkoutPlan(Guid workoutPlanId, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            WorkoutPlan workoutPlan = await CheckAccess(workoutPlanId, userId);
            _unitOfWork.workoutPlans.Remove(workoutPlan);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<WorkoutPlanResponseDto>> GenerateReport(ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            IEnumerable<WorkoutPlanResponseDto> workoutPlanResponseDtos = await _unitOfWork.workoutPlans.GenerateReport(userId);
            return workoutPlanResponseDtos;
        }

        public async Task<WorkoutPlanDto> GetByWorkouPlanId(Guid workoutPlanId, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            WorkoutPlan workoutPlan = await CheckAccess(workoutPlanId, userId);
            WorkoutPlanDto workoutPlanDto = _mapper.Map<WorkoutPlanDto>(workoutPlan);
            return workoutPlanDto;
        }

        public async Task<IEnumerable<WorkoutPlanResponseDto>> GetWorkoutsbyUserId(ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            IEnumerable<WorkoutPlanResponseDto> workoutPlanResponseDtos = await _unitOfWork.workoutPlans.GetAllWorkoutsByUserId(userId);
            return workoutPlanResponseDtos;
        }

        public async Task UpdateWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            WorkoutPlan data = await CheckAccess(model.Id, userId);
            if (data.Name != model.Name)
            {
                var existingPlan = await CheckName(model.Name, userId);
                if (existingPlan != null)
                {
                    throw new WorkoutPlanNameAlreadyExistsException();
                }
            }
            WorkoutPlan workoutPlan = WorkoutPlan.Create(model.Name, model.Description, userId);
            workoutPlan.Id = data.Id;
            _unitOfWork.workoutPlans.Update(workoutPlan);
            await _unitOfWork.Save();
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
