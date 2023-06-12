using Microsoft.Extensions.DependencyInjection;
using MovieLibrary.Core.Repository.Abstraction;
using MovieLibrary.Core.Service;

namespace MovieLibrary.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services.AddScoped<IMovieService, MovieService>()
                        .AddScoped<ICategoryService, CategoryService>();
    }
}