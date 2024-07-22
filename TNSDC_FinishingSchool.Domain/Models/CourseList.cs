using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Domain.Models
{
    public class CourseList
    {
        public string CourseName { get; set; }
        public string EligibilityLevel { get; set; }
        public string SubCategory { get; set; }
        public string CourseCategory { get; set; }
        public string CourseMode { get; set; }
        public string FeesType { get; set; }
        public string Stipend { get; set; }
        public string StipendAmount { get; set; }
        public int MarketPrice { get; set; }
        public int ActualPrice { get; set; }
        public int CandidatePrice { get; set; }
        public int Target { get; set; }
        public string InternshipPartner { get; set; }
        public string PlacementPartner { get; set; }
        public string ExpectedSalary { get; set; }
        public string JobOpportunities { get; set; }
        public List<string> CourseAvailableLocation { get; set; }
        public string CourseBannerImage { get; set; }
        public string OverviewImage { get; set; }
        public string UploadCourseVideo { get; set; }
        public string UploadTestimonialVideo { get; set; }
        public string RegistrationClosing { get; set; }
        public string CandidateWithdrawn { get; set; }
        public string CourseDescription { get; set; }
    }
}
