using NHibernate;
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
            ISession session = NHibernateHelper.GetSession();
            ITransaction transaction = session.BeginTransaction();

            session.Save(obj);
            transaction.Commit();

            NHibernateHelper.CloseSession();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public T GetObject<T>(int id)
        {
            ISession session = NHibernateHelper.GetSession();
            ITransaction transaction = session.BeginTransaction();

            object obj = session.Get<T>(id);
            transaction.Commit();

            NHibernateHelper.CloseSession();

            return (T)obj;
        }
    }
}
