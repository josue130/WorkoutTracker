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
        private WorkoutCommentsService _workoutCommentsService;
        public WorkoutCommentsServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _workoutCommentsService = new WorkoutCommentsService(_unitOfWorkMock.Object, _mapperMock.Object);
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
            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                .ReturnsAsync(new WorkoutPlan(model.WorkoutId, "", "", userId));


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

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                .ReturnsAsync(new WorkoutPlan (  model.WorkoutId,"","",  userId ));

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
            var workoutCommentId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(Guid.NewGuid());

            _unitOfWorkMock.Setup(u => u.workoutsComments.Get(It.IsAny<Expression<Func<WorkoutComments, bool>>>()))
                .ReturnsAsync((WorkoutComments)null);

            // Act
            Func<Task> act = async () => await _workoutCommentsService.DeleteWorkoutComment(workoutCommentId, user);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Fact]
        public async Task DeleteWorkoutComment_WithAuthorizedUser_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var workoutCommentId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            var workoutComment = WorkoutComments.Create(Guid.NewGuid(), "Nice session");

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

            _unitOfWorkMock.Setup(u => u.workoutsComments.GetWorkoutComments(workoutId))
                .ReturnsAsync(workoutComments);
            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                .ReturnsAsync(new WorkoutPlan(workoutId, "", "", userId));

            var workoutCommentsDto = new List<WorkoutCommentsDto> { new WorkoutCommentsDto() };
            _mapperMock.Setup(m => m.Map<IEnumerable<WorkoutCommentsDto>>(workoutComments)).Returns(workoutCommentsDto);

            // Act
            var result = await _workoutCommentsService.GetWorkoutCommentsByWorkoutId(workoutId, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be(workoutCommentsDto);
        }

        [Fact]
        public async Task UpdateWorkoutComment_WithEmptyComment_ShouldReturnFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new WorkoutCommentsDto { Id = Guid.NewGuid(), WorkoutId = Guid.NewGuid(), Comment = "" };
            var user = CreateUserClaimsPrincipal(userId);
            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
               .ReturnsAsync(new WorkoutPlan(model.WorkoutId, "", "", userId));
            // Act
            var result = await _workoutCommentsService.UpdateWorkoutComment(model, user);


            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CommentsError.CommentsCannotBeEmpty);
        }
    }
}
