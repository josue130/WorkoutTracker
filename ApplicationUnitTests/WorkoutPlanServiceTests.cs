using AutoMapper;
using System.Linq.Expressions;
using System.Security.Claims;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Services.Implementation;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;
using Workout.Domain.Exceptions;

namespace ApplicationUnitTests
{
    public class WorkoutPlanServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly WorkoutPlanService _service;

        public WorkoutPlanServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new WorkoutPlanService(_unitOfWorkMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task AddWorkoutPlan_ShouldSucceed_WhenNameIsUnique()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutDto = new WorkoutPlanDto { Name = "Plan A", Description = "Desc" };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync((WorkoutPlan)null); 

            _unitOfWorkMock.Setup(u => u.workoutPlans.Add(It.IsAny<WorkoutPlan>()))
                           .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);

            // Act
            await _service.AddWorkoutPlan(workoutDto, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.workoutPlans.Add(It.IsAny<WorkoutPlan>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task AddWorkoutPlan_ShouldThrowException_WhenNameAlreadyExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutDto = new WorkoutPlanDto { Name = "Plan A", Description = "Desc" };

            var existingWorkout = new WorkoutPlan { Name = "Plan A", UserId = userId };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync(existingWorkout);

            // Act
            Func<Task> act = async () => await _service.AddWorkoutPlan(workoutDto, user);

            // Assert
            await act.Should().ThrowAsync<WorkoutPlanNameAlreadyExistsException>();
            _unitOfWorkMock.Verify(u => u.workoutPlans.Add(It.IsAny<WorkoutPlan>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Never);
        }
        [Fact]
        public async Task UpdateWorkoutPlan_ShouldThrowException_WhenTheNewNameAlreadyExists() 
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutDto = new WorkoutPlanDto { Id = Guid.NewGuid(), Name = "Plan B", Description = "Desc", UserId = userId };

            var currentPlan = new WorkoutPlan { Id = (Guid)workoutDto.Id, Name = "Plan A", UserId = userId };
            var existingPlan = new WorkoutPlan { Name = "Plan B", UserId = userId }; 

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync((Expression<Func<WorkoutPlan, bool>> predicate) =>
                                predicate.Compile()(existingPlan) ? existingPlan : currentPlan);

            // Act
            Func<Task> act = async () => await _service.UpdateWorkoutPlan(workoutDto, user);

            // Assert
            await act.Should().ThrowAsync<WorkoutPlanNameAlreadyExistsException>();
            _unitOfWorkMock.Verify(u => u.workoutPlans.Add(It.IsAny<WorkoutPlan>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Never);
        }
        [Fact]
        public async Task UpdateWorkoutPlan_ShouldSucceed_WhenNameIsUnique()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutDto = new WorkoutPlanDto { Id = Guid.NewGuid(), Name = "Plan B", Description = "Desc", UserId = userId };

            var currentPlan = new WorkoutPlan { Id = (Guid)workoutDto.Id, Name = "Plan A", UserId = userId };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync((Expression<Func<WorkoutPlan, bool>> predicate) =>
                                predicate.Compile()(currentPlan) ? currentPlan : null);

            // Act
            await _service.UpdateWorkoutPlan(workoutDto, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.workoutPlans.Update(It.IsAny<WorkoutPlan>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteWorkoutPlan_ShouldSucceed_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutPlanId = Guid.NewGuid();
            var workoutPlan = new WorkoutPlan { Id = workoutPlanId, UserId = userId };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync(workoutPlan);

            _unitOfWorkMock.Setup(u => u.workoutPlans.Remove(workoutPlan));
                           

            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteWorkoutPlan(workoutPlanId, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.workoutPlans.Remove(workoutPlan), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }
        [Fact]
        public async Task DeleteWorkoutPlan_ShouldThrowException_WhenUserHasNoAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var workoutPlanId = Guid.NewGuid();

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync((WorkoutPlan)null);

            // Act
            Func<Task> act = async () => await _service.DeleteWorkoutPlan(workoutPlanId, user);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _unitOfWorkMock.Verify(u => u.workoutPlans.Remove(It.IsAny<WorkoutPlan>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Never);
        }


        private ClaimsPrincipal GetTestUser(Guid userId)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        }));
        }
    }
}