using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout.Application.Common.Dto;
using Workout.Application.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutAPI.Controllers
{
    [Route("api/workout-comments")]
    [ApiController]
    [Authorize]
    public class WorkoutCommentsController : ControllerBase
    {
        private readonly IWorkoutCommentsService _workoutCommentsService;
        private readonly ResponseDto _response;
        public WorkoutCommentsController(IWorkoutCommentsService workoutCommentsService)
        {
            _workoutCommentsService = workoutCommentsService;  
            _response = new();
        }

        [HttpGet("{workout_id:guid}")]
        public async Task<IActionResult> Get(Guid workout_id)
        {
            var response = await _workoutCommentsService.GetWorkoutCommentsByWorkoutId(workout_id, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkoutCommentsDto workoutCommentsDto)
        {
            var response = await _workoutCommentsService.AddWorkoutComment(workoutCommentsDto, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WorkoutCommentsDto workoutCommentsDto)
        {
            var response = await _workoutCommentsService.UpdateWorkoutComment(workoutCommentsDto, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }


        [HttpDelete("{workout_comment_id:guid}")]
        public async Task<IActionResult> Delete(Guid workout_comment_id)
        {
            var response = await _workoutCommentsService.DeleteWorkoutComment(workout_comment_id, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }
    }
}
