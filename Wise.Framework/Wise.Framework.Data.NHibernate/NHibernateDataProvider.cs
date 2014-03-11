using NHibernate;
using Wise.Framework.Data.Interface;

namespace Wise.Framework.Data.NHibernate
{
    public class NHibernateDataProvider : IDataProvider
    {
        private readonly ISessionFactory sessionFactory;
        public NHibernateDataProvider(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        protected ISession Session
        {
            get
            {
                return sessionFactory.GetCurrentSession();
            }
        }

        public TEntity Get<TEntity>(object id)
        {
            return Session.Get<TEntity>(id);
        }

        public TEntity GetById<TKey, TEntity>(TKey id)
        {
            return Session.Get<TEntity>((TKey)id);
        }

        public void Save<TEntity>(TEntity entity)
        {
             Session.Save(entity);
        }

        public void Delete<TEntity>(TEntity entity)
        {
            Session.Delete(entity);
        }

        public void DeleteById<TKey, TEntity>(TKey id)
        {
            Session.Delete(string.Format("from {0} where {1} = {2}", typeof(TEntity),
                sessionFactory.GetClassMetadata(typeof (TEntity)).IdentifierPropertyName, id));

        }
    }
}
