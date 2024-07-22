using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.Common;
using TNSDC_FinishingSchool.Bussiness.JWT;
using TNSDC_FinishingSchool.Bussiness.Services.Interface;
using TNSDC_FinishingSchool.Bussiness.SMS_Service;
using TNSDC_FinishingSchool.Domain.Exceptions;

namespace TNSDC_FinishingSchool.Bussiness.Services.Implementations
{
    public class MobileOtpService : IMobileOtpService
    {
        private readonly SMSService _smsService;
        private readonly IConfiguration _configuration;
        private readonly IJwtUtils _jwtUtils;
        private readonly DbContext _dbContext;

        public MobileOtpService(SMSService sms_Service, IConfiguration configuration, IJwtUtils jwtUtils, DbContext dbContext)
        {
            _smsService = sms_Service;
            _configuration = configuration;
            _jwtUtils = jwtUtils;
            _dbContext = dbContext;
        }

        //public async Task<GenericAPIResponse<AuthResp>> AuthenticateUser(AuthReq req)
        //{
        //    var response = new GenericAPIResponse<AuthResp>();
        //    try
        //    {
        //        var user = await _appUserRepo.GetByAsync(x => x.Username.Equals(req.Username) && x.IsDeleted != true
        //        && x.IsActive == true) ?? throw new UserFriendlyException(ApiErrorCodeMessages.INVALIDUSERNAMEORPASSWORD);

        //        bool isValid = BcryptPassword.VerifyPassword(req.Password, user.Password);
        //        if (!isValid)
        //            throw new UserFriendlyException(ApiErrorCodeMessages.INVALIDUSERNAMEORPASSWORD);
        //        if (user.IsFirstLogin == true)
        //        {
        //            user.IsFirstLogin = false;
        //            user.UpdatedAt = DateTime.Now;
        //            await _appUserRepo.UpdateAsync(user);
        //        }

        //        string token = _jwtUtils.GenerateJwtToken(user);
        //        var resp = new AuthResp
        //        {
        //            Token = token,
        //            MobileNo = user.MobileNo,
        //            UserId = user.Id,
        //            UserName = user.Username
        //        };
        //        return response.SucceesResponse(resp);
        //    }
        //    catch (UserFriendlyException ex)
        //    {
        //        return response.ErrorResponse(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return response.InternalErrorResponse(ex.Message);
        //    }
        //}

        //public async Task<GenericAPIResponse<string>> CreateUser(CreateUserReq req)
        //{
        //    var response = new GenericAPIResponse<string>();
        //    try
        //    {
        //        var user = await _appUserRepo.GetUserByMobileNumber(req.MobileNo);
        //        if (user != null)
        //            throw new UserFriendlyException(ApiErrorCodeMessages.USERALREADYEXIST);

        //        //var otp = await _otpVerificationRepo.GetByAsync(x => x.MobileNo == req.MobileNo && x.VerificationModeId == req.VerificationMode
        //        // && x.VerificationCode == req.OTP && x.ExpiresAt >= DateTime.Now && x.IsDeleted == false
        //        // && x.IsVerified == false) ?? throw new UserFriendlyException(ApiErrorCodeMessages.INVALIDOTP);

        //        //otp.IsVerified = true;
        //        //await _otpVerificationRepo.UpdateAsync(otp);

        //        int passwordLength = Convert.ToInt32(_configuration.GetSection("RandomGenerateConfig")["PasswordLength"]?.ToString());
        //        //string password = CommonMethods.GenerateRandomPassword(passwordLength);
        //        string password = "123";
        //        var newUser = new AppUser
        //        {
        //            MobileNo = req.MobileNo,
        //            Password = BcryptPassword.HashPassword(password),
        //            UserRoleId = 1
        //        };
        //        await _appUserRepo.AddAsync(newUser);
        //        await _appUserRepo.ReloadEntityAsync(newUser);

        //        var smsBodyReplace = new Dictionary<string, string>(){
        //            {"@@RandomOTP",$"{newUser.Username},{password}" }
        //        };

        //        //var resp = await _smsService.SendSms("Login_Otp_Content", "Login_Otp_TempId", req.MobileNo, smsBodyReplace);

        //        //if (resp.StatusCode != HttpStatusCode.OK)
        //        //    return response.BuildResponse((int)resp.StatusCode, ApiErrorCodeMessages.FAILURE, [resp.ErrorMsg]);

        //        return response.SucceesResponse("Candidate Created Successfully");
        //    }
        //    catch (UserFriendlyException ex)
        //    {
        //        return response.ErrorResponse(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return response.InternalErrorResponse(ex.Message);
        //    }
        //}

        public async Task<APIResponse> GenerateSignupOTP(long mobileNo)
        {
            var response = new APIResponse();
            try
            {
                //string sql = @"EXEC USP_MobileOtpVerfication @InputType, @MobileNo, @Otp, @jsonOutput OUTPUT";

                //SqlParameter InputType = new SqlParameter("@InputType", "CHECK_MOBILENO");
                //SqlParameter MobileNo = new SqlParameter("@MobileNo", mobileNo);
                //SqlParameter Otp = new SqlParameter("@Otp", "");
               
                //var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

                //await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { InputType, MobileNo, Otp, jsonOutput });

                //if (jsonOutput.Value.ToString() == "0")
                //    throw new UserFriendlyException("Mobile no does not exists");

                var randomOtp = CommonMethods.GenerateOtpText();
                var smsBodyReplace = new Dictionary<string, string>() { { "@@RandomOTP", randomOtp } };
                var resp = await _smsService.SendSms("Login_Otp_Content", "Login_Otp_TempId", mobileNo, smsBodyReplace);

                if (resp.StatusCode != HttpStatusCode.OK)
                    return response.ErrorResponse(resp.Errors, resp.StatusCode);

               
                string sql = @"EXEC USP_MobileOtpVerfication @InputType, @MobileNo, @Otp, @jsonOutput OUTPUT";

                SqlParameter InputType = new SqlParameter("@InputType", "INSERT_OTP");
                SqlParameter MobileNo = new SqlParameter("@MobileNo", mobileNo);
                SqlParameter Otp = new SqlParameter("@Otp", randomOtp);

                var jsonOutput = new SqlParameter("@jsonOutput", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

                var result = await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { InputType, MobileNo, Otp, jsonOutput });

                return response.SuccessResponse("OTP Sent Successfully.");
            }
            catch (UserFriendlyException ex)
            {
                return response.ErrorResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return response.InternalErrorResponse(ex.Message);
            }

        }
    }
}
