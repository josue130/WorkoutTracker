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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseDto _response;
        public WorkoutController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{id:int}")]
        public async Task<ResponseDto> Get(Guid id)
        {
            try
            {
                Workout data = await _unitOfWork.workouts.Get(workout => workout.Id == id);
                _response.Result = _mapper.Map<WorkoutDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        
        [HttpGet("Report/{UserId:int}")]
        public async Task<ResponseDto> GetReport(Guid UserId)
        {
            try
            {
                List<ReportHeaderDto> report = await _unitOfWork.workouts.GenerateReport(UserId);
                _response.Result = report;

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
                await _unitOfWork.workouts.Add(data);
                await _unitOfWork.Save();
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
                _unitOfWork.workouts.Update(data);
                await  _unitOfWork.Save();
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
        public async Task<ResponseDto> Delete(Guid id)
        {
            try
            {
                Workout data = await _unitOfWork.workouts.Get(workout => workout.Id == id);
                _unitOfWork.workouts.Remove(data);
                await _unitOfWork.Save();
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
