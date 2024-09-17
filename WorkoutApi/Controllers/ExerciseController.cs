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
    public class ExerciseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly ResponseDto _response;
        public ExerciseController(IMapper mapper, AppDbContext db)
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
                IEnumerable<Exercise> data = await _db.exercises.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<ExerciseDto>>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("{id}")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                Exercise data = await _db.exercises.FirstAsync(exercise => exercise.Id == id);
                _response.Result = _mapper.Map<ExerciseDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] ExerciseDto model)
        {
            try
            {
                Exercise data = _mapper.Map<Exercise>(model);
                await _db.exercises.AddAsync(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<ExerciseDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] ExerciseDto model)
        {
            try
            {
                Exercise data = _mapper.Map<Exercise>(model);
                _db.exercises.Update(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<ExerciseDto>(data);
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
                Exercise data = await _db.exercises.FirstAsync(exercise => exercise.Id == id);
                _db.exercises.Remove(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<ExerciseDto>(data);
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
