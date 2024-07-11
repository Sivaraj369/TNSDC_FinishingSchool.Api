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
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using TNSDC_FinishingSchool.Domain.Models;

namespace TNSDC_FinishingSchool.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
     [Route("api/v1/[controller]")]
    [ApiController]

    public class TrainerCreationController : ControllerBase
    {
        private readonly DbContext _dbContext;
        protected APIResponse _response;

        public TrainerCreationController(DbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<string>> GetTrainer(string queryparams)
        {
            string sql = @"EXEC sp_GetTrainer @queryParams, @jsonOutput OUTPUT";

            SqlParameter queryparam = new SqlParameter("@queryParams", queryparams ?? DBNull.Value.ToString());
            var jsonoutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { queryparam, jsonoutput });
                var result = jsonoutput.Value.ToString();

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

                string trainerJson = JsonConvert.SerializeObject(trainer);

                string sql = @"EXEC sp_InsertTrainer @jsonInput, @jsonOutput Output";
                var values = new SqlParameter("jsonInput", trainerJson);
                var jsonOutput = new SqlParameter("jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

                int result = await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { values, jsonOutput });

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.CreateOperationSuccess;
                _response.Result = "";
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok(_response);
        }


    }
}
