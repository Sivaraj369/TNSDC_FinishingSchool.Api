using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Bussiness.Common
{
    public class APIWarning
    {
        public string Description { get; set; }

        public APIWarning(string description)
        {
            Description = description;
        }
    }
}
