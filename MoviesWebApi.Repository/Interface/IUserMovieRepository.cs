using MoviesWebApi.Common;
using MoviesWebApi.Entity;
using System.Collections.Generic;

namespace MoviesWebApi.Repository.Interface
{
    public interface IUserMovieRepository : IDetailDataRepository<UserMovie, int, int>
    {
        DbResult<List<UserMovie>> GetLastNRecordsByMasterId(int n, int masterId);
        DbResult<List<UserMovie>> GetAllByMovieId(int movieId);
    }
}
