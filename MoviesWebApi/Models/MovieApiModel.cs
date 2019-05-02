namespace MoviesWebApi.Models
{
    public class MovieApiModel
    {
        //the prop name cases are done exactly seen on the API spec (pg 3)
        public int id { get; set; }
        public string title { get; set; }
        public short? yearOfRelease { get; set; }
        public short? runningTime { get; set; }
        public string genres { get; set; }
        public decimal? averageRating { get; set; }
    }
}