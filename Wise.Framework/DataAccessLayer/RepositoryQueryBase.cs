using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Wise.Framework.DataAccessLayer
{
    public abstract class RepositoryQueryBase
    {

        protected void AddCriterionToCriteria(ICriteria criteria, ICriterion[] criterias)
        {
            criterias.ForEach(x => criteria.Add(x));
        }
    }
}
