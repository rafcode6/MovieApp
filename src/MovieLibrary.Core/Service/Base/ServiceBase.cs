using Mapster;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using System.Linq;

namespace MovieLibrary.Core.Repository.Base;

public abstract class ServiceBase<TDetails, TUpsertDto, TParameter, TEntity>
    : IServiceBase<TDetails, TUpsertDto, TParameter>
    where TEntity : class, IEntity
{
    protected MovieLibraryContext MovieLibraryContext { get; set; }

    public ServiceBase(MovieLibraryContext movieLibraryContext)
    {
        MovieLibraryContext = movieLibraryContext;
    }


    public TDetails Find(int key)
        => MovieLibraryContext.Set<TEntity>().Where(x => x.Id == key).ProjectToType<TDetails>().FirstOrDefault();


    public IQueryable<TDetails> FindAll()
        => MovieLibraryContext.Set<TEntity>().AsNoTracking().ProjectToType<TDetails>();

    public IQueryable<TDetails> FindByCondition(TParameter parameter)
    {
        var query = MovieLibraryContext.Set<TEntity>().AsNoTracking();
        query = buildConditions(query, parameter);
        return query.ProjectToType<TDetails>();
    }

    public TDetails Create(TUpsertDto dto)
    {
        var entity = dto.Adapt<TEntity>();
        MovieLibraryContext.Set<TEntity>().Add(entity);
        MovieLibraryContext.SaveChanges();

        return entity.Adapt<TDetails>();
    }

    public TDetails Update(int id, TUpsertDto dto)
    {
        var query = MovieLibraryContext.Set<TEntity>().AsQueryable();
        query = updateIncludes(query);
        var entity = query.FirstOrDefault(x => x.Id == id);
        dto.Adapt(entity);
        MovieLibraryContext.SaveChanges();

        return entity.Adapt<TDetails>();
    }
    public void Delete(int id)
    {
        MovieLibraryContext.Remove(MovieLibraryContext.Set<TEntity>().Find(id));
        MovieLibraryContext.SaveChanges();
    }

    protected abstract IQueryable<TEntity> updateIncludes(IQueryable<TEntity> query);

    protected abstract IQueryable<TEntity> buildConditions(IQueryable<TEntity> query, TParameter parameter);
}
