using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Domain.Models
{
    public class Batch
    {

        public string BatchId { get; set; } 
        public string Course { get; set; }
        public string CourseMode { get; set; }
        public string Target { get; set; }
        public string Trainers { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
    }
}
