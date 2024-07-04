using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Data.DbContexts;
using TNSDC_FinishingSchool.Domain.Contracts;
using TNSDC_FinishingSchool.Domain.Models;

namespace TNSDC_FinishingSchool.Data.Repositories
{
    public class SectorRepository : GenericRepository<Sector>, ISectorRepository
    {
        public SectorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task UpdateAsync(Sector sector)
        {
            _dbContext.Update(sector);
            await _dbContext.SaveChangesAsync();
        }
    }
}
