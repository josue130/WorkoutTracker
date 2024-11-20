using AutoMapper;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Common.Result;
using Workout.Application.Errors;
using Workout.Application.Services.Implementation;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

namespace ApplicationUnitTests
{
    public class WorkoutCommentsServiceTests
    {

        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IAuthService> _authServiceMock;
        private Mock<IWorkoutPlanService> _workoutPlanServiceMock;
        private WorkoutCommentsService _workoutCommentsService;
        public WorkoutCommentsServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _authServiceMock = new Mock<IAuthService>();
            _workoutPlanServiceMock = new Mock<IWorkoutPlanService>();
            _mapperMock = new Mock<IMapper>();
            _workoutCommentsService = new WorkoutCommentsService(_unitOfWorkMock.Object, _mapperMock.Object,
                _authServiceMock.Object, _workoutPlanServiceMock.Object);
        }
        private ClaimsPrincipal CreateUserClaimsPrincipal(Guid userId)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        }));
        }

        [Fact]
        public async Task AddWorkoutComment_WithEmptyComment_ShouldReturnFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new WorkoutCommentsDto { WorkoutId = Guid.NewGuid(), Comment = "" };
            var user = CreateUserClaimsPrincipal(userId);
            var workoutPlan = new WorkoutPlan(model.WorkoutId, "", "", userId);
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));


            _workoutPlanServiceMock.Setup(u => u.CheckAccess(model.WorkoutId, userId))
                .ReturnsAsync(Result<WorkoutPlan>.Success(workoutPlan));


            // Act
            var result = await _workoutCommentsService.AddWorkoutComment(model, user);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CommentsError.CommentsCannotBeEmpty);
        }

        [Fact]
        public async Task AddWorkoutComment_WithValidInputs_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new WorkoutCommentsDto { WorkoutId = Guid.NewGuid(), Comment = "Great workout!" };
            var user = CreateUserClaimsPrincipal(userId);
            var workoutPlan = new WorkoutPlan(model.WorkoutId, "", "", userId);
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));


            _workoutPlanServiceMock.Setup(u => u.CheckAccess(model.WorkoutId, userId))
                .ReturnsAsync(Result<WorkoutPlan>.Success(workoutPlan));

            _unitOfWorkMock.Setup(u => u.workoutsComments.Add(It.IsAny<WorkoutComments>()))
                           .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);

            // Act
            var result = await _workoutCommentsService.AddWorkoutComment(model, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be("Comment added successfully");
        }

        [Fact]
        public async Task DeleteWorkoutComment_WithUnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var workoutCommentId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));
            _unitOfWorkMock.Setup(u => u.workoutsComments.Get(It.IsAny<Expression<Func<WorkoutComments, bool>>>()))
                .ReturnsAsync((WorkoutComments)null);

            // Act
            var result = await _workoutCommentsService.DeleteWorkoutComment(workoutCommentId, user);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CommentsError.CommentNotFound);
        }

        [Fact]
        public async Task DeleteWorkoutComment_WithAuthorizedUser_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var workoutCommentId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            var workoutComment = WorkoutComments.Create(Guid.NewGuid(), "Nice session");
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));
            _unitOfWorkMock.Setup(u => u.workoutsComments.Get(It.IsAny<Expression<Func<WorkoutComments, bool>>>()))
                .ReturnsAsync(workoutComment);

            // Act
            var result = await _workoutCommentsService.DeleteWorkoutComment(workoutCommentId, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be("Comment deleted successfully");
        }

        [Fact]
        public async Task GetWorkoutCommentsByWorkoutId_WithValidUser_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var workoutId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            var workoutComments = new List<WorkoutComments>{WorkoutComments.Create(workoutId, "Nice session")};
            var workoutPlan = new WorkoutPlan(workoutId, "", "", userId);
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));
            _unitOfWorkMock.Setup(u => u.workoutsComments.GetWorkoutComments(workoutId))
                .ReturnsAsync(workoutComments);
            _workoutPlanServiceMock.Setup(u => u.CheckAccess(workoutId, userId))
                .ReturnsAsync(Result<WorkoutPlan>.Success(workoutPlan));
            var workoutCommentsDto = new List<WorkoutCommentsDto> { new WorkoutCommentsDto() };
            _mapperMock.Setup(m => m.Map<IEnumerable<WorkoutCommentsDto>>(workoutComments)).Returns(workoutCommentsDto);

            // Act
            var result = await _workoutCommentsService.GetWorkoutCommentsByWorkoutId(workoutId, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().BeEquivalentTo(workoutCommentsDto);
        }

        [Fact]
        public async Task UpdateWorkoutComment_WithEmptyComment_ShouldReturnFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var DtoModel = new WorkoutCommentsDto { Id = Guid.NewGuid(), WorkoutId = Guid.NewGuid(), Comment = "" };
            var model = new WorkoutComments ( Guid.NewGuid(), Guid.NewGuid(), "" ,DateTime.Now);
            var user = CreateUserClaimsPrincipal(userId);
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));
            _unitOfWorkMock.Setup(u => u.workoutsComments.Get(It.IsAny<Expression<Func<WorkoutComments, bool>>>()))
                .ReturnsAsync(model);

            // Act
            var result = await _workoutCommentsService.UpdateWorkoutComment(DtoModel, user);


            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CommentsError.CommentsCannotBeEmpty);
        }
    }
}
