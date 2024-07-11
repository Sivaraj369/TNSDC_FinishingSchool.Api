using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System;
using TNSDC_FinishingSchool.Bussiness.Common;
using System.Reflection.PortableExecutable;
using TNSDC_FinishingSchool.Bussiness.ApplicationConstants;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace TNSDC_FinishingSchool.Api.Controllers
{
     [Route("api/v1/[controller]")]
    [ApiController]
    public class UserProfileInfoController : ControllerBase
    {
        private readonly DbContext _dbContext;
        protected APIResponse _response;

        public UserProfileInfoController(DbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new APIResponse();
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getuserprofileinfo")]
        public async Task<ActionResult<string>> GetUserProfileInfo(string queryparams)
        {
            string userGetSessionKey = Request.Headers["userSessionKey"].ToString();

            string sql = @"EXEC Usp_UserProfileInfo_Get @queryParams, @userGetSessionKey, @jsonOutput OUTPUT, @userSessionKey OUTPUT";

            SqlParameter userGetSessionKeyParam = new SqlParameter("@userGetSessionKey", userGetSessionKey ?? DBNull.Value.ToString());
            SqlParameter queryparam = new SqlParameter("@queryParams", queryparams ?? DBNull.Value.ToString());
            var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };
            var userSessionKey = new SqlParameter("@userSessionKey", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { userGetSessionKeyParam, queryparam, jsonOutput, userSessionKey });

                var result = System.Text.Json.JsonSerializer.Deserialize<object>(jsonOutput.Value.ToString());

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = result;

                Response.Headers.Append("userSessionKey", userSessionKey.Value.ToString());
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
