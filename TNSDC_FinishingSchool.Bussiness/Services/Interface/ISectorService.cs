using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.DTO.Sector;
using TNSDC_FinishingSchool.Bussiness.DTO.Trainer;

namespace TNSDC_FinishingSchool.Bussiness.Services.Interface
{
    public interface ISectorService
    {
        Task<SectorDto> GetByIdAsync(int id);

        Task<IEnumerable<SectorDto>> GetAllAsync();

    }
}
