using System.Linq;

namespace MovieLibrary.Core.Repository.Base;

public interface IServiceBase<TDto, TUpsertDto, in TParameters>
{
    TDto Find(int key);
    IQueryable<TDto> FindAll();
    IQueryable<TDto> FindByCondition(TParameters parameters);
    TDto Create(TUpsertDto entity);
    TDto Update(int key, TUpsertDto entity);
    void Delete(int key);
}
