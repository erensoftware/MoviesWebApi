using System;
using MoviesWebApi.Entity;

namespace MoviesWebApi.Repository.DbConnectionManager
{
    /// <summary>
    /// Static Singleton Db Connection Manager class.
    /// </summary>
    public static class DbConnection
    {
        //lazy is by definition thread-safe: implicitly uses LazyThreadSafetyMode.ExecutionAndPublication /*.Net >= 4*/
        private static readonly Lazy<MovieDBEntities> _db
            = new Lazy<MovieDBEntities>(() => new MovieDBEntities());

        public static MovieDBEntities GetSQLConnection()
        {
            return _db.Value;
        }
    }
}
