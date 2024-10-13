using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task DeleteScheduledWorkout(Guid scheduleWorkoutId)
        {
            ScheduleWorkout scheduleWorkout = await _unitOfWork.scheduleWorkouts.Get(sw => sw.Id == scheduleWorkoutId);
            _unitOfWork.scheduleWorkouts.Remove(scheduleWorkout);
            await _unitOfWork.Save();
            
        }

        public async Task<IEnumerable<ScheduleWorkoutDto>> GetScheduleWorkoutsByUserId(Guid userId)
        {
            IEnumerable<ScheduleWorkout> scheduleWorkouts = await _unitOfWork.scheduleWorkouts.GetScheduleWorkouts(userId);
            IEnumerable<ScheduleWorkoutDto> scheduleWorkoutsDto = _mapper.Map<IEnumerable<ScheduleWorkoutDto>>(scheduleWorkouts);
            return scheduleWorkoutsDto;
        }

        public async Task SetWorkoutSchedule(ScheduleWorkoutDto model)
        {
            ScheduleWorkout scheduleWorkout = _mapper.Map<ScheduleWorkout>(model);
            await _unitOfWork.scheduleWorkouts.Add(scheduleWorkout);
            await _unitOfWork.Save();
        }

        public async Task UpdateScheduledWorkout(ScheduleWorkoutDto model)
        {
            ScheduleWorkout scheduleWorkout = _mapper.Map<ScheduleWorkout>(model);
            _unitOfWork.scheduleWorkouts.Update(scheduleWorkout);
            await _unitOfWork.Save();
        }
    }
}
