using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Domain.Exceptions
{
    public class UserFriendlyException : ApplicationException
    {
        public readonly string ErrorCode;

        public UserFriendlyException(string messageCode) : base(messageCode)
        {
            ErrorCode = messageCode;
        }

        public UserFriendlyException(string messageCode, Exception innerException) : base(messageCode, innerException)
        {
            ErrorCode = messageCode;
        }
    }

}
