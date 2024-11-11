using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout.Application.Common.Dto;
using Workout.Application.Services.Implementation;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutAPI.Controllers
{
    [Route("api/workout-exercises")]
    [ApiController]
    [Authorize]
    public class WorkoutExerciseController : ControllerBase
    {
        private readonly IWorkoutExerciseService _workoutExerciseService;
        private readonly ResponseDto _response;
        public WorkoutExerciseController(IWorkoutExerciseService workoutExerciseService)
        {
            _workoutExerciseService = workoutExerciseService;
            _response = new();
        }
        [HttpGet("{workout_plan_id:Guid}")]
        public async Task<IActionResult> Get(Guid workout_plan_id)
        {
            var response = await _workoutExerciseService.GetWorkoutExerciseById(workout_plan_id, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkoutExerciseDto model)
        {
            var response = await _workoutExerciseService.AddWorkoutExercise(model,User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WorkoutExerciseDto model)
        {
            var response = await _workoutExerciseService.UpdateWorkoutExercise(model, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }

        [HttpDelete("{workout_exercise_id:Guid}")]
        public async Task<IActionResult> Delete(Guid workout_exercise_id)
        {
            var response = await _workoutExerciseService.DeleteWorkoutExercise(workout_exercise_id, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }
    }
}
