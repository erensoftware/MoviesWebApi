using MoviesWebApi.Common;
using MoviesWebApi.Entity;
using MoviesWebApi.Models;
using MoviesWebApi.Repository.Interface;
using MoviesWebApi.Repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MoviesWebApi.Controllers
{
    public class ApiAController : ApiController
    {
        private static IMovieRepository _movieRep;
        public ApiAController()
        {
            _movieRep = new MovieRepository();
        }
        public ApiAController(IMovieRepository movieRep)
        {
            _movieRep = movieRep;
        }
        [Route("api/ApiA/GetByYear/{year}")]
        public IHttpActionResult GetByYear(short? year)
        {

            if (year == null)
            {
                return BadRequest();
            }

            DbResult<List<Movie>> res = _movieRep.GetByYear((short)year);

            if (!res.IsSuccess)
                return NotFound();

            IList<MovieApiModel> movieList =
                res.Result.Select(s => new MovieApiModel
                {
                    id = s.MovieId,
                    title = s.Title,
                    yearOfRelease = s.YearOfRelease,
                    runningTime = s.RunningTime,
                    genres = s.Genres,
                    averageRating = s.AverageRating
                }).ToList();

            if (movieList.Count == 0)
            {
                return NotFound();
            }

            return Ok(movieList);
        }

        [Route("api/ApiA/GetByTitle/{title}")]
        public IHttpActionResult GetByTitle(string title)
        {

            if (string.IsNullOrEmpty(title))
            {
                return BadRequest();
            }

            DbResult<List<Movie>> res = _movieRep.GetByTitle(title);

            if (!res.IsSuccess)
                return NotFound();

            IList<MovieApiModel> movieList =
                res.Result.Select(s => new MovieApiModel
                {
                    id = s.MovieId,
                    title = s.Title,
                    yearOfRelease = s.YearOfRelease,
                    runningTime = s.RunningTime,
                    genres = s.Genres,
                    averageRating = s.AverageRating
                }).ToList();

            if (movieList.Count == 0)
            {
                return NotFound();
            }

            return Ok(movieList);
        }

        [Route("api/ApiA/GetByTitle/{genre}")]
        public IHttpActionResult GetByGenre(string genre)
        {

            if (string.IsNullOrEmpty(genre))
            {
                return BadRequest();
            }

            DbResult<List<Movie>> res = _movieRep.GetByGenre(genre);

            if (!res.IsSuccess)
                return NotFound();

            IList<MovieApiModel> movieList =
                res.Result.Select(s => new MovieApiModel
                {
                    id = s.MovieId,
                    title = s.Title,
                    yearOfRelease = s.YearOfRelease,
                    runningTime = s.RunningTime,
                    genres = s.Genres,
                    averageRating = s.AverageRating
                }).ToList();

            if (movieList.Count == 0)
            {
                return NotFound();
            }

            return Ok(movieList);
        }
        [Route("api/ApiA/GetByAll/{title}/{year}/{genre}")]
        public IHttpActionResult GetByAll(string title, short? year, string genre)
        {

            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(genre) && year == null)
            {
                return BadRequest();
            }

            DbResult<List<Movie>> res = _movieRep.GetByTitleGenreYear(title, genre, year);

            if (!res.IsSuccess)
                return NotFound();

            IList<MovieApiModel> movieList =
                res.Result.Select(s => new MovieApiModel
                {
                    id = s.MovieId,
                    title = s.Title,
                    yearOfRelease = s.YearOfRelease,
                    runningTime = s.RunningTime,
                    genres = s.Genres,
                    averageRating = s.AverageRating
                }).ToList();

            if (movieList.Count == 0)
            {
                return NotFound();
            }

            return Ok(movieList);
        }

        [Route("api/ApiA/GetByTitleGenre/{title}/{genre}")]
        public IHttpActionResult GetByTitleGenre(string title, string genre)
        {

            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(genre))
            {
                return BadRequest();
            }

            DbResult<List<Movie>> res = _movieRep.GetByTitleGenre(title, genre);

            if (!res.IsSuccess)
                return NotFound();

            IList<MovieApiModel> movieList =
                res.Result.Select(s => new MovieApiModel
                {
                    id = s.MovieId,
                    title = s.Title,
                    yearOfRelease = s.YearOfRelease,
                    runningTime = s.RunningTime,
                    genres = s.Genres,
                    averageRating = s.AverageRating
                }).ToList();

            if (movieList.Count == 0)
            {
                return NotFound();
            }

            return Ok(movieList);
        }

        [Route("api/ApiA/GetByTitleYear/{title}/{year}")]
        public IHttpActionResult GetByTitleYear(string title, short? year)
        {

            if (string.IsNullOrEmpty(title) && year == null)
            {
                return BadRequest();
            }

            DbResult<List<Movie>> res = _movieRep.GetByTitleYear(title, year);

            if (!res.IsSuccess)
                return NotFound();

            IList<MovieApiModel> movieList =
                res.Result.Select(s => new MovieApiModel
                {
                    id = s.MovieId,
                    title = s.Title,
                    yearOfRelease = s.YearOfRelease,
                    runningTime = s.RunningTime,
                    genres = s.Genres,
                    averageRating = s.AverageRating
                }).ToList();

            if (movieList.Count == 0)
            {
                return NotFound();
            }

            return Ok(movieList);
        }

        [Route("api/ApiA/GetByGenreYear/{genre}/{year}")]
        public IHttpActionResult GetByGenreYear(string genre, short? year)
        {

            if (string.IsNullOrEmpty(genre) && year == null)
            {
                return BadRequest();
            }

            DbResult<List<Movie>> res = _movieRep.GetByGenreYear(genre, year);

            if (!res.IsSuccess)
                return NotFound();

            IList<MovieApiModel> movieList =
                res.Result.Select(s => new MovieApiModel
                {
                    id = s.MovieId,
                    title = s.Title,
                    yearOfRelease = s.YearOfRelease,
                    runningTime = s.RunningTime,
                    genres = s.Genres,
                    averageRating = s.AverageRating
                }).ToList();

            if (movieList.Count == 0)
            {
                return NotFound();
            }

            return Ok(movieList);
        }
    }
}
