using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout.Application.Common.Dto;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutAPI.Controllers
{
    [Route("api/Workout-schedules")]
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
            var response = await _scheduleWorkoutService.GetScheduleWorkoutsByUserId(User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScheduleWorkoutDto scheduleWorkoutDto)
        {
            var response = await _scheduleWorkoutService.SetWorkoutSchedule(scheduleWorkoutDto, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ScheduleWorkoutDto scheduleWorkoutDto)
        {
            var response =  await _scheduleWorkoutService.UpdateScheduledWorkout(scheduleWorkoutDto, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }

        [HttpDelete("{schedule_workout_id:guid}")]
        public async Task<IActionResult> Delete(Guid schedule_workout_id)
        {
            var response =  await _scheduleWorkoutService.DeleteScheduledWorkout(schedule_workout_id, User);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(response.Values);
        }
    }
}
