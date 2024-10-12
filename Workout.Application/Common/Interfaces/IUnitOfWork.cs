using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IWorkoutPlansRepository workoutPlans { get; }
        IWorkoutCommentsRepository workoutsComments { get; }
        IWorkoutExercisesRepository workoutExercises { get; }
        IScheduleWorkoutRepository scheduleWorkouts { get; }
        IExerciseRepository exercises { get; }
        Task Save();
    }
}
