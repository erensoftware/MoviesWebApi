using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MoviesWebApi.Common;
using MoviesWebApi.Entity;
using MoviesWebApi.Repository.DbConnectionManager;

namespace MoviesWebApi.Repository.Base
{
    /// <summary>
    /// Base Repository
    /// </summary>
    /// <typeparam name="T">Type of Entity</typeparam>
    public class BaseRepository<T>
    {
        protected static MovieDBEntities _db;
        //protected static ILogger _logger;
        protected DbTransaction<T> _dbTrans = null;
        protected BaseRepository
            (
            //ILogger logger, 
            DbTransaction<T> dbTran)
        {
            _db = DbConnection.GetSQLConnection();//the singleton db conn
            //_logger = logger;
            if (dbTran != null)
                _dbTrans = dbTran;
            else
                //_dbTrans = new DbTransaction<T>(_logger);
                _dbTrans = new DbTransaction<T>();
        }
    }
}
