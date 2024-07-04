using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.DTO.Sector;
using TNSDC_FinishingSchool.Bussiness.DTO.Trainer;
using TNSDC_FinishingSchool.Bussiness.Services.Interface;
using TNSDC_FinishingSchool.Domain.Contracts;
using TNSDC_FinishingSchool.Domain.Models;

namespace TNSDC_FinishingSchool.Bussiness.Services.Implementations
{
    public class SectorService : ISectorService
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;

        public SectorService(ISectorRepository sectorRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SectorDto>> GetAllAsync()
        {
            var result = await _sectorRepository.GetAllAsync();

            return _mapper.Map<List<SectorDto>>(result);
        }

        public async Task<SectorDto> GetByIdAsync(int id)
        {
            var result = await _sectorRepository.GetByIdAsync(x => x.SectorID == id);

            return _mapper.Map<SectorDto>(result);
        }
    }
}
