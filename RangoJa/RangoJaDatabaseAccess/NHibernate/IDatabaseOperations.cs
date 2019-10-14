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
        /// Gets a object from the database.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="id">Object id.</param>
        /// <param name="type">Object type.</param>
        /// <returns></returns>
        T GetObject<T>(int id);
    }
}
