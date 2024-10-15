using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout.Application.Common.Dto;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleWorkoutService _scheduleWorkoutService;
        private readonly ResponseDto _response;
        public ScheduleController(IScheduleWorkoutService scheduleWorkoutService)
        {
            _scheduleWorkoutService = scheduleWorkoutService;
            _response = new();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<ScheduleWorkoutDto> data = await _scheduleWorkoutService.GetScheduleWorkoutsByUserId(User);
            _response.result = data;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScheduleWorkoutDto scheduleWorkoutDto)
        {
            await _scheduleWorkoutService.SetWorkoutSchedule(scheduleWorkoutDto, User);
            return Ok(_response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ScheduleWorkoutDto scheduleWorkoutDto)
        {
            await _scheduleWorkoutService.UpdateScheduledWorkout(scheduleWorkoutDto, User);
            return Ok(_response);
        }

        [HttpDelete("{schedule_workout_id:guid}")]
        public async Task<IActionResult> Delete(Guid schedule_workout_id)
        {
            await _scheduleWorkoutService.DeleteScheduledWorkout(schedule_workout_id, User);
            return Ok(_response);
        }
    }
}
