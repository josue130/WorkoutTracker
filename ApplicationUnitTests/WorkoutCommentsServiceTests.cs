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
using Workout.Application.Services.Implementation;
using Workout.Domain.Entities;

namespace ApplicationUnitTests
{
    public class WorkoutCommentsServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private WorkoutCommentsService _service;
        public WorkoutCommentsServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new WorkoutCommentsService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetWorkoutCommentsByWorkoutId_ShouldReturnComments_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutId = Guid.NewGuid();
            var comments = new List<WorkoutComments> { new WorkoutComments { WorkoutId = workoutId, Comment = "TEST" } };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync(new WorkoutPlan { Id = workoutId, UserId = userId });

            _unitOfWorkMock.Setup(u => u.workoutsComments.GetWorkoutComments(workoutId))
                           .ReturnsAsync(comments);

            _mapperMock.Setup(m => m.Map<IEnumerable<WorkoutCommentsDto>>(comments))
                       .Returns(new List<WorkoutCommentsDto>());

            // Act
            var result = await _service.GetWorkoutCommentsByWorkoutId(workoutId, user);

            // Assert
            result.Should().NotBeNull();
            _unitOfWorkMock.Verify(u => u.workoutsComments.GetWorkoutComments(workoutId), Times.Once);
        }



        [Fact]
        public async Task AddWorkoutComment_ShouldSucceed_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var commentDto = new WorkoutCommentsDto { WorkoutId = Guid.NewGuid(), Comment = "TEST" };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync(new WorkoutPlan { Id = commentDto.WorkoutId, UserId = userId });


            _unitOfWorkMock.Setup(u => u.workoutsComments.Add(It.IsAny<WorkoutComments>()))
                           .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);

            // Act
            await _service.AddWorkoutComment(commentDto, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.workoutsComments.Add(It.IsAny<WorkoutComments>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }
        [Fact]
        public async Task AddWorkoutComment_ShouldThrowUnauthorizedException_WhenUserHasNoAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var commentDto = new WorkoutCommentsDto { WorkoutId = Guid.NewGuid(), Comment = "TEST" };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync((WorkoutPlan)null);

            // Act
            Func<Task> act = async () => await _service.AddWorkoutComment(commentDto, user);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _unitOfWorkMock.Verify(u => u.Save(), Times.Never);
        }
        [Fact]
        public async Task DeleteWorkoutComment_ShouldSucceed_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var commentId = Guid.NewGuid();

            _unitOfWorkMock.Setup(u => u.workoutsComments.Get(It.IsAny<Expression<Func<WorkoutComments, bool>>>()))
                           .ReturnsAsync(new WorkoutComments { Id = commentId, Workout = new WorkoutPlan { UserId = userId } });

            // Act
            await _service.DeleteWorkoutComment(commentId, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.workoutsComments.Remove(It.IsAny<WorkoutComments>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteWorkoutComment_ShouldThrowUnauthorizedException_WhenUserHasNoAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var commentId = Guid.NewGuid();

            _unitOfWorkMock.Setup(u => u.workoutsComments.Get(It.IsAny<Expression<Func<WorkoutComments, bool>>>()))
                           .ReturnsAsync((WorkoutComments)null);

            // Act
            Func<Task> act = async () => await _service.DeleteWorkoutComment(commentId, user);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _unitOfWorkMock.Verify(u => u.Save(), Times.Never);
        }

        [Fact]
        public async Task UpdateWorkoutComment_ShouldSucceed_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var commentDto = new WorkoutCommentsDto { Id = Guid.NewGuid(), WorkoutId = Guid.NewGuid(), Comment = "TEST" };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync(new WorkoutPlan { Id = commentDto.WorkoutId, UserId = userId });


            _unitOfWorkMock.Setup(u => u.workoutsComments.Update(It.IsAny<WorkoutComments>()));

            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateWorkoutComment(commentDto, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.workoutsComments.Update(It.IsAny<WorkoutComments>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }


        private ClaimsPrincipal GetTestUser(Guid userId)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            return new ClaimsPrincipal(new ClaimsIdentity(claims));
        }

    }
}
