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
using System.IO;

namespace TNSDC_FinishingSchool.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly DbContext _dbContext;
        protected APIResponse _response;
        private readonly IJwtUtils _jwtUtils;

        public BatchController(DbContext dbContext, IJwtUtils jwtUtils)
        {
            _dbContext = dbContext;
            _response = new APIResponse();
            _jwtUtils = jwtUtils;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("CreateBatch")]
        public async Task<ActionResult<APIResponse>> CandidateCreation1([FromBody] Batch trainerpartner)
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


                string trainerJson = JsonConvert.SerializeObject(trainerpartner);

                string sql = @"EXEC Usp_BatchDetails @jsonInput, @jsonOutput OUTPUT";

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



        [HttpGet("ViewBatch")]
        public async Task<ActionResult<APIResponse>> GetTrainingPartners()
        {
            {
                string sql = @"EXEC GetBatchDetails @jsonOutput OUTPUT";

                var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

                try
                {
                    await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { jsonOutput });

                    var result = (jsonOutput.Value != DBNull.Value && !string.IsNullOrEmpty(jsonOutput.Value.ToString())) ? System.Text.Json.JsonSerializer.Deserialize<object>(jsonOutput.Value.ToString()) : "No records found";

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
}
