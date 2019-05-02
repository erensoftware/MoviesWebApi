namespace MoviesWebApi.Common
{
    /// <summary>
    /// Result encapsulation wrapper for DB operations
    /// </summary>
    /// <typeparam name="T">Returning Result Object Type</typeparam>
    public class DbResult<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
