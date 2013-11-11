using System;
using System.Collections;
using NHibernate;
using NHibernate.Criterion;

namespace Wise.Framework.DataAccessLayer
{
    public class CountQuery : RepositoryQueryBase ,IRepositoryQuery
    {
        private readonly Type targetType;
        private readonly DetachedCriteria detachedCriteria;
        private readonly ICriterion[] criterias;

        public CountQuery(Type targetType)
        {

            this.targetType = targetType;
        }
        public CountQuery(Type targetType, DetachedCriteria detachedCriteria)
        {
            this.detachedCriteria = detachedCriteria;
            this.targetType = targetType;
        }

        public CountQuery(Type targetType, ICriterion[] criterias)
        {
            this.criterias = criterias;
            this.targetType = targetType;
        }


        public IEnumerable Enumerate(ISession session)
        {
            throw new NotImplementedException();
        }

        public object Execute(ISession session)
        {
            if (this.detachedCriteria == null && this.criterias == null)
            {
                ICriteria criteria = session.CreateCriteria(targetType);
                criteria.SetProjection(new IProjection[] { Projections.RowCount() });
                return Convert.ToInt32(criteria.UniqueResult());
            }
            if (this.detachedCriteria != null)
            {
                ICriteria executableCriteria = this.detachedCriteria.GetExecutableCriteria(session);
                executableCriteria.SetProjection(new IProjection[] { Projections.RowCount() });
                int num = Convert.ToInt32(executableCriteria.UniqueResult());
                executableCriteria.SetProjection(new IProjection[1]);
                return num;
            }
            
            if (this.criterias != null)
            {
                ICriteria criteria = session.CreateCriteria(targetType);
                AddCriterionToCriteria(criteria, this.criterias);
                criteria.SetProjection(new IProjection[] { Projections.RowCount() });
                return Convert.ToInt32(criteria.UniqueResult());
            }
            return default(int);
        }

        public Type RootType { get; private set; }
    }
}