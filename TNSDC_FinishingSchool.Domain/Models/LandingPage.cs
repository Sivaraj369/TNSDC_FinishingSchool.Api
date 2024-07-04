using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Domain.Common;

namespace TNSDC_FinishingSchool.Domain.Models
{
    public class LandingPage : BaseModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public byte[] Icon { get; set; }
    }
}
