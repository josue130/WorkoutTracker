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
    public class WorkoutCommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly ResponseDto _response;
        public WorkoutCommentsController(IMapper mapper, AppDbContext db)
        {
            _mapper = mapper;
            _db = db;
            _response = new();
        }

        [HttpGet("WorkoutId/{WorkoutId}")]
        public async Task<ResponseDto> Get(int WorkoutId)
        {
            try
            {
                IEnumerable<WorkoutComments> data = await _db.workoutComments.Where(workout => workout.WorkoutId == WorkoutId).ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<WorkoutCommentsDto>>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] WorkoutCommentsDto model)
        {
            try
            {
                WorkoutComments data = _mapper.Map<WorkoutComments>(model);
                await _db.workoutComments.AddAsync(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<WorkoutCommentsDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] WorkoutCommentsDto model)
        {
            try
            {
                WorkoutComments data = _mapper.Map<WorkoutComments>(model);
                _db.workoutComments.Update(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<WorkoutCommentsDto>(data);
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
                WorkoutComments data = _db.workoutComments.First(wc => wc.Id == id);
                _db.workoutComments.Remove(data);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<WorkoutCommentsDto>(data);
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
