using Microsoft.EntityFrameworkCore;
using MovieLibrary.Core.Repository.Abstraction;
using MovieLibrary.Core.Repository.Base;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using MovieLibrary.Dto;
using System.Linq;

namespace MovieLibrary.Core.Service;

public class MovieService : ServiceBase<MovieDto, MovieUpsertDto, MovieParameters, Movie>, IMovieService
{
    public MovieService(MovieLibraryContext movieLibraryContext) : base(movieLibraryContext)
    {
    }

    protected override IQueryable<Movie> buildConditions(IQueryable<Movie> query, MovieParameters parameters)
    {
        if (!string.IsNullOrEmpty(parameters.Title))
        {
            query = query.Where(x => EF.Functions.Like(x.Title, $"%{parameters.Title}%"));
        }

        if (parameters?.CategoriesID.Any() ?? false)
        {
            query = query.Where(x => x.MovieCategories.Any(y => parameters.CategoriesID.Contains(y.CategoryId)));
        }

        if (parameters.MinImdb.HasValue)
        {
            query = query.Where(x => x.ImdbRating >= parameters.MinImdb);
        }

        if (parameters.MaxImdb.HasValue)
        {
            query = query.Where(x => x.ImdbRating <= parameters.MaxImdb);
        }

        return query
            .OrderByDescending(on => on.ImdbRating)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);
    }

    protected override IQueryable<Movie> updateIncludes(IQueryable<Movie> query)
    {
        return query.Include(x => x.MovieCategories);
    }
}
