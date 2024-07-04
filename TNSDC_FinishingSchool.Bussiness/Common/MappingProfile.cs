using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.DTO.LandingPage;
using TNSDC_FinishingSchool.Bussiness.DTO.Trainer;
using TNSDC_FinishingSchool.Domain.Models;

namespace TNSDC_FinishingSchool.Bussiness.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Trainer, CreateTrainerDto>().ReverseMap();
            CreateMap<Trainer, UpdateTrainerDto>().ReverseMap();
            CreateMap<Trainer, TrainerDto>().ReverseMap();

            CreateMap<LandingPage, LandingPageDto>().ReverseMap();
        }
    }
}