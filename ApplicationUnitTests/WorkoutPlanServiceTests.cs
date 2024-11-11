using AutoMapper;
using System.Linq.Expressions;
using System.Security.Claims;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Errors;
using Workout.Application.Services.Implementation;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

namespace ApplicationUnitTests
{
    public class WorkoutPlanServiceTests
    {
        
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly WorkoutPlanService _workoutPlanService;

        public WorkoutPlanServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _workoutPlanService = new WorkoutPlanService(_unitOfWorkMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task AddWorkoutPlan_WithInvalidInputs_ShouldReturnFailureResult()
        {
            // Arrange
            var model = new WorkoutPlanDto { Name = "", Description = "" };
            var user = CreateUserClaimsPrincipal(Guid.NewGuid());

            // Act
            var result = await _workoutPlanService.AddWorkoutPlan(model, user);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(WorkoutPlanError.InvalidInputs);
        }

        [Fact]
        public async Task AddWorkoutPlan_WithExistingName_ShouldReturnFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new WorkoutPlanDto { Name = "PlanName", Description = "Description" };
            var existingWorkout = new WorkoutPlan (  Guid.NewGuid(),"PlanName", "Description", userId );
            var user = CreateUserClaimsPrincipal(userId);

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                .ReturnsAsync(existingWorkout);

            // Act
            var result = await _workoutPlanService.AddWorkoutPlan(model, user);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(WorkoutPlanError.WorkoutPlanNameAlreadyExists);
        }

        [Fact]
        public async Task AddWorkoutPlan_WithValidInputs_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new WorkoutPlanDto { Name = "New Plan", Description = "Plan Description" };
            var user = CreateUserClaimsPrincipal(userId);

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                .ReturnsAsync((WorkoutPlan)null);

            // Act
            var result = await _workoutPlanService.AddWorkoutPlan(model, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be("WorkoutPlan added successfully.");
        }

        [Fact]
        public async Task DeleteWorkoutPlan_WithUnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var workoutPlanId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(Guid.NewGuid());

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                .ReturnsAsync((WorkoutPlan)null);

            // Act
            Func<Task> act = async () => await _workoutPlanService.DeleteWorkoutPlan(workoutPlanId, user);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Fact]
        public async Task DeleteWorkoutPlan_WithAuthorizedUser_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var workoutPlanId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            var workoutPlan = WorkoutPlan.Create("Plan", "Description", userId);

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                .ReturnsAsync(workoutPlan);

            // Act
            var result = await _workoutPlanService.DeleteWorkoutPlan(workoutPlanId, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be("WorkoutPlan deleted successfully");
        }

        [Fact]
        public async Task GenerateReport_WithValidUser_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            var workoutPlans = new List<WorkoutPlanResponseDto> { new WorkoutPlanResponseDto() };

            _unitOfWorkMock.Setup(u => u.workoutPlans.GenerateReport(userId))
                .ReturnsAsync(workoutPlans);

            // Act
            var result = await _workoutPlanService.GenerateReport(user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be(workoutPlans);
        }

        [Fact]
        public async Task GetByWorkouPlanId_WithValidIdAndUser_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var workoutPlanId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            var workoutPlan = WorkoutPlan.Create("Plan", "Description", userId);

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                .ReturnsAsync(workoutPlan);

            var workoutPlanDto = new WorkoutPlanDto();
            _mapperMock.Setup(m => m.Map<WorkoutPlanDto>(workoutPlan)).Returns(workoutPlanDto);

            // Act
            var result = await _workoutPlanService.GetByWorkouPlanId(workoutPlanId, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be(workoutPlanDto);
        }

        

        private ClaimsPrincipal CreateUserClaimsPrincipal(Guid userId)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        }));
        }

        
    }
}