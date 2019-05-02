using System;
using System.Collections.Generic;
using MoviesWebApi.Common;
using MoviesWebApi.Entity;

namespace MoviesWebApi.Repository.DbConnectionManager
{
    /// <summary>
    /// Db DML Transaction handling object, it can save db changes or get single or list db query results in an organized way in which uses DbResult generic object.
    /// </summary>
    /// <typeparam name="T">Db Entity Type</typeparam>
    public class DbTransaction<T>
    {
        public DbTransaction()
        {
            //normally inject/initialize a static logger member here
        }
        public virtual DbResult<int> SaveChanges(MovieDBEntities db)
        {
            DbResult<int> DMLResult = new DbResult<int>();
            try
            {
                int cnt = db.SaveChanges();
                if (cnt > 0)
                {
                    DMLResult.IsSuccess = true;
                    DMLResult.Message = "Transaction Successful";
                    DMLResult.Result = cnt;
                }
                else
                {
                    DMLResult.IsSuccess = false;
                    DMLResult.Message = "Transaction Failed";
                    DMLResult.Result = 0;
                }
            }
            catch (Exception ex)
            {
                //This is the part where I usually log things
                throw ex;
            }
            return DMLResult;
        }

        public DbResult<T> GetSingleResult(T obj)
        {
            DbResult<T> result = new DbResult<T>();
            try
            {
                if (obj != null)
                {
                    result.IsSuccess = true;
                    result.Message = "Read Successful";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                //HandleAndLogException(ex, obj, ref result, "No Records Found : ");
                throw ex;
            }
            result.Result = obj;
            return result;
        }

        public DbResult<List<T>> GetListResult(List<T> objList)
        {
            DbResult<List<T>> resList = new DbResult<List<T>>();
            try
            {
                if (objList == null || objList.Count == 0)
                {
                    resList.IsSuccess = false;
                    resList.Message = "No Records Found";
                }
                else
                {
                    resList.IsSuccess = true;
                    resList.Message = "Read Successful";
                }
            }
            catch (Exception ex)
            {
                //HandleAndLogException(ex, objList, ref resList, "No Records Found : ");
                throw ex;
            }
            resList.Result = objList;
            return resList;
        }
    }
}
