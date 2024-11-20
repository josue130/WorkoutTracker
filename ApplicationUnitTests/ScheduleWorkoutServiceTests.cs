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
using Workout.Application.Common.Result;
using Workout.Application.Errors;
using Workout.Application.Services.Implementation;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

namespace ApplicationUnitTests
{
    public class ScheduleWorkoutServiceTests
    {
        
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private Mock<IAuthService> _authServiceMock;
        private Mock<IWorkoutPlanService> _workoutPlanServiceMock;
        private readonly ScheduleWorkoutService _scheduleWorkoutService;

        public ScheduleWorkoutServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _authServiceMock = new Mock<IAuthService>();
            _workoutPlanServiceMock = new Mock<IWorkoutPlanService>();
            _scheduleWorkoutService = new ScheduleWorkoutService(_unitOfWorkMock.Object, _mapperMock.Object,
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
        public async Task SetWorkoutSchedule_WithPastDate_ShouldReturnFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new ScheduleWorkoutDto { WorkoutId = Guid.NewGuid(), ScheduledDate = DateTime.Today.AddDays(-1) };
            var user = CreateUserClaimsPrincipal(userId);
            var workoutPlan = new WorkoutPlan(model.WorkoutId, "", "", userId);
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));


            _workoutPlanServiceMock.Setup(u => u.CheckAccess(model.WorkoutId, userId))
                .ReturnsAsync(Result<WorkoutPlan>.Success(workoutPlan));
            // Act
            var result = await _scheduleWorkoutService.SetWorkoutSchedule(model, user);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ScheduleWorkoutError.InvalidDate);
        }

        [Fact]
        public async Task SetWorkoutSchedule_WithValidDate_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new ScheduleWorkoutDto { WorkoutId = Guid.NewGuid(), ScheduledDate = DateTime.Today.AddDays(1) };
            var user = CreateUserClaimsPrincipal(userId);
            var workoutPlan = new WorkoutPlan(model.WorkoutId, "", "", userId);
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));
            _workoutPlanServiceMock.Setup(u => u.CheckAccess(model.WorkoutId, userId))
                .ReturnsAsync(Result<WorkoutPlan>.Success(workoutPlan));
       

            _unitOfWorkMock.Setup(u => u.scheduleWorkouts.Add(It.IsAny<ScheduleWorkout>()))
                           .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);
            // Act
            var result = await _scheduleWorkoutService.SetWorkoutSchedule(model, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be("Workout scheduled successfully.");
        }

        [Fact]
        public async Task DeleteScheduledWorkout_WithUnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var scheduleWorkoutId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));
            _unitOfWorkMock.Setup(u => u.scheduleWorkouts.Get(It.IsAny<Expression<Func<ScheduleWorkout, bool>>>()))
                .ReturnsAsync((ScheduleWorkout)null);

            // Act
            var result =  await _scheduleWorkoutService.DeleteScheduledWorkout(scheduleWorkoutId, user);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ScheduleWorkoutError.ScheduleNotFound);
        }

        [Fact]
        public async Task DeleteScheduledWorkout_WithAuthorizedUser_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var scheduleWorkoutId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            var scheduleWorkout = ScheduleWorkout.Create(DateTime.Today.AddDays(1), Guid.NewGuid());

            _unitOfWorkMock.Setup(u => u.scheduleWorkouts.Get(It.IsAny<Expression<Func<ScheduleWorkout, bool>>>()))
                .ReturnsAsync(scheduleWorkout);
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));
            // Act
            var result = await _scheduleWorkoutService.DeleteScheduledWorkout(scheduleWorkoutId, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be("Scheduled workout deleted successfully.");
        }

        [Fact]
        public async Task GetScheduleWorkoutsByUserId_WithValidUser_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            var scheduleWorkouts = new List<ScheduleWorkout>
        {
            ScheduleWorkout.Create(DateTime.Today.AddDays(1), Guid.NewGuid())
        };
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));
            _unitOfWorkMock.Setup(u => u.scheduleWorkouts.GetScheduleWorkouts(userId))
                .ReturnsAsync(scheduleWorkouts);

            var scheduleWorkoutsDto = new List<ScheduleWorkoutDto> { new ScheduleWorkoutDto() };
            _mapperMock.Setup(m => m.Map<IEnumerable<ScheduleWorkoutDto>>(scheduleWorkouts)).Returns(scheduleWorkoutsDto);

            // Act
            var result = await _scheduleWorkoutService.GetScheduleWorkoutsByUserId(user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().BeEquivalentTo(scheduleWorkoutsDto);
        }

        [Fact]
        public async Task UpdateScheduledWorkout_WithPastDate_ShouldReturnFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new ScheduleWorkoutDto { Id = Guid.NewGuid(), WorkoutId = Guid.NewGuid(), ScheduledDate = DateTime.Today.AddDays(-1) };
            var user = CreateUserClaimsPrincipal(userId);
            var scheduleWorkout = new ScheduleWorkout(Guid.NewGuid(),DateTime.Today.AddDays(1),model.WorkoutId);

            _unitOfWorkMock.Setup(u => u.scheduleWorkouts.Get(It.IsAny<Expression<Func<ScheduleWorkout, bool>>>()))
                .ReturnsAsync(scheduleWorkout);
            _authServiceMock.Setup(s => s.GetUserId(user))
                .Returns(Result<Guid>.Success(userId));


            // Act
            var result = await _scheduleWorkoutService.UpdateScheduledWorkout(model, user);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ScheduleWorkoutError.InvalidDate);
        }
    }
}
