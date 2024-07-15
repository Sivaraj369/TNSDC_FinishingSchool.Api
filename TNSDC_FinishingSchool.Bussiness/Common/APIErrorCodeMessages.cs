using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Bussiness.Common
{
    public class APIErrorCodeMessages
    {
        #region Common responseMsg
        public const string FAILURE = "FAILURE";
        public const string SUCCESS = "SUCCESS";
        #endregion

        #region Common ErrorMsg
        public const string NULLCURRENTURL = "Can't Access Current Url, httpcontext is null";
        public const string FILENOTFOUND = "File not Found";
        public const string UNAUTHORIZED = "Unauthorized";
        #endregion

        #region ErrorMessages
        public const string EMPTYDATATABLE = "No Data in the DataTable";
        #region AppUser
        public const string USERALREADYEXIST = "Can't create New User with this MobileNo, User Already Exist.";
        public const string INVALIDUSERNAMEORPASSWORD = "Invalid Username or Password.";

        #endregion

        #region OTPVerification
        public const string INVALIDOTP = "Invalid OTP, Please try again";


        #endregion

        #endregion


    }
}
