namespace MoviesWebApi.Models
{
    public class UserMovieModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public short UserRating { get; set; }
    }
}