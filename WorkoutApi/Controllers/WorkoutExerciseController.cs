﻿using AutoMapper;
using Azure;
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
    [Route("api/workout-exercises")]
    [ApiController]
    [Authorize]
    public class WorkoutExerciseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseDto _response;
        public WorkoutExerciseController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _response = new();
        }

        [HttpGet("{workout_id:Guid}")]
        public async Task<ResponseDto> Get(Guid workout_id)
        {
            try
            {
                IEnumerable<WorkoutExercise> data = await _unitOfWork.workoutExercises.GetWorkoutExercises(workout_id); 
                 
                _response.Result = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(data);
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
                await _unitOfWork.workoutExercises.Add(data);
                await _unitOfWork.Save();
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
                 _unitOfWork.workoutExercises.Update(data);
                await _unitOfWork.Save();
                _response.Result = _mapper.Map<WorkoutExerciseDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete("{workout_exercise_id:Guid}")]
        public async Task<ResponseDto> Delete(Guid workout_exercise_id)
        {
            try
            {
                WorkoutExercise data = await _unitOfWork.workoutExercises.Get(we => we.Id == workout_exercise_id);
                _unitOfWork.workoutExercises.Remove(data);
                await _unitOfWork.Save();
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
