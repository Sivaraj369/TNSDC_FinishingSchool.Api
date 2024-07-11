using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.Common;
using TNSDC_FinishingSchool.Bussiness.JWT;
using TNSDC_FinishingSchool.Bussiness.Services.Implementations;
using TNSDC_FinishingSchool.Bussiness.Services.Interface;

namespace TNSDC_FinishingSchool.Bussiness
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<ILandingPageService, LandingPageService>();
            services.AddScoped<IJwtUtils, JwtUtils>();

            return services;
        }

    }
}
