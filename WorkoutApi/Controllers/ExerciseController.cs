using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutApi.Data;
using WorkoutApi.Models;
using WorkoutApi.Models.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Exercise> data = _db.exercises.ToList();
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
        public ResponseDto Get(int id)
        {
            try
            {
                Exercise data = _db.exercises.First(exercise => exercise.Id == id);
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
        public ResponseDto Post([FromBody] ExerciseDto model)
        {
            try
            {
                Exercise data = _mapper.Map<Exercise>(model);
                _db.exercises.Add(data);
                _db.SaveChanges();
                _response.Result = _mapper.Map<ExerciseDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut("{id}")]
        public ResponseDto Put([FromBody] ExerciseDto model)
        {
            try
            {
                Exercise data = _mapper.Map<Exercise>(model);
                _db.exercises.Update(data);
                _db.SaveChanges();
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
        public ResponseDto Delete(int id)
        {
            try
            {
                Exercise data = _db.exercises.First(exercise => exercise.Id == id);
                _db.exercises.Remove(data);
                _db.SaveChanges();
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
