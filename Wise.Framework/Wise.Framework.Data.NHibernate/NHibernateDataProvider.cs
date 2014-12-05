using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Remotion.Linq.Utilities;
using Wise.Framework.Data.Interface;
using Wise.Framework.Interface.Data;

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
            get { return sessionFactory.GetCurrentSession(); }
        }

        public TEntity Get<TEntity>(object id) where TEntity : class
        {
            return Session.Get<TEntity>(id);
        }

        public TEntity GetById<TKey, TEntity>(TKey id) where TEntity : class
        {
            return Session.Get<TEntity>(id);
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class
        {
            Session.Save(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            Session.Delete(entity);
        }

        public void DeleteById<TKey, TEntity>(TKey id) where TEntity : class
        {
            Session.Delete(string.Format("from {0} where {1} = {2}", typeof (TEntity),
                sessionFactory.GetClassMetadata(typeof (TEntity)).IdentifierPropertyName, id));
        }


        public IEnumerable<TEntity> GetBySearchCriteria<TEntity, TProvider>(ISearchCriteria<TEntity, TProvider> searchCriteria)
            where TEntity : class
            where TProvider : class
        {
            if (searchCriteria == null)
                throw new ArgumentNullException("searchCriteria");
            if (typeof(TProvider) != typeof(DetachedCriteria))
                throw new ArgumentTypeException("Type of searchCriteria is not matching", typeof(DetachedCriteria), typeof(TProvider));

            var crit = searchCriteria as ISearchCriteria<TEntity, DetachedCriteria>;
            if (crit != null)
            {
                return crit.Criteria.GetExecutableCriteria(Session).List<TEntity>();
            }

            throw new ArgumentTypeException("searchCriteria", typeof (ISearchCriteria<TEntity, DetachedCriteria>),
                typeof (ISearchCriteria<TEntity, TProvider>));
        }
    }
}