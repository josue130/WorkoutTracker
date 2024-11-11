using AutoMapper;
using System.Security.Claims;
using System.Xml.Linq;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Common.Result;
using Workout.Application.Errors;
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
        public async Task<Result> AddWorkoutComment(WorkoutCommentsDto model, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            await CheckAccessToWorkout(model.WorkoutId, userId);

            if (string.IsNullOrWhiteSpace(model.Comment))
            {
                return Result.Failure(CommentsError.CommentsCannotBeEmpty);
            }


            WorkoutComments workoutComments = WorkoutComments.Create(model.WorkoutId,model.Comment);
            await _unitOfWork.workoutsComments.Add(workoutComments);
            await _unitOfWork.Save();

            return Result.Success("Comment added successfully");
        }

        public async Task<Result> DeleteWorkoutComment(Guid workoutCommentId, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            WorkoutComments workoutComments = await CheckAccess(workoutCommentId, userId);
            _unitOfWork.workoutsComments.Remove(workoutComments);
            await _unitOfWork.Save();

            return Result.Success("Comment deleted successfully");
        }

        public async Task<Result> GetWorkoutCommentsByWorkoutId(Guid workoutId, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            await CheckAccessToWorkout(workoutId, userId);
            IEnumerable<WorkoutComments> workoutComments = await _unitOfWork.workoutsComments.GetWorkoutComments(workoutId);
            IEnumerable<WorkoutCommentsDto> workoutCommentsDto = _mapper.Map<IEnumerable<WorkoutCommentsDto>>(workoutComments);
            return Result.Success(workoutCommentsDto);

        }

        public async Task<Result> UpdateWorkoutComment(WorkoutCommentsDto model, ClaimsPrincipal user)
        {
            Guid userId = CheckUserId(user);
            await CheckAccessToWorkout(model.WorkoutId, userId);

            if (string.IsNullOrWhiteSpace(model.Comment))
            {
                return Result.Failure(CommentsError.CommentsCannotBeEmpty);
            }

            WorkoutComments workoutComments = WorkoutComments.Update((Guid)model.Id,model.WorkoutId, model.Comment);
            _unitOfWork.workoutsComments.Update(workoutComments);
            await _unitOfWork.Save();

            return Result.Success("Comment updated successfully");
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
        private async Task<WorkoutComments> CheckAccess(Guid? workoutCommentId, Guid userId)
        {
            WorkoutComments workoutComments = await _unitOfWork.workoutsComments.Get(wp => wp.Id == workoutCommentId && wp.Workout.UserId == userId);
            if (workoutComments == null)
            {
                throw new UnauthorizedAccessException();
            }
            return workoutComments;
        }

    }
}
