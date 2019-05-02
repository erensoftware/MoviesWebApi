using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesWebApi.Controllers;
using System.Net.Http;
using System.Web.Http;
using MoviesWebApi.Models;
using MoviesWebApi.Common;
using MoviesWebApi.Entity;
using MoviesWebApi.Repository.Interface;
using MoviesWebApi.Repository.Repository;
using Moq;
using MoviesWebApi.Repository.DbConnectionManager;
using System.Web.Http.Results;

namespace MoviesWebApi.Tests
{
    [TestClass]
    public class ApiTestD
    {
        [TestMethod]
        public void Put()
        {
            // Arrange
            var mockDbTransMovie = new Mock<DbTransaction<Movie>>();
            var mockDbTransUserMovie = new Mock<DbTransaction<UserMovie>>();
            var dbContext = DbConnection.GetSQLConnection();
            mockDbTransMovie.Setup(t => t.SaveChanges(dbContext)).Callback(() => { }).Returns(new DbResult<int> { IsSuccess = true, Message = "Mock Success", Result = 1 });
            mockDbTransUserMovie.Setup(t => t.SaveChanges(dbContext)).Callback(() => { }).Returns(new DbResult<int> { IsSuccess = true, Message = "Mock Success", Result = 1 });
            IMovieRepository repMov = new MovieRepository(mockDbTransMovie.Object);
            IUserMovieRepository repUserMov = new UserMovieRepository(mockDbTransUserMovie.Object);
            ApiDController controller = new ApiDController(repMov, repUserMov);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //Act
            IHttpActionResult actionResult = controller.Put(new UserMovieModel { MovieId = 1, UserId = 1, UserRating = 4 });

            var response = actionResult.ExecuteAsync(new System.Threading.CancellationToken());
            response.Wait();

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<MovieApiModel>));
            Assert.AreEqual(1, response.Result.Content.ReadAsAsync<MovieApiModel>().Result.id);
            Assert.AreEqual((decimal)3.5, response.Result.Content.ReadAsAsync<MovieApiModel>().Result.averageRating);
        }
    }
}
