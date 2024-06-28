using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using TNSDC_FinishingSchool.Domain.Contracts;
using TNSDC_FinishingSchool.Bussiness.Common;
using TNSDC_FinishingSchool.Bussiness.ApplicationConstants;
using TNSDC_FinishingSchool.Domain.Model;
using System.Collections.Generic;

namespace TNSDC_FinishingSchool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TrainerController : ControllerBase
    {
        private readonly ITrainerRepository _trainerRepository;
        protected APIResponse _response;

        //List<Trainer> trainer = new List<Trainer>
        //{
        //    //new Trainer
        //    //{
        //    //    Id = 1,
        //    //    FirstName = "John",
        //    //    LastName = "Doe",
        //    //    Email = "john.doe@example.com",
        //    //    PhoneNumber = "+1234567890",
        //    //    Gender = "Male",
        //    //    DateOfBirth = new DateTime(1990, 5, 15),
        //    //    AadharNumber = "123456789012",
        //    //    PanCardNumber = "ABCDE1234F",
        //    //    Qualification = "Bachelor's",
        //    //    Specialization = "Computer Science"
        //    //},
        //    //new Trainer
        //    //{
        //    //    Id = 2,
        //    //    FirstName = "Jane",
        //    //    LastName = "Smith",
        //    //    Email = "jane.smith@example.com",
        //    //    PhoneNumber = "+1987654321",
        //    //    Gender = "Female",
        //    //    DateOfBirth = new DateTime(1985, 10, 25),
        //    //    AadharNumber = "987654321098",
        //    //    PanCardNumber = "PQRST5678G",
        //    //    Qualification = "Master's",
        //    //    Specialization = "Electrical Engineering"
        //    //}

        //};


        public TrainerController(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> Get()
        {
            try
            {
                var trainers = await _trainerRepository.GetAllAsync();
              
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = trainers;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                var trainer = await _trainerRepository.GetByIdAsync(p => p.Id == id);

                if (trainer == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessage.RecordNotFound;
                    return Ok(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = trainer;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok(_response);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[HttpPost]
        //public async Task<ActionResult<APIResponse>> Create([FromBody] Trainer trainer)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.DisplayMessage = CommonMessage.CreateOperationFailed;
        //            _response.AddError(ModelState.ToString());
        //            return Ok(_response);
        //        }

        //        var entity = await _trainerRepository.CreateAsync(trainer);

        //        _response.StatusCode = HttpStatusCode.Created;
        //        _response.IsSuccess = true;
        //        _response.DisplayMessage = CommonMessage.CreateOperationSuccess;
        //        _response.Result = entity;
        //    }
        //    catch (Exception)
        //    {
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.DisplayMessage = CommonMessage.CreateOperationFailed;
        //        _response.AddError(CommonMessage.SystemError);
        //    }

        //    return Ok(_response);
        //}


        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] Trainer trainer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return Ok(_response);
                }

                var entity = await _trainerRepository.CreateAsync(trainer);

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.CreateOperationSuccess;
                _response.Result = entity;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok(_response);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Update([FromBody] Trainer trainer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessage.UpdateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return Ok(_response);
                }

                var trainerResult = await _trainerRepository.GetByIdAsync(p => p.Id == trainer.Id);

                if (trainer == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessage.UpdateOperationFailed;
                    return Ok(_response);
                }

                await _trainerRepository.UpdateAsync(trainer);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.UpdateOperationSuccess;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommonMessage.UpdateOperationFailed;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessage.DeleteOperationFailed;
                    return Ok(_response);
                }

                var trainer = await _trainerRepository.GetByIdAsync(p => p.Id == id);

                if (trainer == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessage.DeleteOperationFailed;
                    return Ok(_response);
                }

                await _trainerRepository.DeleteAsync(trainer);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.DeleteOperationSuccess;
            }
            catch (Exception)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommonMessage.DeleteOperationFailed;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok(_response);
        }
    }
}
