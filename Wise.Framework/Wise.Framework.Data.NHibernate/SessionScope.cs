using System;
using NHibernate;
using NHibernate.Context;

namespace Wise.Framework.Data.NHibernate
{
    public class SessionScope : IDisposable
    {
        private readonly ISession sessionFactory;

        public SessionScope(ISession sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public ISession Session
        {
            get { return sessionFactory; }
        }

        public void Dispose()
        {
        }

    }
}