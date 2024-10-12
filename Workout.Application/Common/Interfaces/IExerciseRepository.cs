using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Entities;

namespace Workout.Application.Common.Interfaces
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        void Update(Exercise model);
    }
}
    

