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
using Microsoft.AspNetCore.Authorization;

namespace TNSDC_FinishingSchool.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DbContext _dbContext;
        protected APIResponse _response;

        public CourseController(DbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetCourse")]
        public async Task<ActionResult<string>> GetCourse(string queryparams)
        {
            string sql = @"EXEC Usp_Course_Get @queryParams, @jsonOutput OUTPUT";

            SqlParameter queryparam = new SqlParameter("@queryParams", SqlDbType.NVarChar)
            {
                Size = -1,
                Value = (object)queryparams ?? DBNull.Value
            };
            var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { queryparam, jsonOutput });
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetPopularCourse")]
        public async Task<ActionResult<string>> GetPopularCourse(string queryparams)
        {
            string sql = @"EXEC Usp_PopularCourse_Get @queryParams, @jsonOutput OUTPUT";

            SqlParameter queryparam = new SqlParameter("@queryParams", SqlDbType.NVarChar)
            {
                Size = -1,
                Value = (object)queryparams ?? DBNull.Value
            };
            var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { queryparam, jsonOutput });
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetCoursedetails")]
        public async Task<ActionResult<string>> GetCourseDetails(string queryparams)
        {
            string sql = @"EXEC Usp_Course_Get_Details @queryParams, @jsonOutput OUTPUT";

            SqlParameter queryparam = new SqlParameter("@queryParams", queryparams ?? DBNull.Value.ToString());
            var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { queryparam, jsonOutput });
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
