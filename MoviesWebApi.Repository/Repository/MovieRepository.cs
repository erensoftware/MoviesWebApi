using MoviesWebApi.Entity;
using System.Collections.Generic;
using System.Linq;
using MoviesWebApi.Common;
using MoviesWebApi.Repository.Base;
using MoviesWebApi.Repository.DbConnectionManager;
using MoviesWebApi.Repository.Interface;

namespace MoviesWebApi.Repository.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(DbTransaction<Movie> dbTran = null) : base(dbTran)
        {
        }

        public DbResult<int> Delete(int id)
        {
            var entity = _db.Movie.SingleOrDefault(t => t.MovieId == id);
            if (entity == null)
            {
                return new DbResult<int> { IsSuccess = false, Message = "No entity found to delete", Result = 0 };
            }
            if (entity.UserMovie != null && entity.UserMovie.Count > 0)
            {
                return new DbResult<int> { IsSuccess = false, Message = "There are dependant records defined, cannot delete", Result = 0 };
            }
            _db.Movie.Remove(entity);
            DbResult<int> res = _dbTrans.SaveChanges(_db);
            //after a delete attempt, entity's foreign keys and entity collections are set to null by EF automatically, so we need to refetch the entity in case of an unsuccessful delete in order to preserve data integrity, otherwise related views get null reference exceptions.
            if (res != null && !res.IsSuccess)
            {
                _db.Entry(entity).Reload();
            }
            return res;
        }

        public DbResult<List<Movie>> GetAll()
        {
            var movieList = _db.Movie.ToList();
            return _dbTrans.GetListResult(movieList);
        }

        public DbResult<List<Movie>> GetByGenre(string genre)
        {
            var movieList = _db.Movie.Where(t => t.Genres.ToLower().Contains(genre.ToLower())).OrderBy(t => t.Title).ToList();
            return _dbTrans.GetListResult(movieList);
        }

        public DbResult<List<Movie>> GetByGenreYear(string genre, short? year)
        {
            List<Movie> movieList = null;
            if (!string.IsNullOrEmpty(genre))
                movieList = _db.Movie.Where(t => t.Genres.ToLower().Contains(genre.ToLower())).ToList();
            if (year != null)
            {
                if (movieList == null)
                    movieList = _db.Movie.Where(t => t.YearOfRelease == year).ToList();
                else
                    movieList = movieList.Where(t => t.YearOfRelease == year).ToList();
            }
            if (movieList != null)
                movieList = movieList.OrderBy(t => t.Title).ToList();
            return _dbTrans.GetListResult(movieList);
        }

        public DbResult<List<Movie>> GetByTitle(string title)
        {
            var movieList = _db.Movie.Where(t => t.Title.ToLower().Contains(title.ToLower())).OrderBy(t => t.Title).ToList();
            return _dbTrans.GetListResult(movieList);
        }

        public DbResult<List<Movie>> GetByTitleGenre(string title, string genre)
        {
            List<Movie> movieList = null;
            if (!string.IsNullOrEmpty(title))
                movieList = _db.Movie.Where(t => t.Title.ToLower().Contains(title.ToLower())).ToList();
            if (!string.IsNullOrEmpty(genre))
            {
                if (movieList == null)
                    movieList = _db.Movie.Where(t => t.Genres.ToLower().Contains(genre.ToLower())).ToList();
                else
                    movieList = movieList.Where(t => t.Genres.ToLower().Contains(genre.ToLower())).ToList();
            }
            if (movieList != null)
                movieList = movieList.OrderBy(t => t.Title).ToList();
            return _dbTrans.GetListResult(movieList);
        }

        public DbResult<List<Movie>> GetByTitleGenreYear(string title, string genre, short? year)
        {
            List<Movie> movieList = null;
            if (!string.IsNullOrEmpty(title))
                movieList = _db.Movie.Where(t => t.Title.ToLower().Contains(title.ToLower())).ToList();
            if (!string.IsNullOrEmpty(genre))
            {
                if (movieList == null)
                    movieList = _db.Movie.Where(t => t.Genres.ToLower().Contains(genre.ToLower())).ToList();
                else
                    movieList = movieList.Where(t => t.Genres.ToLower().Contains(genre.ToLower())).ToList();
            }
            if (year != null)
            {
                if (movieList == null)
                    movieList = _db.Movie.Where(t => t.YearOfRelease == year).ToList();
                else
                    movieList = movieList.Where(t => t.YearOfRelease == year).ToList();
            }
            if (movieList != null)
                movieList = movieList.OrderBy(t => t.Title).ToList();
            return _dbTrans.GetListResult(movieList);
        }

        public DbResult<List<Movie>> GetByTitleYear(string title, short? year)
        {
            List<Movie> movieList = null;
            if (!string.IsNullOrEmpty(title))
                movieList = _db.Movie.Where(t => t.Title.ToLower().Contains(title.ToLower())).ToList();
            if (year != null)
            {
                if (movieList == null)
                    movieList = _db.Movie.Where(t => t.YearOfRelease == year).ToList();
                else
                    movieList = movieList.Where(t => t.YearOfRelease == year).ToList();
            }
            if (movieList != null)
                movieList = movieList.OrderBy(t => t.Title).ToList();
            return _dbTrans.GetListResult(movieList);
        }

        public DbResult<List<Movie>> GetByYear(short year)
        {
            var movieList = _db.Movie.Where(t => t.YearOfRelease == year).OrderBy(t => t.Title).ToList();
            return _dbTrans.GetListResult(movieList);
        }

        public DbResult<List<Movie>> GetFirstNRecords(int n)
        {
            var movieList = _db.Movie.OrderBy(t=>t.AverageRating).ThenBy(t=>t.Title).Take(n).ToList();
            return _dbTrans.GetListResult(movieList);
        }

        public DbResult<List<Movie>> GetLastNRecords(int n)
        {
            var movieList = _db.Movie.OrderByDescending(t => t.AverageRating).ThenBy(t => t.Title).Take(n).ToList();
            return _dbTrans.GetListResult(movieList);
        }

        public DbResult<Movie> GetSingle(int id)
        {
            var entity = _db.Movie.SingleOrDefault(t => t.MovieId == id);
            return _dbTrans.GetSingleResult(entity);
        }

        public DbResult<Movie> Insert(Movie entity)
        {
            _db.Movie.Add(entity);
            DbResult<int> res = _dbTrans.SaveChanges(_db);
            return new DbResult<Movie>
            {
                IsSuccess = res.IsSuccess,
                Message = res.Message,
                Result = entity
            };
        }

        public DbResult<int> Update(Movie entity)
        {
            var entModified = _db.Movie.SingleOrDefault(t => t.MovieId == entity.MovieId);
            entModified.AverageRating = entity.AverageRating;
            entModified.Genres = entity.Genres;
            entModified.RunningTime = entity.RunningTime;
            entModified.Title = entity.Title;
            entModified.YearOfRelease = entity.YearOfRelease;
            return _dbTrans.SaveChanges(_db);
        }
    }
}
