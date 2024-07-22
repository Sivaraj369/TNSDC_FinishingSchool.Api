using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data; 
using System.Net;
using System.Threading.Tasks;
using System;
using TNSDC_FinishingSchool.Bussiness.ApplicationConstants;
using TNSDC_FinishingSchool.Bussiness.Common;
using TNSDC_FinishingSchool.Bussiness.JWT;
using TNSDC_FinishingSchool.Domain.Models;

namespace TNSDC_FinishingSchool.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CourseListController : ControllerBase 
    {
        private readonly DbContext _dbContext;
        protected APIResponse _response;
        private readonly IJwtUtils _jwtUtils;

        public CourseListController(DbContext dbContext, IJwtUtils jwtUtils)
        {
            _dbContext = dbContext;
            _response = new APIResponse();
            _jwtUtils = jwtUtils;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("CreateCourseList")]
        public async Task<ActionResult<APIResponse>> CandidateCreation1([FromBody] CourseList courselist)
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


                string trainerJson = JsonConvert.SerializeObject(courselist);

                string sql = @"EXEC USP_CreateCourse @jsonInput, @jsonOutput OUTPUT";

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



        [HttpGet("ViewCourseList")]
        public  async Task<ActionResult<string>>  GetCourseList()
        {

            string sql = @"EXEC USP_GetMasterValues @InputParamJSON, @jsonOutput OUTPUT";
            var inputParam = new SqlParameter("@InputParamJSON", SqlDbType.NVarChar)
            {
                Value = "ViewCourse"
            };

            var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1)
            {
                Direction = ParameterDirection.Output
            };

            try
            {

                await _dbContext.Database.ExecuteSqlRawAsync(sql, inputParam, jsonOutput);

                var result = (jsonOutput.Value != DBNull.Value && !string.IsNullOrEmpty(jsonOutput.Value.ToString()))
                    ? System.Text.Json.JsonSerializer.Deserialize<object>(jsonOutput.Value.ToString())
                    : "No records found";

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(ex.Message);
                _response.AddError(ex.StackTrace);
            }

            return Ok(_response);
        }

    }
}
