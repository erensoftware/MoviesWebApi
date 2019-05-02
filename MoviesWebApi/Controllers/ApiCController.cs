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
    public class ApiCController : ApiController
    {
        private static IMovieRepository _movieRep;
        private static IUserMovieRepository _userMovieRep;
        public ApiCController()
        {
            _movieRep = new MovieRepository();
            _userMovieRep = new UserMovieRepository();
        }
        public ApiCController(IMovieRepository movieRep, IUserMovieRepository userMovieRep)
        {
            _movieRep = movieRep;
            _userMovieRep = userMovieRep;
        }

        [Route("api/ApiC/{userId}/{topn}")]
        public IHttpActionResult Get(int? userId, int? topn)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            int? n = topn;
            if (topn == null)
                n = 5;

            DbResult<List<UserMovie>> res = _userMovieRep.GetLastNRecordsByMasterId((int)n, (int)userId);

            if (!res.IsSuccess)
                return NotFound();

            IList<MovieApiModel> movieList =
                res.Result.Select(s => new MovieApiModel
                {
                    id = s.MovieId,
                    title = s.Movie.Title,
                    yearOfRelease = s.Movie.YearOfRelease,
                    runningTime = s.Movie.RunningTime,
                    genres = s.Movie.Genres,
                    averageRating = s.Movie.AverageRating
                }).ToList();

            if (movieList.Count == 0)
            {
                return NotFound();
            }

            return Ok(movieList);
        }
    }
}
