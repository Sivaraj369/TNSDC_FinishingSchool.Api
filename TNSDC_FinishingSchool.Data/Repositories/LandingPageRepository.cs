using Microsoft.EntityFrameworkCore;
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
    public class LandingPageRepository : GenericRepository<LandingPage>, ILandingPageRepository
    {
        public LandingPageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task UpdateAsync(LandingPage landingPage)
        {
            _dbContext.Update(landingPage);
            await _dbContext.SaveChangesAsync();
        }
    }
}
