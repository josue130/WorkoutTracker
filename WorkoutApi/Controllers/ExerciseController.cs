using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutApi.Data;
using WorkoutApi.Models;
using WorkoutApi.Models.Dto;
using WorkoutApi.Repository.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutApi.Controllers
{
    [Route("api/exercises")]
    [ApiController]
    [Authorize]
    public class ExerciseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseDto _response;
        public ExerciseController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _response = new();
      
        }
        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Exercise> data = await _unitOfWork.exercises.GetAll();
                _response.Result = _mapper.Map<IEnumerable<ExerciseDto>>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("{exercise_id:int}")]
        public async Task<ResponseDto> Get(int exercise_id)
        {
            try
            {
                Exercise data = await _unitOfWork.exercises.Get(exercise => exercise.Id == exercise_id); 
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
                await _unitOfWork.exercises.Add(data);
                await _unitOfWork.Save();
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
                _unitOfWork.exercises.Update(data);
                await _unitOfWork.Save();
                _response.Result = _mapper.Map<ExerciseDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("{exercise_id:int}")]
        public async Task<ResponseDto> Delete(int exercise_id)
        {
            try
            {
                Exercise data = await _unitOfWork.exercises.Get(exercise => exercise.Id == exercise_id);
                _unitOfWork.exercises.Remove(data);
                await _unitOfWork.Save();
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
