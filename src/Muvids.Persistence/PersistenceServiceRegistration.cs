using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muvids.Application.Contracts.Persistence;
using Muvids.Application.Contracts.Persistence.Common;
using Muvids.Persistence.Repositories;
using Muvids.Persistence.Repositories.Common;

namespace Muvids.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        services.AddDbContext<MuvidsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MuvidsConnectionString"))
        );

        // Base repository
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>)); 

        // Other repositories
        services.AddScoped<IMovieRepository, MovieRepository>();


        return services;
    }
}