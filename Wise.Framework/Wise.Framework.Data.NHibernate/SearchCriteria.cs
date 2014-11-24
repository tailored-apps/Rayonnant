using NHibernate.Criterion;
using Wise.Framework.Interface.Data;

namespace Wise.Framework.Data.NHibernate
{
    public abstract class BaseNhibernateSearchCriteria<TEntity> : DetachedCriteria, ISearchCriteria<TEntity, DetachedCriteria>
    {
        public BaseNhibernateSearchCriteria() : base(typeof (TEntity))
        {
            Criteria = this;
        }

        public DetachedCriteria Criteria { get; private set; }
    }
}