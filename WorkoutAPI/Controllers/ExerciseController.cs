using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout.Application.Common.Dto;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutAPI.Controllers
{
    [Route("api/exercises")]
    [ApiController]
    [Authorize]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        private readonly ResponseDto _response;
        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
            _response = new();
            
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Exercise> data = await _exerciseService.GetAllExercises();
            _response.result = data;
            return Ok(_response);
        }

       
    }
}
