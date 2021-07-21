using NHibernate.Criterion;
using TailoredApps.Rayonnant.Interface.Data;

namespace TailoredApps.Rayonnant.Data.NHibernate
{
    public abstract class BaseNhibernateSearchCriteria<TEntity> : ISearchCriteria<TEntity, DetachedCriteria>
    {
        private DetachedCriteria criteria;

        public BaseNhibernateSearchCriteria() 
        {
            criteria = DetachedCriteria.For<TEntity>();
        }

        public DetachedCriteria Criteria { get { return GetCriteria(criteria); } }

        protected abstract DetachedCriteria GetCriteria(DetachedCriteria detachedCriteria);

    }
}