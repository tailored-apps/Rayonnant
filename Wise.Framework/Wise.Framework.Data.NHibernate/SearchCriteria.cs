using NHibernate.Criterion;
using Wise.Framework.Interface.Data;

namespace Wise.Framework.Data.NHibernate
{
    public abstract class BaseSearchCriteria<TEntity> : DetachedCriteria, ISearchCriteria<TEntity, DetachedCriteria>
    {
        public BaseSearchCriteria() : base(typeof (TEntity))
        {
            Criteria = this;
        }

        public DetachedCriteria Criteria { get; private set; }
    }
}