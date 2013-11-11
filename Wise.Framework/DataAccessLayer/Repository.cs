using System;
using System.Collections;
using System.Collections.Generic;
using Iesi.Collections;
using NHibernate;
using NHibernate.Criterion;

namespace Wise.Framework.DataAccessLayer
{
    public class Repository : RepositoryGenericBase, IRepository
    {
        public Repository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {

        }


        public void Save<T>(T obj) where T : EntityBase
        {
            base.Save(obj);
        }

        public void Delete<T>(T obj) where T : EntityBase
        {
            base.Delete(obj);
        }

        public IEnumerable<T> GetAll<T>() where T : EntityBase
        {
            return base.FindAll<T>();
        }

        public T GetById<T>(object id) where T : EntityBase
        {
            return base.FindByPrimaryKey<T>(id);
        }

        public IEnumerable<T> GetBySearchCriteria<T>(ICriterion searchCriteria) where T : EntityBase
        {
            return base.FindAll<T>(searchCriteria);
        }
    }
}
