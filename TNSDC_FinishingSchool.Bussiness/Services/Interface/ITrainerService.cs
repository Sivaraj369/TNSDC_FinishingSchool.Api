using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.DTO.Trainer;

namespace TNSDC_FinishingSchool.Bussiness.Services.Interface
{
    public interface ITrainerService
    {
        Task<TrainerDto> GetByIdAsync(int id);

        Task<IEnumerable<TrainerDto>> GetAllAsync();

        Task<TrainerDto> CreateAsync(CreateTrainerDto createTrainerDto);

        Task UpdateAsync(UpdateTrainerDto updateTrainerDto);

        Task DeleteAsync(int id);
    }
}
