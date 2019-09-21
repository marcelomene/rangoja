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
        public void Save<T>(T obj)
        {
            ISession session = NHibernateHelper.GetSession();
            ITransaction transaction = session.BeginTransaction();

            session.Save(obj);
            transaction.Commit();

            NHibernateHelper.CloseSession();
        }
    }
}
