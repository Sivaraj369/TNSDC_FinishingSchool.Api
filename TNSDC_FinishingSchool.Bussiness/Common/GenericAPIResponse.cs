using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Bussiness.Common
{
    public class GenericAPIResponse<T>
    {
        public int StatusCode { get; set; }
        public string StatusMsg { get; set; } = null!;
        public ICollection<string> ErrorMsg { get; set; } = new List<string>();
        public T? Response { get; set; }//Data

        public GenericAPIResponse<T> SucceesResponse(T responce)
        {

            return new GenericAPIResponse<T>()
            {
                StatusCode = 200,
                StatusMsg = APIErrorCodeMessages.SUCCESS,
                Response = responce
            };
        }
        public GenericAPIResponse<T> SucceesResponseForPost(T msg)
        {
            return new GenericAPIResponse<T>()
            {
                StatusCode = 201,
                StatusMsg = APIErrorCodeMessages.SUCCESS,
                Response = msg

            };
        }
        public GenericAPIResponse<T> ErrorResponse(string msg)
        {
            return new GenericAPIResponse<T>()
            {
                StatusCode = 400,
                StatusMsg = APIErrorCodeMessages.FAILURE,
                ErrorMsg = new List<string>
                {
                    msg,
                },

            };
        }

        public GenericAPIResponse<T> InternalErrorResponse(string msg)
        {
            return new GenericAPIResponse<T>()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                StatusMsg = APIErrorCodeMessages.FAILURE,
                ErrorMsg = new List<string>
                {
                    msg,
                },
            };
        }
        public GenericAPIResponse<T> Unauthorized()
        {
            return new GenericAPIResponse<T>()
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                StatusMsg = APIErrorCodeMessages.FAILURE,
                ErrorMsg = new List<string>
                {
                    APIErrorCodeMessages.UNAUTHORIZED,
                },
            };
        }
        public GenericAPIResponse<T> BuildResponse(int statusCode, string statusMsg, List<string> msg = default, T response = default)
        {
            var errorResponse = new GenericAPIResponse<T>()
            {
                StatusCode = statusCode,
                StatusMsg = statusMsg,
            };
            if (msg?.Count > 0)
                errorResponse.ErrorMsg = msg;
            if (response != null)
                Response = response;
            return errorResponse;
        }

    }
}
