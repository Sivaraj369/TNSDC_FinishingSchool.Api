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
    public class SectionController : ControllerBase
    {
        private readonly DbContext _dbContext;
        protected APIResponse _response;

        public SectionController(DbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetSections")]
        public async Task<ActionResult<string>> GetSection()
        {
            string sql = @"EXEC Usp_Sections_Get @jsonOutput OUTPUT";

            var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { jsonOutput });
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












