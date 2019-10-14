using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangoJaDatabaseAccess.NHibernate
{
    interface IDatabaseOperations
    {
        /// <summary>
        /// Saves a object in the database.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="obj">Object.</param>
        void SaveObject<T>(T obj);
        
        /// <summary>
        /// Saves a object in the database asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void SaveObjectAsync<T>(T obj);

        /// <summary>
        /// Gets a object from the database.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="id">Object id.</param>
        /// <param name="type">Object type.</param>
        /// <returns></returns>
        T GetObject<T>(int id);

        /// <summary>
        /// Gets a object from the database asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetObjectAsync<T>(int id);

        /// <summary>
        /// Gets all objects of the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> GetAllObjects<T>() where T : class;

        /// <summary>
        /// Gets all objects of the given type asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IList<T>> GetAllObjectsAsync<T>() where T : class;
    }
}
