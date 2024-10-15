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
    public class ReportController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutPlanService;
        private readonly ResponseDto _response;
        public ReportController(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
            _response = new();

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<WorkoutPlanResponseDto> data = await _workoutPlanService.GenerateReport(User);
            _response.result = data;
            return Ok(_response);
        }
    }
}
