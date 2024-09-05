using Api.Helper;
using Core.Interfaces;
using Infrastructure.Data;


namespace API.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register services here
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ImagesUrlUploadResolver>(); 
            services.AddScoped<ImagesUrlComplaintResolver>(); 
            return services;
        }
    }
}


