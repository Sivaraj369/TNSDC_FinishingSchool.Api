using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.Common;

namespace TNSDC_FinishingSchool.Bussiness.Services.Interface
{
    public interface IMobileOtpService
    {
        Task<APIResponse> GenerateSignupOTP(long mobileNo);
        //Task<GenericAPIResponse<string>> CreateUser(CreateUserRequest req);
    }
}
