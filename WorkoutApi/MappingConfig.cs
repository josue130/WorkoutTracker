using AutoMapper;
using WorkoutApi.Models;
using WorkoutApi.Models.Dto;

namespace WorkoutApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Exercise, ExerciseDto>().ReverseMap();
                config.CreateMap<Workout, WorkoutDto>().ReverseMap();
                config.CreateMap<WorkoutExercise, WorkoutExerciseDto>().ReverseMap();
                config.CreateMap<ScheduleWorkout, ScheduleWorkoutDto>().ReverseMap();
                config.CreateMap<WorkoutComments, WorkoutCommentsDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
