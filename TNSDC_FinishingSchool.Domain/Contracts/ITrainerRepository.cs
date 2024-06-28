using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Domain.Model;


namespace TNSDC_FinishingSchool.Domain.Contracts
{
    public interface ITrainerRepository : IGenericRepository<Trainer>
    {
        Task UpdateAsync(Trainer trainer);
    }
}
