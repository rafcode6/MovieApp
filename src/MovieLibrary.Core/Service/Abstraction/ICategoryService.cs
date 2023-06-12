using MovieLibrary.Core.Repository.Base;
using MovieLibrary.Data.Entities;
using MovieLibrary.Dto;

namespace MovieLibrary.Core.Repository.Abstraction;

public interface ICategoryService : IServiceBase<CategoryDto, CategoryUpsertDto, CategoryParameters>
{

}