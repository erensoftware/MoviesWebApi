using MoviesWebApi.Common;
using MoviesWebApi.Entity;
using MoviesWebApi.Models;
using MoviesWebApi.Repository.Interface;
using MoviesWebApi.Repository.Repository;
using System;
using System.Web.Http;

namespace MoviesWebApi.Controllers
{
    public class ApiDController : ApiController
    {
        private static IMovieRepository _movieRep;
        private static IUserMovieRepository _userMovieRep;
        public ApiDController()
        {
            _movieRep = new MovieRepository();
            _userMovieRep = new UserMovieRepository();
        }
        public ApiDController(IMovieRepository movieRep, IUserMovieRepository userMovieRep)
        {
            _movieRep = movieRep;
            _userMovieRep = userMovieRep;
        }

        public IHttpActionResult Put(UserMovieModel data)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            if(data.UserId < 0)
                return BadRequest("Invalid UserId");

            if (data.MovieId < 0)
                return BadRequest("Invalid MovieId");

            if (data.UserRating < 0)
                return BadRequest("Invalid Rating");

            DbResult<UserMovie> res = _userMovieRep.GetSingle(data.UserId, data.MovieId);
            if (!res.IsSuccess)
            {
                DbResult<UserMovie> resIns = _userMovieRep.Insert(new UserMovie
                {
                    MovieId = data.MovieId,
                    UserId = data.UserId,
                    UserRating = data.UserRating
                });
                if (!resIns.IsSuccess)
                    return BadRequest("UserId or MovieId does not exist in the system");
            }
            else
            {
                DbResult<int> resUpd = _userMovieRep.Update(new UserMovie
                {
                    MovieId = data.MovieId,
                    UserId = data.UserId,
                    UserRating = data.UserRating
                });
                if (!resUpd.IsSuccess)
                    return InternalServerError(new System.Exception(resUpd.Message));
            }

            var ratings = _userMovieRep.GetAllByMovieId(data.MovieId).Result;
            decimal totalrating = 0;
            foreach (var item in ratings)
            {
                if (item.UserRating.HasValue)
                    totalrating += item.UserRating.Value;
            }
            decimal average = totalrating / ratings.Count;

            Movie entMovie = _movieRep.GetSingle(data.MovieId).Result;
            entMovie.AverageRating = average;
            DbResult<int> resMov = _movieRep.Update(entMovie);
            if (!resMov.IsSuccess)
                return InternalServerError(new System.Exception(resMov.Message));

            var returnValue = new MovieApiModel
            {
                id = entMovie.MovieId,
                title = entMovie.Title,
                yearOfRelease = entMovie.YearOfRelease,
                runningTime = entMovie.RunningTime,
                genres = entMovie.Genres,
                averageRating = Math.Round(average * 2, MidpointRounding.AwayFromZero) / 2
            };

            return Ok(returnValue);
        }

    }
}
