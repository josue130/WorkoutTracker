using AutoMapper;
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
    public class ScheduleWorkoutServiceTests
    {
        /*
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ScheduleWorkoutService _service;

        public ScheduleWorkoutServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new ScheduleWorkoutService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task SetWorkoutSchedule_ShouldSucceed_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var scheduleDto = new ScheduleWorkoutDto { WorkoutId = Guid.NewGuid(), ScheduledDate = DateTime.UtcNow };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync(new WorkoutPlan { Id = scheduleDto.WorkoutId, UserId = userId });

            _unitOfWorkMock.Setup(u => u.scheduleWorkouts.Add(It.IsAny<ScheduleWorkout>()));
            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);

            // Act
            await _service.SetWorkoutSchedule(scheduleDto, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.scheduleWorkouts.Add(It.IsAny<ScheduleWorkout>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }
        [Fact]
        public async Task DeleteScheduledWorkout_ShouldSucceed_WhenUserHasAccesse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var scheduleId = Guid.NewGuid();

            _unitOfWorkMock.Setup(u => u.scheduleWorkouts.Get(It.IsAny<Expression<Func<ScheduleWorkout, bool>>>()))
                           .ReturnsAsync(new ScheduleWorkout { Id = scheduleId, Workout = new WorkoutPlan { UserId = userId } });

            _unitOfWorkMock.Setup(u => u.scheduleWorkouts.Remove(It.IsAny<ScheduleWorkout>()));
            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteScheduledWorkout(scheduleId, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.scheduleWorkouts.Remove(It.IsAny<ScheduleWorkout>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }
        [Fact]
        public async Task GetScheduleWorkoutsByUserId_ShouldReturnSchedules_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);

            var schedules = new List<ScheduleWorkout>
            {
                new ScheduleWorkout { Id = Guid.NewGuid() },
                new ScheduleWorkout { Id = Guid.NewGuid() }
            };

            _unitOfWorkMock.Setup(u => u.scheduleWorkouts.GetScheduleWorkouts(userId))
                           .ReturnsAsync(schedules);

            _mapperMock.Setup(m => m.Map<IEnumerable<ScheduleWorkoutDto>>(It.IsAny<IEnumerable<ScheduleWorkout>>()))
                       .Returns(new List<ScheduleWorkoutDto>());

            // Act
            var result = await _service.GetScheduleWorkoutsByUserId(user);

            // Assert
            result.Should().NotBeNull();
            _unitOfWorkMock.Verify(u => u.scheduleWorkouts.GetScheduleWorkouts(userId), Times.Once);
        }

        [Fact]
        public async Task UpdateScheduledWorkout_ShouldSucceed_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = GetTestUser(userId);
            var scheduleDto = new ScheduleWorkoutDto { Id = Guid.NewGuid(), WorkoutId = Guid.NewGuid(), ScheduledDate = DateTime.UtcNow };

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                           .ReturnsAsync(new WorkoutPlan { Id = scheduleDto.WorkoutId, UserId = userId });

            _unitOfWorkMock.Setup(u => u.scheduleWorkouts.Update(It.IsAny<ScheduleWorkout>()));
            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateScheduledWorkout(scheduleDto, user);

            // Assert
            _unitOfWorkMock.Verify(u => u.scheduleWorkouts.Update(It.IsAny<ScheduleWorkout>()), Times.Once);
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
