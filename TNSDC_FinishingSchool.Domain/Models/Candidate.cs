using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Domain.Models
{
    public class Candidate
    {
        public string AadharNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string CandidateNameAsPerAadhar { get; set; }
        public string EmailId { get; set; }
        public string ProfilePictureUrl { get; set; } // Optional if handling image URL
        public string EducationalQualification { get; set; }
        public string Community { get; set; }
    }
}
