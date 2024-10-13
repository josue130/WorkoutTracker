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
    public class WorkoutCommentsService : IWorkoutCommentsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public WorkoutCommentsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddWorkoutComment(WorkoutCommentsDto model)
        {
            WorkoutComments workoutComments = _mapper.Map<WorkoutComments>(model);
            await _unitOfWork.workoutsComments.Add(workoutComments);
            await _unitOfWork.Save();
        }

        public async Task DeleteWorkoutComment(Guid workoutCoomentId)
        {
            WorkoutComments workoutComments = await _unitOfWork.workoutsComments.Get(wc => wc.Id == workoutCoomentId);
            _unitOfWork.workoutsComments.Remove(workoutComments);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<WorkoutCommentsDto>> GetWorkoutCommentsByWorkoutId(Guid workoutId)
        {
            IEnumerable<WorkoutComments> workoutComments = await _unitOfWork.workoutsComments.GetWorkoutComments(workoutId);
            IEnumerable<WorkoutCommentsDto> workoutCommentsDto = _mapper.Map<IEnumerable<WorkoutCommentsDto>>(workoutComments);
            return workoutCommentsDto;

        }

        public async Task UpdateWorkoutComment(WorkoutCommentsDto model)
        {
            WorkoutComments workoutComments = _mapper.Map<WorkoutComments>(model);
            _unitOfWork.workoutsComments.Update(workoutComments);
            await _unitOfWork.Save();
        }
    }
}
