using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutApi.Data;
using WorkoutApi.Models;
using WorkoutApi.Models.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleWorkoutController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly ResponseDto _response;

        public ScheduleWorkoutController(IMapper mapper, AppDbContext db)
        {
            _mapper = mapper;
            _db = db;
            _response = new();
        }


        [HttpGet("{UserId}")]
        public async Task<ResponseDto> Get(int UserId)
        {
            try
            {
                IEnumerable<ScheduleWorkout> data = await _db.scheduleWorkouts
                     .Include(sw => sw.Workout)
                     .Where(sw => sw.ScheduledDate >= DateTime.Now && sw.Workout.UserId == UserId)
                     .OrderBy(sw => sw.ScheduledDate)
                     .ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<ScheduleWorkoutDto>>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] ScheduleWorkoutDto model)
        {
            try
            {
                ScheduleWorkout data = _mapper.Map<ScheduleWorkout>(model);
                await _db.scheduleWorkouts.AddAsync(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<ScheduleWorkoutDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] ScheduleWorkoutDto model)
        {
            try
            {
                ScheduleWorkout data = _mapper.Map<ScheduleWorkout>(model);
                _db.scheduleWorkouts.Update(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<ScheduleWorkoutDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                ScheduleWorkout data = _db.scheduleWorkouts.First(sw => sw.Id == id);
                _db.scheduleWorkouts.Remove(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<ScheduleWorkoutDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
