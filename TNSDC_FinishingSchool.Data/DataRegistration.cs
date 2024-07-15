using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSDC_FinishingSchool.Bussiness.Common;
using TNSDC_FinishingSchool.Bussiness.Services.Implementations;
using TNSDC_FinishingSchool.Bussiness.Services.Interface;
using TNSDC_FinishingSchool.Bussiness.SMS_Service;
using TNSDC_FinishingSchool.Data.Repositories;
using TNSDC_FinishingSchool.Domain.Contracts;

namespace TNSDC_FinishingSchool.Data
{
    public static class DataRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ITrainerRepository, TrainerRepository>();
            services.AddScoped<ISectorRepository, SectorRepository>();
            services.AddScoped<ILandingPageRepository, LandingPageRepository>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<SMSService>();
            services.AddScoped<APIRequestHandler>();


            return services;
        }
    }
}
