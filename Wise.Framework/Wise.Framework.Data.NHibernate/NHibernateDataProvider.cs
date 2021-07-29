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
        private readonly ISession session;
        private readonly ISessionFactory sessionFactory;

        public NHibernateDataProvider(ISession session, ISessionFactory sessionFactory)
        {
            this.session = session;
            this.sessionFactory = sessionFactory;
        }

        protected ISession Session
        {
            get { return session; }
        }

        public TEntity Get<TEntity>(object id) where TEntity : class
        {
            return Session.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return Session.CreateCriteria<TEntity>().List<TEntity>();
        }

        public TEntity GetById<TKey, TEntity>(TKey id) where TEntity : class
        {
            return Session.Get<TEntity>(id);
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class
        {
            Session.Save(entity);
            Session.Flush();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            Session.Delete(entity);
            Session.Flush();
        }

        public void DeleteById<TKey, TEntity>(TKey id) where TEntity : class
        {
            Session.Delete(string.Format("from {0} where {1} = {2}", typeof (TEntity),
                sessionFactory.GetClassMetadata(typeof (TEntity)).IdentifierPropertyName, id));
            Session.Flush();
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

        public void BeginTransaction()
        {
            session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            session.Transaction.Commit();
            session.Flush();
        }

        public void Rollback()
        {
            session.Transaction.Rollback();
            session.Flush();
        }
    }
}