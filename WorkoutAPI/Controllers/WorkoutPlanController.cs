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
            IEnumerable<WorkoutPlanResponseDto> data = await _workoutPlanService.GetWorkoutsbyUserId(User);
            _response.result = data;
            return Ok(_response);
        }


        [HttpGet("{workout_plan_id:Guid}")]
        public async Task<IActionResult> Get(Guid workout_plan_id)
        {
            WorkoutPlanDto data =  await _workoutPlanService.GetByWorkouPlanId(workout_plan_id,User);
            _response.result = data;
            return Ok(_response);
        }

     
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkoutPlanDto workoutPlanDto)
        {
            await _workoutPlanService.AddWorkoutPlan(workoutPlanDto, User);
            return Ok(_response);
        }

     
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WorkoutPlanDto workoutPlanDto)
        {
            await _workoutPlanService.UpdateWorkoutPlan(workoutPlanDto, User);
            return Ok(_response);
        }

        [HttpDelete("{workout_plan_id:guid}")]
        public async Task<IActionResult> Delete(Guid workout_plan_id)
        {
            await _workoutPlanService.DeleteWorkoutPlan(workout_plan_id, User);
            return Ok(_response);
        }
    }
}
