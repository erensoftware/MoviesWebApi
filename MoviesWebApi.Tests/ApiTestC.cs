using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesWebApi.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using MoviesWebApi.Models;
using System.Collections.Generic;
using MoviesWebApi.Common;
using MoviesWebApi.Entity;
using MoviesWebApi.Repository.Interface;
using MoviesWebApi.Repository.Repository;

namespace MoviesWebApi.Tests
{
    [TestClass]
    public class ApiTestC
    {
        [TestMethod]
        public void GetTop5OfUser1Movies()
        {
            // Arrange
            ApiCController controller = new ApiCController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            IUserMovieRepository rep = new UserMovieRepository();

            // Act
            var response = controller.Get(1,5).ExecuteAsync(new System.Threading.CancellationToken());
            response.Wait();

            DbResult<List<UserMovie>> res = rep.GetAllByMasterId(1);
            UserMovie[] orderedList = null;
            if (res.IsSuccess)
            {
                orderedList = res.Result.OrderByDescending(t => t.UserRating).ThenBy(t => t.Movie.Title).Take(5).ToArray();
            }

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            Assert.AreEqual(5, response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.Count());
            Assert.AreEqual(orderedList[0].MovieId, response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.ToArray()[0].id);
            Assert.AreEqual(orderedList[4].MovieId, response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.ToArray()[4].id);
        }
    }
}
