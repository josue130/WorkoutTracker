using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;
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
        private readonly IAuthService _authService;
        private readonly IWorkoutPlanService _workoutPlanService;
        public WorkoutCommentsService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService, IWorkoutPlanService workoutPlanService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
            _workoutPlanService = workoutPlanService;
        }
        public async Task<Result<string>> AddWorkoutComment(WorkoutCommentsDto model, ClaimsPrincipal user)
        {
            Result<Guid> getUserResult = _authService.GetUserId(user);
            if (getUserResult.IsFailure)
            {
                return Result<string>.Failure(getUserResult.Error);
            }
            Result<WorkoutPlan> accessResult = await _workoutPlanService.CheckAccess(model.WorkoutId, getUserResult.Values);
            if (accessResult.IsFailure)
            {
                return Result<string>.Failure(accessResult.Error);
            }

            if (string.IsNullOrWhiteSpace(model.Comment))
            {
                return Result<string>.Failure(CommentsError.CommentsCannotBeEmpty);
            }


            WorkoutComments workoutComments = WorkoutComments.Create(model.WorkoutId,model.Comment);
            await _unitOfWork.workoutsComments.Add(workoutComments);
            await _unitOfWork.Save();

            return Result<string>.Success("Comment added successfully");
        }

        public async Task<Result<string>> DeleteWorkoutComment(Guid workoutCommentId, ClaimsPrincipal user)
        {
            Result<Guid> getUserResult = _authService.GetUserId(user);
            if (getUserResult.IsFailure)
            {
                return Result<string>.Failure(getUserResult.Error);
            }
            Result<WorkoutComments> accessResult = await CheckAccess(workoutCommentId, getUserResult.Values);
            if (accessResult.IsFailure)
            {
                return Result<string>.Failure(accessResult.Error);
            }

            _unitOfWork.workoutsComments.Remove(accessResult.Values);
            await _unitOfWork.Save();

            return Result<string>.Success("Comment deleted successfully");
        }

        public async Task<Result<IEnumerable<WorkoutCommentsDto>>> GetWorkoutCommentsByWorkoutId(Guid workoutId, ClaimsPrincipal user)
        {
            Result<Guid> getUserResult = _authService.GetUserId(user);
            if (getUserResult.IsFailure)
            {
                return Result<IEnumerable<WorkoutCommentsDto>>.Failure(getUserResult.Error);
            }
            Result<WorkoutPlan> accessResult = await _workoutPlanService.CheckAccess(workoutId, getUserResult.Values);
            if (accessResult.IsFailure)
            {
                return Result<IEnumerable<WorkoutCommentsDto>>.Failure(accessResult.Error);
            }
            IEnumerable<WorkoutComments> workoutComments = await _unitOfWork.workoutsComments.GetWorkoutComments(workoutId);
            IEnumerable<WorkoutCommentsDto> workoutCommentsDto = _mapper.Map<IEnumerable<WorkoutCommentsDto>>(workoutComments);
            return Result<IEnumerable<WorkoutCommentsDto>>.Success(workoutCommentsDto);

        }

        public async Task<Result<string>> UpdateWorkoutComment(WorkoutCommentsDto model, ClaimsPrincipal user)
        {
            Result<Guid> getUserResult = _authService.GetUserId(user);
            if (getUserResult.IsFailure)
            {
                return Result<string>.Failure(getUserResult.Error);
            }
            Result<WorkoutComments> accessResult = await CheckAccess(model.Id, getUserResult.Values);
            if (accessResult.IsFailure)
            {
                return Result<string>.Failure(CommentsError.CommentNotFound);
            }

            if (string.IsNullOrWhiteSpace(model.Comment))
            {
                return Result<string>.Failure(CommentsError.CommentsCannotBeEmpty);
            }

            WorkoutComments workoutComments = WorkoutComments.Update((Guid)model.Id,model.WorkoutId, model.Comment);
            _unitOfWork.workoutsComments.Update(workoutComments);
            await _unitOfWork.Save();

            return Result<string>.Success("Comment updated successfully");
        }
        private async Task<Result<WorkoutComments>> CheckAccess(Guid? workoutCommentId, Guid userId)
        {
            WorkoutComments workoutComments = await _unitOfWork.workoutsComments.Get(wp => wp.Id == workoutCommentId && wp.Workout.UserId == userId);
            if (workoutComments == null)
            {
                return Result<WorkoutComments>.Failure(CommentsError.CommentNotFound);
            }
            return Result <WorkoutComments>.Success(workoutComments);
        }

    }
}
