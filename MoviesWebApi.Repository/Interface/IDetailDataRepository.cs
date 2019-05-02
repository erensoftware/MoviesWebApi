using System;
using System.Collections.Generic;
using MoviesWebApi.Common;

namespace MoviesWebApi.Repository.Interface
{
    /// <summary>
    /// Data Rep interface for detail  tables
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <typeparam name="M">entity master PK type</typeparam>
    /// <typeparam name="D">entity detail PK type</typeparam>
    public interface IDetailDataRepository<T, M, D>
    {
        DbResult<List<T>> GetAllByMasterId(M masterId);
        DbResult<int> DeleteAllByMasterId(M masterId);
        DbResult<List<T>> GetAll();
        DbResult<List<T>> GetFirstNRecords(int n);
        DbResult<List<T>> GetLastNRecords(int n);
        DbResult<T> GetSingle(M masterId, D detailId);
        DbResult<T> Insert(T entity);
        DbResult<int> Update(T entity);
        DbResult<int> Delete(M masterId, D detailId);
    }
}
