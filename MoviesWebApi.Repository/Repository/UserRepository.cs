using System.Collections.Generic;
using System.Linq;
using MoviesWebApi.Repository.Base;
using MoviesWebApi.Entity;
using MoviesWebApi.Repository.DbConnectionManager;
using MoviesWebApi.Common;
using MoviesWebApi.Repository.Interface;

namespace MoviesWebApi.Repository.Repository
{
    public class UserRepository : BaseRepository<User>, IDataRepository<User,int>
    {
        public UserRepository(DbTransaction<User> dbTran = null) : base(dbTran)
        {
        }

        public DbResult<int> Delete(int id)
        {
            var entity = _db.User.SingleOrDefault(t => t.UserId == id);
            if (entity == null)
            {
                return new DbResult<int> { IsSuccess = false, Message = "No entity found to delete", Result = 0 };
            }
            if (entity.UserMovie != null && entity.UserMovie.Count > 0)
            {
                return new DbResult<int> { IsSuccess = false, Message = "There are dependant records defined, cannot delete", Result = 0 };
            }
            _db.User.Remove(entity);
            DbResult<int> res = _dbTrans.SaveChanges(_db);
            //after a delete attempt, entity's foreign keys and entity collections are set to null by EF automatically, so we need to refetch the entity in case of an unsuccessful delete in order to preserve data integrity, otherwise related views get null reference exceptions.
            if (res != null && !res.IsSuccess)
            {
                _db.Entry(entity).Reload();
            }
            return res;
        }

        public DbResult<List<User>> GetAll()
        {
            var userList = _db.User.ToList();
            return _dbTrans.GetListResult(userList);
        }

        public DbResult<List<User>> GetFirstNRecords(int n)
        {
            var userList = _db.User.OrderBy(t => t.UserId).Take(n).ToList();
            return _dbTrans.GetListResult(userList);
        }

        public DbResult<List<User>> GetLastNRecords(int n)
        {
            var userList = _db.User.OrderByDescending(t => t.UserId).Take(n).ToList();
            return _dbTrans.GetListResult(userList);
        }

        public DbResult<User> GetSingle(int id)
        {
            var entity = _db.User.SingleOrDefault(t => t.UserId == id);
            return _dbTrans.GetSingleResult(entity);
        }

        public DbResult<User> Insert(User entity)
        {
            _db.User.Add(entity);
            DbResult<int> res = _dbTrans.SaveChanges(_db);
            return new DbResult<User>
            {
                IsSuccess = res.IsSuccess,
                Message = res.Message,
                Result = entity
            };
        }

        public DbResult<int> Update(User entity)
        {
            var entModified = _db.User.SingleOrDefault(t => t.UserId == entity.UserId);
            entModified.AddressLine1 = entity.AddressLine1;
            entModified.AddressLine2 = entity.AddressLine2;
            entModified.City = entity.City;
            entModified.Country = entity.Country;
            entModified.Email = entity.Email;
            entModified.FirstName = entity.FirstName;
            entModified.LastName = entity.LastName;
            entModified.Password = entity.Password;
            entModified.PostalCode = entity.PostalCode;
            entModified.Username = entity.Username;

            return _dbTrans.SaveChanges(_db);
        }
    }
}
