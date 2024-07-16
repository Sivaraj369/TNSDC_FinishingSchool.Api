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
using TNSDC_FinishingSchool.Domain.Models;

namespace TNSDC_FinishingSchool.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MasterValuesController : ControllerBase
    {
        private readonly DbContext _dbContext;
        protected APIResponse _response;

        public MasterValuesController(DbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]       
        [HttpGet("GetMasterValues")]
        public async Task<ActionResult<string>> GetMasters(string queryparams)
        {
            string sql = @"EXEC USP_GetMasterValues @InputParamJSON, @jsonOutput OUTPUT";

            SqlParameter queryparam = new SqlParameter("@InputParamJSON", SqlDbType.NVarChar)
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
    }


}













