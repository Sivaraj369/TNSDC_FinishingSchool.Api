using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Bussiness.DTO.Trainer
{
    public class UpdateTrainerDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Gender { get; set; } // Consider using an enum for Gender

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 12)]
        public string AadharNumber { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string PanCardNumber { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public string Specialization { get; set; }

    }
}
