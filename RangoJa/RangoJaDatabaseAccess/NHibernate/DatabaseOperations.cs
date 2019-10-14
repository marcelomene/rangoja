using NHibernate;
using RangoJaDatabaseAccess.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangoJaDatabaseAccess.NHibernate
{
    public class DatabaseOperations : IDatabaseOperations
    {
        public void SaveObject<T>(T obj)
        {
            using (ISession session = NHibernateHelper.GetSession())
            {
                ITransaction transaction = session.BeginTransaction();
                session.Save(obj);
                transaction.Commit();
            }
        }

        public async void SaveObjectAsync<T>(T obj)
        {
            using (ISession session = NHibernateHelper.GetSession())
            {
                ITransaction transaction = session.BeginTransaction();
                await session.SaveAsync(obj);
                await transaction.CommitAsync();
            }
        }

        /// <summary>
        /// Gets a object from the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public T GetObject<T>(int id)
        {
            using (ISession session = NHibernateHelper.GetSession())
            {
                object obj = session.Get<T>(id);
                return (T)obj;
            }
        }

        public async Task<T> GetObjectAsync<T>(int id)
        { 
            using (ISession session = NHibernateHelper.GetSession())
            {
                object obj = await session.GetAsync<T>(id);
                return (T)obj;
            }
        }

        /// <summary>
        /// Gets all objects of the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetAllObjects<T>() where T : class
        {
            using (ISession session = NHibernateHelper.GetSession())
            {
                var objs = session.CreateCriteria<T>().List<T>();
                return objs;
            }
        }

        public async Task<IList<T>> GetAllObjectsAsync<T>() where T : class
        {
            using (ISession session = NHibernateHelper.GetSession())
            {
                var objs = await session.CreateCriteria<T>().ListAsync<T>();
                return objs;
            }
        }
    }
}
