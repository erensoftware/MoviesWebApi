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
    public class ApiBController : ApiController
    {
        private static IMovieRepository _movieRep;
        public ApiBController()
        {
            _movieRep = new MovieRepository();
        }
        public ApiBController(IMovieRepository movieRep)
        {
            _movieRep = movieRep;
        }

        [Route("api/ApiB/{topn}")]
        public IHttpActionResult Get(int? topn)
        {
            int? n = topn;
            if (n == null)
            {
                n = 5;
            }

            DbResult<List<Movie>> res = _movieRep.GetLastNRecords((int)n);

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
