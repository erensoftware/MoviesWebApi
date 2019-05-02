using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesWebApi.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using MoviesWebApi.Models;
using System.Collections.Generic;

namespace MoviesWebApi.Tests
{
    [TestClass]
    public class ApiTestA
    {
        [TestMethod]
        public void GetByTitle()
        {
            // Arrange
            ApiAController controller = new ApiAController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.GetByTitle("aveng").ExecuteAsync(new System.Threading.CancellationToken());
            response.Wait();

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            Assert.AreNotEqual(0, response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.Count());
        }
        [TestMethod]
        public void GetByGenre()
        {
            // Arrange
            ApiAController controller = new ApiAController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.GetByGenre("Action").ExecuteAsync(new System.Threading.CancellationToken());
            response.Wait();

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            Assert.AreNotEqual(0, response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.Count());
        }

        [TestMethod]
        public void GetByGenres()
        {
            // Arrange
            ApiAController controller = new ApiAController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.GetByGenre("Action, Thriller").ExecuteAsync(new System.Threading.CancellationToken());
            response.Wait();

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            Assert.AreNotEqual(0, response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.Count());
        }

        [TestMethod]
        public void GetByYear()
        {
            // Arrange
            ApiAController controller = new ApiAController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.GetByYear(1988).ExecuteAsync(new System.Threading.CancellationToken());
            response.Wait();

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            Assert.AreNotEqual(0, response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.Count());
        }

        [TestMethod]
        public void GetByTitleGenreYear()
        {
            // Arrange
            ApiAController controller = new ApiAController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.GetByAll("Ave", 2018, "Action").ExecuteAsync(new System.Threading.CancellationToken());
            response.Wait();

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            Assert.AreNotEqual(0, response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.Count());
        }

        [TestMethod]
        public void GetFailWithNoParams()
        {
            // Arrange
            ApiAController controller = new ApiAController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.GetByAll(null, null, null).ExecuteAsync(new System.Threading.CancellationToken());
            response.Wait();

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreNotEqual(true, response.Result.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.Result.StatusCode);
        }

        [TestMethod]
        public void GetFailWithNoData()
        {
            // Arrange
            ApiAController controller = new ApiAController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.GetByTitle("ozkan").ExecuteAsync(new System.Threading.CancellationToken());
            response.Wait();

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreNotEqual(true, response.Result.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, response.Result.StatusCode);
        }
    }
}
