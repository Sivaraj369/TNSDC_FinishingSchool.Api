using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using TNSDC_FinishingSchool.Domain.Contracts;
using TNSDC_FinishingSchool.Bussiness.Common;
using TNSDC_FinishingSchool.Bussiness.ApplicationConstants;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using TNSDC_FinishingSchool.Bussiness.Services.Interface;
using TNSDC_FinishingSchool.Bussiness.DTO.Trainer;
using System.Linq;
using TNSDC_FinishingSchool.Bussiness.Services.Implementations;
using TNSDC_FinishingSchool.Bussiness.DTO.LandingPage;

namespace TNSDC_FinishingSchool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandingPageController : ControllerBase
    {
        private readonly ILandingPageService _landingPageService;
        protected APIResponse _response;

        public LandingPageController(ILandingPageService landingPageService)
        {
            _landingPageService = landingPageService;
            _response = new APIResponse();
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet()]
        public async Task<ActionResult<APIResponse>> GetDetails()
        {
            try
            {
                var result = await _landingPageService.GetAllAsync();

                //var result = resultAll.Select(t => new LandingPageDto
                //{
                //    Id = t.Id,
                //    Code = t.Code,
                //    Description = t.Description,
                //    IconBase64 = t.IconBase64 != null ? Convert.ToBase64String(t.Icon) : null
                //}).ToList();

                //var result = await _landingPageService.GetAllAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(ex.Message);
            }

            return Ok(_response);
        }
    }
}










