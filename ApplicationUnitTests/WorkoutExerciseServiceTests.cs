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
    public class WorkoutExerciseServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private WorkoutExerciseService _service;

        /*
        public WorkoutExerciseServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new WorkoutExerciseService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetWorkoutExerciseById_ShouldReturnExercises_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutId = Guid.NewGuid();

            var workoutExercises = new List<WorkoutExercise> { new WorkoutExercise { WorkoutId = workoutId } };
            _unitOfWorkMock.Setup(u => u.workoutExercises.GetWorkoutExercises(workoutId, userId))
                           .ReturnsAsync(workoutExercises);

            _mapperMock.Setup(m => m.Map<IEnumerable<WorkoutExerciseDto>>(workoutExercises))
                       .Returns(new List<WorkoutExerciseDto>());

            // Act
            var result = await _service.GetWorkoutExerciseById(workoutId, user);

            // Assert
            result.Should().NotBeNull();
            _unitOfWorkMock.Verify(u => u.workoutExercises.GetWorkoutExercises(workoutId, userId), Times.Once);
        }


        [Fact]
        public async Task AddWorkoutExercise_ShouldSucceed_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutDto = new WorkoutExerciseDto
            {
                WorkoutId = Guid.NewGuid(),
                ExerciseId = 2,
                Sets = 3,
                Repetitions = 10,
                Weight = 50
            };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync(new WorkoutPlan { Id = workoutDto.WorkoutId, UserId = userId });

            _unitOfWorkMock.Setup(u => u.workoutExercises.Add(It.IsAny<WorkoutExercise>()))
                           .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);

            // Act
            await _service.AddWorkoutExercise(workoutDto, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.workoutExercises.Add(It.IsAny<WorkoutExercise>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task AddWorkoutExercise_ShouldThrowUnauthorizedException_WhenUserHasNoAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutDto = new WorkoutExerciseDto { WorkoutId = Guid.NewGuid(), ExerciseId = 2 };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync((WorkoutPlan)null);

            // Act
            Func<Task> act = async () => await _service.AddWorkoutExercise(workoutDto, user);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _unitOfWorkMock.Verify(u => u.Save(), Times.Never);
        }

        [Fact]
        public async Task DeleteWorkoutExercise_ShouldSucceed_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutExerciseId = Guid.NewGuid();

            _unitOfWorkMock.Setup(u => u.workoutExercises.Get(It.IsAny<Expression<Func<WorkoutExercise, bool>>>()))
                           .ReturnsAsync(new WorkoutExercise { Id = workoutExerciseId, Workout = new WorkoutPlan { UserId = userId } });

            // Act
            await _service.DeleteWorkoutExercise(workoutExerciseId, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.workoutExercises.Remove(It.IsAny<WorkoutExercise>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }
        [Fact]
        public async Task DeleteWorkoutExercise_ShouldThrowUnauthorizedException_WhenUserHasNoAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutExerciseId = Guid.NewGuid();

            _unitOfWorkMock.Setup(u => u.workoutExercises.Get(It.IsAny<Expression<Func<WorkoutExercise, bool>>>()))
                           .ReturnsAsync((WorkoutExercise)null);

            // Act
            Func<Task> act = async () => await _service.DeleteWorkoutExercise(workoutExerciseId, user);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _unitOfWorkMock.Verify(u => u.Save(), Times.Never);
        }

        [Fact]
        public async Task UpdateWorkoutExercise_ShouldSucceed_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutDto = new WorkoutExerciseDto { Id = Guid.NewGuid(), WorkoutId = Guid.NewGuid(), Sets = 4, Repetitions = 12, Weight = 60 };

            _unitOfWorkMock.Setup(u => u.workoutExercises.Get(It.IsAny<Expression<Func<WorkoutExercise, bool>>>()))
                           .ReturnsAsync(new WorkoutExercise { Id = (Guid)workoutDto.Id, Workout = new WorkoutPlan { UserId = userId } });

            // Act
            await _service.UpdateWorkoutExercise(workoutDto, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.workoutExercises.Update(It.IsAny<WorkoutExercise>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }


        private ClaimsPrincipal GetTestUser(Guid userId)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            return new ClaimsPrincipal(new ClaimsIdentity(claims));
        }
        */
    }
}
