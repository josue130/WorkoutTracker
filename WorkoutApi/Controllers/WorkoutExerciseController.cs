using AutoMapper;
using Azure;
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
    public class WorkoutExerciseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly ResponseDto _response;
        public WorkoutExerciseController(IMapper mapper, AppDbContext db)
        {
            _mapper = mapper;
            _db = db;
            _response = new();
        }

        [HttpGet("{workoutId:int}")]
        public async Task<ResponseDto> Get(int workoutId)
        {
            try
            {
                WorkoutExercise data = await _db.workoutExercises.FirstAsync(we => we.WorkoutId == workoutId);
                _response.Result = _mapper.Map<WorkoutExerciseDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] WorkoutExerciseDto model)
        {
            try
            {
                WorkoutExercise data = _mapper.Map<WorkoutExercise>(model);
                await _db.workoutExercises.AddAsync(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<WorkoutExerciseDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

      
        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] WorkoutExerciseDto model)
        {
            try
            {
                WorkoutExercise data = _mapper.Map<WorkoutExercise>(model);
                _db.workoutExercises.Update(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<WorkoutExerciseDto>(data);
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
                WorkoutExercise data = await _db.workoutExercises.FirstAsync(we => we.Id == id);
                _db.workoutExercises.Remove(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<WorkoutExerciseDto>(data);
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
