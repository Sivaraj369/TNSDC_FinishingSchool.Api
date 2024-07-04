using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Domain.Common;

namespace TNSDC_FinishingSchool.Domain.Models
{
    public class Sector : BaseModel
    {
        public int SectorID { get; set; }
        public string SectorName { get; set; }
        public string SectorDescription { get; set; }
        public string Imagepath { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
    }
}
