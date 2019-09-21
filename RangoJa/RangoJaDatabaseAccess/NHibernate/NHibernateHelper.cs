using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System.Reflection;

namespace RangoJaDatabaseAccess.NHibernate
{
    public sealed class NHibernateHelper
    {
        private static readonly ISessionFactory sessionFactory;
        private const string currentSession = "nhibernate.current_session";

        static NHibernateHelper()
        {
            try
            {
                sessionFactory = new Configuration().Configure()
                    .AddAssembly("RangoJaDatabaseAccess").BuildSessionFactory();
            }
            catch
            {
                throw;
            }
        }

        public static ISession GetSession()
        {
            if (!CurrentSessionContext.HasBind(sessionFactory))
                CurrentSessionContext.Bind(sessionFactory.OpenSession());
            return sessionFactory.GetCurrentSession();
        }

        public static void CloseSession()
        {
            sessionFactory.GetCurrentSession()?.Close();
            sessionFactory.GetCurrentSession()?.Dispose();
        }
            
        public static void CloseSessionFactory()
        {
            if (sessionFactory != null || !sessionFactory.IsClosed)
                sessionFactory.Close();
        }
    }
}
