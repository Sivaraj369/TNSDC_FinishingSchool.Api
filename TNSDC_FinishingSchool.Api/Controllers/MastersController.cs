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

namespace TNSDC_FinishingSchool.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MastersController : ControllerBase
    {
        private readonly DbContext _dbContext;
        protected APIResponse _response;

        public MastersController(DbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetQualification")]
        public async Task<ActionResult<string>> GetQualification()
        {
            string queryParams = string.Empty;
            string sql = @"EXEC Usp_Masters_Get @masterName, @queryParams, @jsonOutput OUTPUT";

            SqlParameter queryParam = new SqlParameter("@queryParams", SqlDbType.NVarChar)
            {
                Size = -1,
                Value = (object)queryParams ?? DBNull.Value
            };

            SqlParameter masterName = new SqlParameter("@masterName", "Qualification");
            var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { masterName, queryParam, jsonOutput });
                var result = System.Text.Json.JsonSerializer.Deserialize<object>(jsonOutput.Value.ToString());

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


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetMastersIncandidate")]
        public async Task<ActionResult<string>> GetMastersInCandidate()
        {
            string queryParams = string.Empty;
            string sql = @"EXEC Usp_Masters_Get @masterName, @queryParams, @jsonOutput OUTPUT";

            SqlParameter queryParam = new SqlParameter("@queryParams", SqlDbType.NVarChar)
            {
                Size = -1,
                Value = (object)queryParams ?? DBNull.Value
            };

            SqlParameter masterName = new SqlParameter("@masterName", "candidatemasters");
            var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { masterName, queryParam, jsonOutput });
                var result = System.Text.Json.JsonSerializer.Deserialize<object>(jsonOutput.Value.ToString());

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















