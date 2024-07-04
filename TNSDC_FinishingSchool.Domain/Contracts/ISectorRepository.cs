using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Domain.Models;


namespace TNSDC_FinishingSchool.Domain.Contracts
{
    public interface ISectorRepository : IGenericRepository<Sector>
    {
        Task UpdateAsync(Sector sector);
    }
}
