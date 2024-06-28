using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Data.DbContexts;
using TNSDC_FinishingSchool.Domain.Contracts;
using TNSDC_FinishingSchool.Domain.Model;

namespace TNSDC_FinishingSchool.Data.Repositories
{
    public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task UpdateAsync(Trainer trainer)
        {
            _dbContext.Update(trainer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
