using System.Collections.Generic;
using System.Linq;
using MoviesWebApi.Repository.Base;
using MoviesWebApi.Entity;
using MoviesWebApi.Repository.DbConnectionManager;
using MoviesWebApi.Common;
using MoviesWebApi.Repository.Interface;

namespace MoviesWebApi.Repository.Repository
{
    public class UserMovieRepository : BaseRepository<UserMovie>, IUserMovieRepository
    {
        public UserMovieRepository(DbTransaction<UserMovie> dbTran = null) : base(dbTran)
        {
        }

        public DbResult<int> Delete(int masterId, int detailId)
        {
            var entity = _db.UserMovie.SingleOrDefault(t => t.UserId == masterId && t.MovieId == detailId);
            if (entity == null)
            {
                return new DbResult<int> { IsSuccess = false, Message = "No entity found to delete", Result = 0 };
            }
            _db.UserMovie.Remove(entity);
            DbResult<int> res = _dbTrans.SaveChanges(_db);
            //after a delete attempt, entity's foreign keys and entity collections are set to null by EF automatically, so we need to refetch the entity in case of an unsuccessful delete in order to preserve data integrity, otherwise related views get null reference exceptions.
            if (res != null && !res.IsSuccess)
            {
                _db.Entry(entity).Reload();
            }
            return res;
        }

        public DbResult<int> DeleteAllByMasterId(int masterId)
        {
            var entityList = _db.UserMovie.Where(t => t.UserId == masterId);
            if (entityList == null)
            {
                return new DbResult<int> { IsSuccess = false, Message = "No entity found to delete", Result = 0 };
            }
            _db.UserMovie.RemoveRange(entityList);
            DbResult<int> res = _dbTrans.SaveChanges(_db);
            //after a delete attempt, entity's foreign keys and entity collections are set to null by EF automatically, so we need to refetch the entity in case of an unsuccessful delete in order to preserve data integrity, otherwise related views get null reference exceptions.
            if (res != null && !res.IsSuccess)
            {
                foreach (var entity in entityList)
                {
                    _db.Entry(entity).Reload();
                }
            }
            return res;
        }

        public DbResult<List<UserMovie>> GetAll()
        {
            var entityList = _db.UserMovie.ToList();
            return _dbTrans.GetListResult(entityList);
        }

        public DbResult<List<UserMovie>> GetAllByMasterId(int masterId)
        {
            var entityList = _db.UserMovie.Where(t => t.UserId == masterId).ToList();
            return _dbTrans.GetListResult(entityList);
        }

        public DbResult<List<UserMovie>> GetAllByMovieId(int movieId)
        {
            var entityList = _db.UserMovie.Where(t => t.MovieId == movieId).ToList();
            return _dbTrans.GetListResult(entityList);
        }

        public DbResult<List<UserMovie>> GetFirstNRecords(int n)
        {
            var entityList = _db.UserMovie.OrderBy(t=>t.UserRating).Take(n).ToList();
            return _dbTrans.GetListResult(entityList);
        }

        public DbResult<List<UserMovie>> GetLastNRecords(int n)
        {
            var entityList = _db.UserMovie.OrderByDescending(t => t.UserRating).Take(n).ToList();
            return _dbTrans.GetListResult(entityList);
        }

        public DbResult<List<UserMovie>> GetLastNRecordsByMasterId(int n, int masterId)
        {
            var entityList = _db.UserMovie.Where(t => t.UserId == masterId).OrderByDescending(t => t.UserRating).ThenBy(t=>t.Movie.Title).Take(n).ToList();
            return _dbTrans.GetListResult(entityList);
        }

        public DbResult<UserMovie> GetSingle(int masterId, int detailId)
        {
            var entity = _db.UserMovie.SingleOrDefault(t => t.UserId == masterId && t.MovieId == detailId);
            return _dbTrans.GetSingleResult(entity);
        }

        public DbResult<UserMovie> Insert(UserMovie entity)
        {
            _db.UserMovie.Add(entity);
            DbResult<int> res = _dbTrans.SaveChanges(_db);
            return new DbResult<UserMovie>
            {
                IsSuccess = res.IsSuccess,
                Message = res.Message,
                Result = entity
            };
        }

        public DbResult<int> Update(UserMovie entity)
        {
            var entModified = _db.UserMovie.SingleOrDefault(t => t.UserId == entity.UserId && t.MovieId == entity.MovieId);
            entModified.UserRating = entity.UserRating;
            return _dbTrans.SaveChanges(_db);
        }
    }
}
