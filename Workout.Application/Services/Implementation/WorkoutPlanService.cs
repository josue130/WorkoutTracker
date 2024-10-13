using AutoMapper;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
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
        public async Task AddWorkoutPlan(WorkoutPlanDto model)
        {
            WorkoutPlan workoutPlan = _mapper.Map<WorkoutPlan>(model);
            await _unitOfWork.workoutPlans.Add(workoutPlan);
            await _unitOfWork.Save();
        }

        public async Task DeleteWorkoutPlan(Guid workoutPlanId)
        {
            WorkoutPlan workoutPlan = await _unitOfWork.workoutPlans.Get(wp => wp.Id == workoutPlanId);
            _unitOfWork.workoutPlans.Remove(workoutPlan);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<WorkoutPlanResponseDto>> GenerateReport(Guid userId)
        {
            IEnumerable<WorkoutPlanResponseDto> workoutPlanResponseDtos = await _unitOfWork.workoutPlans.GenerateReport(userId);
            return workoutPlanResponseDtos;
        }

        public async Task<WorkoutPlanDto> GetByWorkouPlanId(Guid workoutPlanId)
        {
            WorkoutPlan workoutPlan = await _unitOfWork.workoutPlans.Get(wp => wp.Id == workoutPlanId);
            WorkoutPlanDto workoutPlanDto = _mapper.Map<WorkoutPlanDto>(workoutPlan);
            return workoutPlanDto;
        }

        public async Task<IEnumerable<WorkoutPlanResponseDto>> GetWorkoutsbyUserId(Guid userId)
        {
            IEnumerable<WorkoutPlanResponseDto> workoutPlanResponseDtos = await _unitOfWork.workoutPlans.GetAllWorkoutsByUserId(userId);
            return workoutPlanResponseDtos;
        }

        public async Task UpdateWorkoutPlan(WorkoutPlanDto model)
        {
            WorkoutPlan workoutPlan = _mapper.Map<WorkoutPlan>(model);
            _unitOfWork.workoutPlans.Update(workoutPlan);
            await _unitOfWork.Save();
        }
    }
}
