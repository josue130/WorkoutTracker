using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutApi.Data;
using WorkoutApi.Models;
using WorkoutApi.Models.Dto;
using WorkoutApi.Repository.IRepository;
using WorkoutApi.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutApi.Controllers
{
    [Route("api/schedule-workouts")]
    [ApiController]
    [Authorize]
    public class ScheduleWorkoutController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleWorkoutController(IMapper mapper, AppDbContext db, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _response = new();
            _unitOfWork = unitOfWork;       
        }


        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                var userId = JwtHelper.GetUserId(User);
                IEnumerable<ScheduleWorkout> data = await _unitOfWork.scheduleWorkouts.GetScheduleWorkouts(userId);
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
                await _unitOfWork.scheduleWorkouts.Add(data);
                await _unitOfWork.Save();
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
                _unitOfWork.scheduleWorkouts.Update(data);
                await _unitOfWork.Save();
                _response.Result = _mapper.Map<ScheduleWorkoutDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("{schedule_workout_id:Guid}")]
        public async Task<ResponseDto> Delete(Guid schedule_workout_id)
        {
            try
            {
                ScheduleWorkout data = await _unitOfWork.scheduleWorkouts.Get(sw => sw.Id == schedule_workout_id);
                _unitOfWork.scheduleWorkouts.Remove(data);
                await _unitOfWork.Save();
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
