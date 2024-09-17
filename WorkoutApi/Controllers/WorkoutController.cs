using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class WorkoutController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly ResponseDto _response;
        public WorkoutController(IMapper mapper, AppDbContext db)
        {
            _db = db;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Workout> data = _db.workouts.ToList();
                _response.Result = _mapper.Map<IEnumerable<WorkoutDto>>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("{id:int}")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                Workout data = await _db.workouts.FirstAsync(workout => workout.Id == id);
                _response.Result = _mapper.Map<WorkoutDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        

        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] WorkoutDto model)
        {
            try
            {
                Workout data = _mapper.Map<Workout>(model);
                await _db.workouts.AddAsync(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<WorkoutDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] WorkoutDto model)
        {
            try
            {
                Workout data = _mapper.Map<Workout>(model);
                _db.workouts.Update(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<WorkoutDto>(data);
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
                Workout data = _db.workouts.First(workout => workout.Id == id);
                _db.workouts.Remove(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<WorkoutDto>(data);
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
