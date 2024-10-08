﻿using AutoMapper;
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
    [Route("api/workout-comments")]
    [ApiController]
    [Authorize]
    public class WorkoutCommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        private readonly IUnitOfWork _unitOfWork;
        public WorkoutCommentsController(IMapper mapper,IUnitOfWork unitOfWork)
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
                IEnumerable<WorkoutComments> data = await _unitOfWork.workoutsComments.GetWorkoutComments(workout_id);
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
                await _unitOfWork.workoutsComments.Add(data);
                await _unitOfWork.Save();
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
                _unitOfWork.workoutsComments.Update(data);
                await _unitOfWork.Save();
                _response.Result = _mapper.Map<WorkoutCommentsDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("{workout_comment_id:Guid}")]
        public async Task<ResponseDto> Delete(Guid workout_comment_id)
        {
            try
            {
                WorkoutComments data = await _unitOfWork.workoutsComments.Get(wc => wc.Id == workout_comment_id);
                _unitOfWork.workoutsComments.Remove(data);
                await _unitOfWork.Save();
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
