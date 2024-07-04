using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.DTO.Trainer;
using TNSDC_FinishingSchool.Bussiness.Services.Interface;
using TNSDC_FinishingSchool.Domain.Contracts;
using TNSDC_FinishingSchool.Domain.Models;


namespace TNSDC_FinishingSchool.Bussiness.Services.Implementations
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public TrainerService(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task<TrainerDto> CreateAsync(CreateTrainerDto createTrainerDto)
        {
            var trainer = _mapper.Map<Trainer>(createTrainerDto);

            var createdEntity = await _trainerRepository.CreateAsync(trainer);

            var entity = _mapper.Map<TrainerDto>(createdEntity);

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var trainer = await _trainerRepository.GetByIdAsync(x => x.Id == id);

            await _trainerRepository.DeleteAsync(trainer);
        }

        public async Task<IEnumerable<TrainerDto>> GetAllAsync()
        {
            var trainer = await _trainerRepository.GetAllAsync();

            return _mapper.Map<List<TrainerDto>>(trainer);
        }

        public async Task<TrainerDto> GetByIdAsync(int id)
        {
            var category = await _trainerRepository.GetByIdAsync(x => x.Id == id);

            return _mapper.Map<TrainerDto>(category);
        }

        public async Task UpdateAsync(UpdateTrainerDto updateTrainerDto)
        {
            var category = _mapper.Map<Trainer>(updateTrainerDto);

            await _trainerRepository.UpdateAsync(category);
        }
    }
}









