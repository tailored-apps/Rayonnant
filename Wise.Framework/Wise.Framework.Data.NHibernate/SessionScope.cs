using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Context;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.Data.NHibernate
{
    public class SessionScope : IDisposable
    {
        private ISessionFactory sessionFactory;
        public SessionScope(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
            if (!CurrentSessionContext.HasBind(sessionFactory))
                CurrentSessionContext.Bind(sessionFactory.OpenSession());
        }

        private void DisposeCurrentSession()
        {
            ISession currentSession = CurrentSessionContext.Unbind(sessionFactory);

            currentSession.Close();
            currentSession.Dispose();
        }
        public void Dispose()
        {
            DisposeCurrentSession();
        }
    }
}
