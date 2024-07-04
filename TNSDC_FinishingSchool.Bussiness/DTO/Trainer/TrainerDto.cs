using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Bussiness.DTO.Trainer
{
    public class TrainerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; } // Consider using an enum for Gender

        public DateTime DateOfBirth { get; set; }

        public string AadharNumber { get; set; }

        public string PanCardNumber { get; set; }

        public string Qualification { get; set; }

        public string Specialization { get; set; }

    }
}
