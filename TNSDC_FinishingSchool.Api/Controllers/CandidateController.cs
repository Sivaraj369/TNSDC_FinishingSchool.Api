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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using TNSDC_FinishingSchool.Domain.Models;
using TNSDC_FinishingSchool.Bussiness.JWT;
using Microsoft.AspNetCore.Authorization;

namespace TNSDC_FinishingSchool.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly DbContext _dbContext;
        protected APIResponse _response;
        private readonly IJwtUtils _jwtUtils;

        public CandidateController(DbContext dbContext, IJwtUtils jwtUtils)
        {
            _dbContext = dbContext;
            _response = new APIResponse();
            _jwtUtils = jwtUtils;
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[HttpPost("candidateBasicDetails")]
        //public async Task<ActionResult<APIResponse>> CandidateBasicDetails([FromBody] CandidateBasicDetails candidateBasicDetails)
        //{
        //    object result = "";
        //    try
        //    {
        //        string userGetSessionKey = Request.Headers["userSessionKey"].ToString();

        //        if (!ModelState.IsValid)
        //        {
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.DisplayMessage = CommonMessage.CreateOperationFailed;
        //            _response.AddError(ModelState.ToString());
        //            return Ok(_response);
        //        }

        //        string trainerJson = JsonConvert.SerializeObject(candidateBasicDetails);

        //        string sql = @"EXEC sp_InsertCandidateBasicDetails @userGetSessionKey, @jsonInput, @jsonOutput OUTPUT, @userSessionKey OUTPUT";

        //        SqlParameter userGetSessionKeyParam = new SqlParameter("@userGetSessionKey", userGetSessionKey ?? DBNull.Value.ToString());
        //        var values = new SqlParameter("jsonInput", trainerJson);
        //        var jsonOutput = new SqlParameter("jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };
        //        var userSessionKey = new SqlParameter("@userSessionKey", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

        //        await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { userGetSessionKeyParam, values, jsonOutput, userSessionKey });
        //        result = System.Text.Json.JsonSerializer.Deserialize<object>(jsonOutput.Value.ToString());

        //        //string token = _jwtUtils.GenerateJwtToken(candidateBasicDetails.PhoneNumber.ToString());

        //        _response.StatusCode = HttpStatusCode.Created;
        //        _response.IsSuccess = true;
        //        _response.DisplayMessage = result.ToString();
        //        _response.Result = result;

        //        //Response.Headers.Append("userSessionKey", userSessionKey.Value.ToString());

        //    }
        //    catch (Exception)
        //    {
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.DisplayMessage = CommonMessage.CreateOperationFailed;
        //        _response.AddError(result.ToString());
        //    }

        //    return Ok(_response);
        //}

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("candidateCreation")]
        public async Task<ActionResult<APIResponse>> CandidateCreation([FromBody] Candidate candidate)
        {
            object result = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return Ok(_response);
                }

                string trainerJson = JsonConvert.SerializeObject(candidate);

                string sql = @"EXEC sp_InsertCandidateLogin @jsonInput, @jsonOutput OUTPUT";

                var values = new SqlParameter("jsonInput", trainerJson);
                var jsonOutput = new SqlParameter("jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

                await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { values, jsonOutput });
                result = System.Text.Json.JsonSerializer.Deserialize<object>(jsonOutput.Value.ToString());

                var parsedJson = System.Text.Json.JsonDocument.Parse(result.ToString());
                var candidateId = parsedJson.RootElement.GetProperty("CandidateID").GetInt32();

                string token = _jwtUtils.GenerateJwtToken(candidateId.ToString());

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.CreateOperationSuccess;
                _response.Result = new { result = result, token = token };
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                _response.AddError(result.ToString());
            }

            return Ok(_response);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("applyCourse")]
        public async Task<ActionResult<APIResponse>> ApplyCourse([FromBody] ApplyCourse applyCourse)
        {
            object result = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return Ok(_response);
                }

                string trainerJson = JsonConvert.SerializeObject(applyCourse);

                string sql = @"EXEC sp_InsertApplyCourse @jsonInput, @jsonOutput Output";
                var values = new SqlParameter("jsonInput", trainerJson);
                var jsonOutput = new SqlParameter("jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

                await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { values, jsonOutput });
                result = System.Text.Json.JsonSerializer.Deserialize<object>(jsonOutput.Value.ToString());

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.CreateOperationSuccess;
                _response.Result = result;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                _response.AddError(result.ToString());
            }

            return Ok(_response);
        }

    }
}




