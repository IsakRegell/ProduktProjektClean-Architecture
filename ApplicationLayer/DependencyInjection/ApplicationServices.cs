using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using ApplicationLayer.Mapping;

namespace ApplicationLayer.DependencyInjection // ← detta ska matcha var filen ligger
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // AutoMapper hittar alla profiler i samma assembly
            services.AddAutoMapper(typeof(ProductProfile).Assembly);

            return services;
        }
    }
}
