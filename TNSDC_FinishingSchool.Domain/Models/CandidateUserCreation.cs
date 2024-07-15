using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Domain.Models
{
    public class CandidateUserCreation
    {
        public long MobileNo { get; set; }
        public int OTP { get; set; }
        public int VerificationMode { get; set; }
    }
}
