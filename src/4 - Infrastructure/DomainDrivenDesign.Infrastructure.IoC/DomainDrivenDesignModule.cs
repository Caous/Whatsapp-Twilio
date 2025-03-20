using DomainDrivenDesign.Application.Interfaces;
using DomainDrivenDesign.Application.Services;
using DomainDrivenDesign.Domain.Interfaces.Repositories;
using DomainDrivenDesign.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DomainDrivenDesign.Infrastructure.IoC;

public static class DomainDrivenDesignModule
{
    public static void Register(this IServiceCollection services, IConfiguration configuration)
    {
        // Repositories
        services.AddScoped<ITwilioRepository, TwilioRepository>();
        services.AddScoped<IMongoRepository, MongoRepository>();

        // Services
        services.AddScoped<IWhatsappService, WhatsappService>();

        // Mappers
        //services.AddAutoMapper(typeof(SampleDataMapper));

        // App Services (Commands in future)


        services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = configuration.GetSection("ConnectionStrings:MongoConnection").Value;
            return new MongoClient(connectionString);
        });

        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var database = "whatsapp";
            return client.GetDatabase(database);
        });

    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<SampleContext>(opt => opt.UseInMemoryDatabase("SampleData"));
    }
}
