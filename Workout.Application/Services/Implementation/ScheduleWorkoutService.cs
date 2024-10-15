using AutoMapper;
using System.Security.Claims;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

namespace Workout.Application.Services.Implementation
{
    public class ScheduleWorkoutService : IScheduleWorkoutService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ScheduleWorkoutService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task DeleteScheduledWorkout(Guid scheduleWorkoutId, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            ScheduleWorkout scheduleWorkout = await CheckAccess(scheduleWorkoutId, userId);
            _unitOfWork.scheduleWorkouts.Remove(scheduleWorkout);
            await _unitOfWork.Save();
            
        }

        public async Task<IEnumerable<ScheduleWorkoutDto>> GetScheduleWorkoutsByUserId(ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            IEnumerable<ScheduleWorkout> scheduleWorkouts = await _unitOfWork.scheduleWorkouts.GetScheduleWorkouts(userId);
            IEnumerable<ScheduleWorkoutDto> scheduleWorkoutsDto = _mapper.Map<IEnumerable<ScheduleWorkoutDto>>(scheduleWorkouts);
            return scheduleWorkoutsDto;
        }

        public async Task SetWorkoutSchedule(ScheduleWorkoutDto model, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            await CheckAccessToWorkout(model.WorkoutId, userId);
            ScheduleWorkout scheduleWorkout = ScheduleWorkout.Create(model.ScheduledDate,model.WorkoutId);
            await _unitOfWork.scheduleWorkouts.Add(scheduleWorkout);
            await _unitOfWork.Save();
        }

        public async Task UpdateScheduledWorkout(ScheduleWorkoutDto model, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            await CheckAccessToWorkout(model.WorkoutId, userId);
            ScheduleWorkout scheduleWorkout = ScheduleWorkout.Create(model.ScheduledDate, model.WorkoutId);
            scheduleWorkout.Id = (Guid)model.Id;
            _unitOfWork.scheduleWorkouts.Update(scheduleWorkout);
            await _unitOfWork.Save();
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
        private async Task CheckAccessToWorkout(Guid? workoutPlanId, Guid userId)
        {
            WorkoutPlan workoutPlan = await _unitOfWork.workoutPlans.Get(wp => wp.Id == workoutPlanId && wp.UserId == userId);
            if (workoutPlan == null)
            {
                throw new UnauthorizedAccessException();
            }
        }
        private async Task<ScheduleWorkout> CheckAccess(Guid? sheduleWorkouId, Guid userId)
        {
            ScheduleWorkout scheduleWorkout = await _unitOfWork.scheduleWorkouts.Get(wp => wp.Id == sheduleWorkouId
                                               && wp.Workout.UserId == userId);
            if (scheduleWorkout == null)
            {
                throw new UnauthorizedAccessException();
            }
            return scheduleWorkout;
        }
    }
}
