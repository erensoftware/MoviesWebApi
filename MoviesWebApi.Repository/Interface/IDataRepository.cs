using System.Collections.Generic;
using MoviesWebApi.Common;

namespace MoviesWebApi.Repository.Interface
{
    /// <summary>
    /// Data Rep interface
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <typeparam name="M">entity PK type</typeparam>
    public interface IDataRepository<T, M>
    {
        DbResult<List<T>> GetAll();
        DbResult<List<T>> GetFirstNRecords(int n);
        DbResult<List<T>> GetLastNRecords(int n);
        DbResult<T> GetSingle(M id);
        DbResult<T> Insert(T entity);
        DbResult<int> Update(T entity);
        DbResult<int> Delete(M id);
    }
}
