using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout.Application.Common.Dto;
using Workout.Application.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutAPI.Controllers
{
    [Route("api/[controller]")]
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
            IEnumerable<WorkoutCommentsDto> data = await _workoutCommentsService.GetWorkoutCommentsByWorkoutId(workout_id, User);
            _response.result = data;
            return Ok(_response);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkoutCommentsDto workoutCommentsDto)
        {
            await _workoutCommentsService.AddWorkoutComment(workoutCommentsDto, User);
            return Ok(_response);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WorkoutCommentsDto workoutCommentsDto)
        {
            await _workoutCommentsService.UpdateWorkoutComment(workoutCommentsDto, User);
            return Ok(_response);
        }


        [HttpDelete("{workout_comment_id:guid}")]
        public async Task<IActionResult> Delete(Guid workout_comment_id)
        {
            await _workoutCommentsService.DeleteWorkoutComment(workout_comment_id, User);
            return Ok(_response);
        }
    }
}
