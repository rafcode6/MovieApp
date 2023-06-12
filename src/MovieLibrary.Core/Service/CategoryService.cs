using Microsoft.EntityFrameworkCore;
using MovieLibrary.Core.Repository.Abstraction;
using MovieLibrary.Core.Repository.Base;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using MovieLibrary.Dto;
using System.Linq;

namespace MovieLibrary.Core.Service;

public class CategoryService : ServiceBase<CategoryDto, CategoryUpsertDto, CategoryParameters, Category>, ICategoryService
{
    public CategoryService(MovieLibraryContext movieLibraryContext) : base(movieLibraryContext)
    {
    }

    protected override IQueryable<Category> buildConditions(IQueryable<Category> query, CategoryParameters parameters)
    {
        return query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);
    }

    protected override IQueryable<Category> updateIncludes(IQueryable<Category> query)
    {
        return query.Include(x => x.MovieCategories);
    }
}
