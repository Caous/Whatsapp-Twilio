using DomainDrivenDesign.Application.Interfaces;
using DomainDrivenDesign.Application.Services;
using DomainDrivenDesign.Domain.Interfaces.Repositories;
using DomainDrivenDesign.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesign.Infrastructure.IoC;

public static class DomainDrivenDesignModule
{
    public static void Register(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<ITwilioRepository, TwilioRepository>();

        // Services
        services.AddScoped<IWhatsappService, WhatsappService>();

        // Mappers
        //services.AddAutoMapper(typeof(SampleDataMapper));

        // App Services (Commands in future)
         }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<SampleContext>(opt => opt.UseInMemoryDatabase("SampleData"));
    }
}
