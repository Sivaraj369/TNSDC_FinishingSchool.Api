using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Domain.Models
{
    public class CreateTrainer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

   
        public string Email { get; set; }

     
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

       
        public DateTime DateOfBirth { get; set; }

        public string AadharNumber { get; set; }

        public string PanCardNumber { get; set; }

        public string Qualification { get; set; }

        public string Specialization { get; set; }

        public int Experience { get; set; }

        public string Certification { get; set; }

        public string State { get; set; }

        public string District { get; set; }

        public string City { get; set; }

      
        public string PinCode { get; set; }
    }
}
