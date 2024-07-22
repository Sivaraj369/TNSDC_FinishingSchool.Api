using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Domain.Models
{
    public class TrainingPartner
    {
        public string PartnerName { get; set; }
        public string PartnerType { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public int State { get; set; }
        public int District { get; set; }
        public int City { get; set; }
        public string PinCode { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string PanNo { get; set; }
        public string TanNo { get; set; }
        public string IncorporationNo { get; set; }
        public string CinNo { get; set; }
        public string SpocName { get; set; }
        public string SpocNo { get; set; }
        public string SpocEmail { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public string NameAsOnBank { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IfscCode { get; set; }
    }
}
