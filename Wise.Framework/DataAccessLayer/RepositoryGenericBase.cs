using System.Collections;
using NHibernate;
using NHibernate.Criterion;

namespace Wise.Framework.DataAccessLayer
{
    public abstract class RepositoryGenericBase : RepositoryBase
    {
        protected RepositoryGenericBase(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {

        }


        #region Count
        protected internal int Count<T>()
        {
            return Count(typeof(T));
        }

        protected internal int Count<T>(params ICriterion[] criteria)
        {
            return Count(typeof(T), criteria);
        }

        protected internal int Count<T>(DetachedCriteria detachedCriteria)
        {
            return Count(typeof(T), detachedCriteria);
        }

        #endregion Count

        protected internal void Create<T>(T instance)
        {
            base.Create(instance);
        }

        protected internal void Delete<T>(T instance)
        {
            base.Delete(instance);
        }

        #region DeleteAll
        public int DeleteAll<T>(IEnumerable pkValues)
        {
            return DeleteAll(typeof(T), pkValues);
        }

        public void DeleteAll<T>(string where)
        {
            DeleteAll(typeof(T), where);
        }

        #endregion DeleteAll

        protected internal R ExecuteQuery2<R>(IRepositoryQuery query)
        {
            object obj2 = ExecuteQuery(query);
            if (obj2 == null)
            {
                return default(R);
            }
            if (!typeof(R).IsAssignableFrom(obj2.GetType()))
            {
                throw new QueryException(string.Format("Problem: A query was executed requesting {0} as result, but the query returned an object of type {1}.", typeof(R).Name, obj2.GetType().Name));
            }
            return (R)obj2;
        }

        #region Exists
        public bool Exists<T>()
        {
            return Exists(typeof(T));
        }

        public bool Exists<T>(params ICriterion[] criteria)
        {
            return Exists(typeof(T), criteria);
        }

        public bool Exists<T>(DetachedCriteria detachedCriteria)
        {
            return Exists(typeof(T), detachedCriteria);
        }

        public bool Exists<T>(IDetachedQuery detachedQuery)
        {
            return Exists(typeof(T), detachedQuery);
        }

        public bool Exists<PkType, T>(PkType id)
        {
            return Exists(typeof(T), id);
        }

        #endregion Exists

        public T Find<T>(object id)
        {
            return (T)FindByPrimaryKey(typeof(T), id, true);
        }

        #region FindAll
        public T[] FindAll<T>()
        {
            return (T[])FindAll(typeof(T));
        }

        public T[] FindAll<T>(params ICriterion[] criteria)
        {
            return (T[])FindAll(typeof(T), criteria);
        }

        public T[] FindAll<T>(IDetachedQuery detachedQuery)
        {
            return (T[])FindAll(typeof(T), detachedQuery);
        }

        public T[] FindAll<T>(Order[] orders, params ICriterion[] criteria)
        {
            return (T[])FindAll(typeof(T), orders, criteria);
        }

        public T[] FindAll<T>(DetachedCriteria criteria, params Order[] orders)
        {
            return (T[])FindAll(typeof(T), criteria, orders);
        }

        public T[] FindAll<T>(Order order, params ICriterion[] criteria)
        {
            return (T[])FindAll(typeof(T), new Order[] { order }, criteria);
        }

        #endregion FindAll

        #region FindAllByProperty

        public T[] FindAllByProperty<T>(string property, object value)
        {
            return (T[])FindAllByProperty(typeof(T), property, value);
        }

        public T[] FindAllByProperty<T>(string orderByColumn, string property, object value)
        {
            return (T[])FindAllByProperty(typeof(T), orderByColumn, property, value);
        }

        #endregion FindAllByProperty

        #region FindByPrimaryKey
        protected internal T FindByPrimaryKey<T>(object id)
        {
            return (T)FindByPrimaryKey(typeof(T), id);
        }

        protected internal T FindByPrimaryKey<T>(object id, bool throwOnNotFound)
        {
            return (T)FindByPrimaryKey(typeof(T), id, throwOnNotFound);
        }

        #endregion FindByPrimaryKey

        #region FindFirst
        public T FindFirst<T>(params ICriterion[] criteria)
        {
            return (T)FindFirst(typeof(T), criteria);
        }

        public T FindFirst<T>(IDetachedQuery detachedQuery)
        {
            return (T)FindFirst(typeof(T), detachedQuery);
        }

        public T FindFirst<T>(Order[] orders, params ICriterion[] criteria)
        {
            return (T)FindFirst(typeof(T), orders, criteria);
        }

        public T FindFirst<T>(DetachedCriteria criteria, params Order[] orders)
        {
            return (T)FindFirst(typeof(T), criteria, orders);
        }

        public T FindFirst<T>(Order order, params ICriterion[] criteria)
        {
            return (T)FindFirst(typeof(T), new Order[] { order }, criteria);
        }
        #endregion FindFirst

        #region FindOne
        public T FindOne<T>(params ICriterion[] criteria)
        {
            return (T)FindOne(typeof(T), criteria);
        }

        public T FindOne<T>(DetachedCriteria criteria)
        {
            return (T)FindOne(typeof(T), criteria);
        }

        public T FindOne<T>(IDetachedQuery detachedQuery)
        {
            return (T)FindOne(typeof(T), detachedQuery);
        }

        #endregion FindOne

        protected internal void Refresh<T>(T instance)
        {
            base.Refresh(instance);
        }

        protected internal void Save<T>(T instance)
        {
            base.Save(instance);
        }

        protected internal T SaveCopy<T>(T instance)
        {
            return (T)base.SaveCopy(instance);
        }
        
        #region SlicedFindAll
        public T[] SlicedFindAll<T>(int firstResult, int maxResults, params ICriterion[] criteria)
        {
            return (T[])SlicedFindAll(typeof(T), firstResult, maxResults, criteria);
        }

        public T[] SlicedFindAll<T>(int firstResult, int maxResults, IDetachedQuery detachedQuery)
        {
            return (T[])SlicedFindAll(typeof(T), firstResult, maxResults, detachedQuery);
        }

        public T[] SlicedFindAll<T>(int firstResult, int maxResults, Order[] orders, params ICriterion[] criteria)
        {
            return (T[])SlicedFindAll(typeof(T), firstResult, maxResults, orders, criteria);
        }

        public T[] SlicedFindAll<T>(int firstResult, int maxResults, DetachedCriteria criteria, params Order[] orders)
        {
            return (T[])SlicedFindAll(typeof(T), firstResult, maxResults, orders, criteria);
        }
        #endregion SlicedFindAll

        public T TryFind<T>(object id)
        {
            return (T)FindByPrimaryKey(typeof(T), id, false);
        }

        protected internal void Update<T>(T instance)
        {
            base.Update(instance);
        }
    }





}

