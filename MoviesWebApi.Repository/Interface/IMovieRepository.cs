using MoviesWebApi.Common;
using MoviesWebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesWebApi.Repository.Interface
{
    public interface IMovieRepository : IDataRepository<Movie, int>
    {
        DbResult<List<Movie>> GetByTitle(string title);
        DbResult<List<Movie>> GetByYear(short year);
        DbResult<List<Movie>> GetByGenre(string genre);
        DbResult<List<Movie>> GetByTitleGenreYear(string title, string genre, short? year);
        DbResult<List<Movie>> GetByTitleGenre(string title, string genre);
        DbResult<List<Movie>> GetByTitleYear(string title, short? year);
        DbResult<List<Movie>> GetByGenreYear(string genre, short? year);
    }
}
