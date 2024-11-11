using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout.Application.Common.Dto;
using Workout.Application.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutAPI.Controllers
{
    [Route("api/workouts")]
    [ApiController]
    [Authorize]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutPlanService;
        private readonly ResponseDto _response;
        public WorkoutPlanController(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
            _response = new();
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _workoutPlanService.GetWorkoutsbyUserId(User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }


        [HttpGet("{workout_plan_id:Guid}")]
        public async Task<IActionResult> Get(Guid workout_plan_id)
        {
            var response=  await _workoutPlanService.GetByWorkouPlanId(workout_plan_id,User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }

     
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkoutPlanDto workoutPlanDto)
        {
            var response = await _workoutPlanService.AddWorkoutPlan(workoutPlanDto, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }

     
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WorkoutPlanDto workoutPlanDto)
        {
            var response = await _workoutPlanService.UpdateWorkoutPlan(workoutPlanDto, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }

        [HttpDelete("{workout_plan_id:guid}")]
        public async Task<IActionResult> Delete(Guid workout_plan_id)
        {
            var response = await _workoutPlanService.DeleteWorkoutPlan(workout_plan_id, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }
    }
}
