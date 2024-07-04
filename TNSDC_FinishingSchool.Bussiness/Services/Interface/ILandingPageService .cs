using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.DTO.LandingPage;
using TNSDC_FinishingSchool.Bussiness.DTO.Sector;
using TNSDC_FinishingSchool.Bussiness.DTO.Trainer;

namespace TNSDC_FinishingSchool.Bussiness.Services.Interface
{
    public interface ILandingPageService
    {
        Task<LandingPageDto> GetByIdAsync(int id);

        Task<IEnumerable<LandingPageDto>> GetAllAsync();

    }
}
