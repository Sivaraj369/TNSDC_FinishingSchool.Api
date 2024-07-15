using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string ProfilePicture { get; set; }
        public string EducationQualification { get; set; }
        public string AcademicQualification { get; set; }
        public string InstitutionBoard { get; set; }
        public string PassedOutYear { get; set; }
        public string Percentage { get; set; }
        public string Community { get; set; } 
        public string DifferentlyAbled { get; set; } 
        public string Address { get; set; }
        public string District { get; set; }
        public string FamilyAnnualIncome { get; set; } 
        public string Pincode { get; set; }

    }
}
