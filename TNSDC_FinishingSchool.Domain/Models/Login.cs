using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Domain.Models
{
    public class Login
    {
        public long MobileNumber { get; set; }
        public int Otp { get; set; }
        public int OtpType { get; set; } = 1;
    }
}
