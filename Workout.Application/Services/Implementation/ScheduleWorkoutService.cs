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
    public class ScheduleWorkoutService : IScheduleWorkoutService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ScheduleWorkoutService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> DeleteScheduledWorkout(Guid scheduleWorkoutId, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            ScheduleWorkout scheduleWorkout = await CheckAccess(scheduleWorkoutId, userId);
            _unitOfWork.scheduleWorkouts.Remove(scheduleWorkout);
            await _unitOfWork.Save();

            return Result.Success("Scheduled workout deleted successfully.");

        }

        public async Task<Result> GetScheduleWorkoutsByUserId(ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            IEnumerable<ScheduleWorkout> scheduleWorkouts = await _unitOfWork.scheduleWorkouts.GetScheduleWorkouts(userId);
            IEnumerable<ScheduleWorkoutDto> scheduleWorkoutsDto = _mapper.Map<IEnumerable<ScheduleWorkoutDto>>(scheduleWorkouts);
            return Result.Success(scheduleWorkoutsDto);

        }

        public async Task<Result> SetWorkoutSchedule(ScheduleWorkoutDto model, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            await CheckAccessToWorkout(model.WorkoutId, userId);

            if (model.ScheduledDate < DateTime.Today) return Result.Failure(ScheduleWorkoutError.InvalidDate);

            ScheduleWorkout scheduleWorkout = ScheduleWorkout.Create(model.ScheduledDate,model.WorkoutId);
            await _unitOfWork.scheduleWorkouts.Add(scheduleWorkout);
            await _unitOfWork.Save();

            return Result.Success("Workout scheduled successfully.");

        }

        public async Task<Result> UpdateScheduledWorkout(ScheduleWorkoutDto model, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            await CheckAccessToWorkout(model.WorkoutId, userId);

            if (model.ScheduledDate < DateTime.Today) return Result.Failure(ScheduleWorkoutError.InvalidDate);

            ScheduleWorkout scheduleWorkout = ScheduleWorkout.Update((Guid)model.Id,model.ScheduledDate, model.WorkoutId);
            _unitOfWork.scheduleWorkouts.Update(scheduleWorkout);
            await _unitOfWork.Save();

            return Result.Success("Scheduled workout updated successfully.");

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
