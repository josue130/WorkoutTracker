using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Result;
using Workout.Domain.Entities;

namespace Workout.Application.Services.Interface
{
    public interface IExerciseService 
    {
        Task<Result<IEnumerable<Exercise>>> GetAllExercises();
    }
}
