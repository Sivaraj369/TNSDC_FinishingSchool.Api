using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.DTO.LandingPage;
using TNSDC_FinishingSchool.Bussiness.DTO.Sector;
using TNSDC_FinishingSchool.Bussiness.DTO.Trainer;
using TNSDC_FinishingSchool.Bussiness.Services.Interface;
using TNSDC_FinishingSchool.Domain.Contracts;
using TNSDC_FinishingSchool.Domain.Models;

namespace TNSDC_FinishingSchool.Bussiness.Services.Implementations
{
    public class LandingPageService : ILandingPageService
    {
        private readonly ILandingPageRepository _landingPageRepository;
        private readonly IMapper _mapper;

        public LandingPageService(ILandingPageRepository landingPageRepository, IMapper mapper)
        {
            _landingPageRepository = landingPageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LandingPageDto>> GetAllAsync()
        {
            var result = await _landingPageRepository.GetAllAsync();

            return _mapper.Map<List<LandingPageDto>>(result);
        }

        public async Task<LandingPageDto> GetByIdAsync(int id)
        {
            var result = await _landingPageRepository.GetByIdAsync(x => x.Id == id);

            return _mapper.Map<LandingPageDto>(result);
        }
    }
}
