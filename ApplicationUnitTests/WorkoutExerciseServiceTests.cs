﻿using AutoMapper;
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
    public class WorkoutExerciseServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private WorkoutExerciseService _workoutExerciseService;

        
        public WorkoutExerciseServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _workoutExerciseService = new WorkoutExerciseService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        private ClaimsPrincipal CreateUserClaimsPrincipal(Guid userId)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        }));
        }

        [Fact]
        public async Task AddWorkoutExercise_WithInvalidSets_ShouldReturnFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new WorkoutExerciseDto { WorkoutId = Guid.NewGuid(), Sets = 0, Repetitions = 10, Weight = 50 };
            var user = CreateUserClaimsPrincipal(userId);


            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                .ReturnsAsync(new WorkoutPlan(model.WorkoutId, "", "", userId));

            // Act
            var result = await _workoutExerciseService.AddWorkoutExercise(model, user);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(WorkoutExerciseError.InvalidSets);
        }

        [Fact]
        public async Task AddWorkoutExercise_WithValidInputs_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new WorkoutExerciseDto { ExerciseId = 2, WorkoutId = Guid.NewGuid(), Sets = 3, Repetitions = 10, Weight = 50 };
            var user = CreateUserClaimsPrincipal(userId);

            _unitOfWorkMock.Setup(u => u.workoutPlans.Get(It.IsAny<Expression<Func<WorkoutPlan, bool>>>()))
                .ReturnsAsync(new WorkoutPlan ( model.WorkoutId,"","",  userId ));
            _unitOfWorkMock.Setup(u => u.workoutExercises.Add(It.IsAny<WorkoutExercise>()))
                           .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);
            // Act
            var result = await _workoutExerciseService.AddWorkoutExercise(model, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be("Exercise added successfully.");
        }

        [Fact]
        public async Task DeleteWorkoutExercise_WithUnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var workoutExerciseId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(Guid.NewGuid());

            _unitOfWorkMock.Setup(u => u.workoutExercises.Get(It.IsAny<Expression<Func<WorkoutExercise, bool>>>()))
                .ReturnsAsync((WorkoutExercise)null);

            // Act
            Func<Task> act = async () => await _workoutExerciseService.DeleteWorkoutExercise(workoutExerciseId, user);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Fact]
        public async Task DeleteWorkoutExercise_WithAuthorizedUser_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var workoutExerciseId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            var workoutExercise = WorkoutExercise.Create(2, Guid.NewGuid(), 3, 10, 50);

            _unitOfWorkMock.Setup(u => u.workoutExercises.Get(It.IsAny<Expression<Func<WorkoutExercise, bool>>>()))
                .ReturnsAsync(workoutExercise);

            // Act
            var result = await _workoutExerciseService.DeleteWorkoutExercise(workoutExerciseId, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be("Workout exercise deleted successfully");
        }

        [Fact]
        public async Task GetWorkoutExerciseById_WithValidUser_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var workoutId = Guid.NewGuid();
            var user = CreateUserClaimsPrincipal(userId);
            var workoutExercises = new List<WorkoutExercise>
        {
            WorkoutExercise.Create(2, workoutId, 3, 10, 50)
        };

            _unitOfWorkMock.Setup(u => u.workoutExercises.GetWorkoutExercises(workoutId, userId))
                .ReturnsAsync(workoutExercises);

            var workoutExerciseDtos = new List<WorkoutExerciseDto> { new WorkoutExerciseDto() };
            _mapperMock.Setup(m => m.Map<IEnumerable<WorkoutExerciseDto>>(workoutExercises)).Returns(workoutExerciseDtos);

            // Act
            var result = await _workoutExerciseService.GetWorkoutExerciseById(workoutId, user);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be(workoutExerciseDtos);
        }
    }
}
