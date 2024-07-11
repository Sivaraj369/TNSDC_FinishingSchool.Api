using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Domain.Models
{
    public class CandidateBasicDetails
    {
        public string CandidateNameAsPerAadhar { get; set; }
        public string WorkingCompanyName { get; set; }
        public DateTime DOJ { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string AnnualSalary { get; set; }
    }
}
