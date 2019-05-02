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
    public class ApiTestB
    {
        [TestMethod]
        public void GetTop5OfAllMovies()
        {
            // Arrange
            ApiBController controller = new ApiBController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Get(5).ExecuteAsync(new System.Threading.CancellationToken());
            response.Wait();


            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            Assert.AreEqual(5, response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.Count());
            Assert.AreEqual(response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.Max(t=>t.averageRating), response.Result.Content.ReadAsAsync<IEnumerable<MovieApiModel>>().Result.ToArray()[0].averageRating);
        }
    }
}
