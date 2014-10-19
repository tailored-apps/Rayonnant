using System;
using NHibernate;
using NHibernate.Context;

namespace Wise.Framework.Data.NHibernate
{
    public class SessionScope : IDisposable
    {
        private readonly ISessionFactory sessionFactory;

        public SessionScope(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                ISession sess = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(sess);
            }
        }

        public ISession Session
        {
            get { return sessionFactory.GetCurrentSession(); }
        }

        public void Dispose()
        {
            DisposeCurrentSession();
        }

        private void DisposeCurrentSession()
        {
            ISession currentSession = CurrentSessionContext.Unbind(sessionFactory);
            currentSession.Flush();
            currentSession.Close();
            currentSession.Dispose();
        }
    }
}