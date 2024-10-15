using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Domain.Entities;

namespace Workout.Application.Services
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<WorkoutPlan, WorkoutPlanDto>().ReverseMap();
                config.CreateMap<WorkoutExercise, WorkoutExerciseDto>().ReverseMap();
                config.CreateMap<WorkoutComments, WorkoutCommentsDto>().ReverseMap(); 
                config.CreateMap<ScheduleWorkout, ScheduleWorkoutDto>().ReverseMap();


            });
            return mappingConfig;
        }
    }
}

