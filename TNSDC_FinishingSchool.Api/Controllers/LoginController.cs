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

namespace TNSDC_FinishingSchool.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly DbContext _dbContext;
        protected APIResponse _response;
        private readonly IJwtUtils _jwtUtils;

        public LoginController(DbContext dbContext, IJwtUtils jwtUtils)
        {
            _dbContext = dbContext;
            _response = new APIResponse();
            _jwtUtils = jwtUtils;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Login")]
        public async Task<ActionResult<APIResponse>> Authenticate([FromBody] Login login)
        {

            string token = _jwtUtils.GenerateJwtToken("1");
            return Ok(token);
        }
    }
}
