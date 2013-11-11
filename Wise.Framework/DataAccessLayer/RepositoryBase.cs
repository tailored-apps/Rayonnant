using System;
using System.Collections;
using System.Collections.Generic;
using Iesi.Collections;
using NHibernate;
using NHibernate.Criterion;

namespace Wise.Framework.DataAccessLayer
{
    public abstract class RepositoryBase
    {
        protected internal ISessionFactory holder;

        protected internal ISession Session { get { return holder.GetCurrentSession(); } }

        protected RepositoryBase(ISessionFactory sessionFactory)
        {
            this.holder = sessionFactory;
        }

        private void AddOrdersToCriteria(ICriteria criteria, IEnumerable<Order> orders)
        {
            if (orders != null)
            {
                foreach (Order order in orders)
                {
                    criteria.AddOrder(order);
                }
            }
        }

        public Order[] Asc(params string[] propertyNames)
        {
            return PropertyNamesToOrderArray(true, propertyNames);
        }

        protected internal int Count(Type targetType)
        {
            CountQuery query = new CountQuery(targetType);
            return (int)ExecuteQuery(query);
        }

        protected internal int Count(Type targetType, ICriterion[] criteria)
        {
            CountQuery query = new CountQuery(targetType, criteria);
            return (int)ExecuteQuery(query);
        }

        protected internal int Count(Type targetType, DetachedCriteria detachedCriteria)
        {
            CountQuery query = new CountQuery(targetType, detachedCriteria);
            return (int)ExecuteQuery(query);
        }


        public virtual void Create()
        {
            Create(this);
        }

        protected internal void Create(object instance)
        {
            InternalCreate(instance, false);
        }

        public virtual void CreateAndFlush()
        {
            CreateAndFlush(this);
        }

        protected internal void CreateAndFlush(object instance)
        {
            InternalCreate(instance, true);
        }

        public virtual void Delete()
        {
            Delete(this);
        }

        protected internal void Delete(object instance)
        {
            InternalDelete(instance, false);
        }



        protected internal int DeleteAll(Type targetType, IEnumerable pkValues)
        {
            if (pkValues == null)
            {
                return 0;
            }
            int num = 0;
            foreach (object obj2 in pkValues)
            {
                object instance = FindByPrimaryKey(targetType, obj2, false);
                if (instance != null)
                {
                    var base2 = instance as EntityBase;
                    if (base2 != null)
                    {
                        Session.Delete(base2);
                    }
                    else
                    {
                        Delete(instance);
                    }
                    num++;
                }
            }
            return num;
        }


        public virtual void DeleteAndFlush()
        {
            DeleteAndFlush(this);
        }

        protected internal void DeleteAndFlush(object instance)
        {
            InternalDelete(instance, true);
        }

        public Order[] Desc(params string[] propertyNames)
        {
            return PropertyNamesToOrderArray(false, propertyNames);
        }

        protected internal IEnumerable EnumerateQuery(IRepositoryQuery query)
        {
            IEnumerable enumerable;
            Type rootType = query.RootType;


            enumerable = query.Enumerate(Session);

            return enumerable;
        }



        public object ExecuteQuery(IRepositoryQuery query)
        {
            object obj2;
            Type rootType = query.RootType;
            obj2 = query.Execute(Session);

            return obj2;
        }

        protected internal bool Exists(Type targetType)
        {
            return (Count(targetType) > 0);
        }

        protected internal bool Exists(Type targetType, params ICriterion[] criteria)
        {
            return (Count(targetType, criteria) > 0);
        }

        protected internal bool Exists(Type targetType, DetachedCriteria detachedCriteria)
        {
            return (Count(targetType, detachedCriteria) > 0);
        }

        protected internal bool Exists(Type targetType, IDetachedQuery detachedQuery)
        {
            return (SlicedFindAll(targetType, 0, 1, detachedQuery).Length > 0);
        }

        protected internal bool Exists(Type targetType, object id)
        {
            bool flag;

            flag = Session.Get(targetType, id) != null;

            return flag;
        }



        protected internal Array FindAll(Type targetType)
        {
            DetachedCriteria detachedCriteria = DetachedCriteria.For(targetType).SetResultTransformer(CriteriaSpecification.DistinctRootEntity);
            return FindAll(targetType, detachedCriteria, null);
        }

        protected internal Array FindAll(Type targetType, params ICriterion[] criteria)
        {
            return FindAll(targetType, null, criteria);
        }

        protected internal Array FindAll(Type targetType, IDetachedQuery detachedQuery)
        {
            Array array;


            array = BuildArray(targetType, detachedQuery.GetExecutableQuery(Session).List());

            return array;
        }

        protected internal Array FindAll(Type targetType, Order[] orders, params ICriterion[] criteria)
        {
            Array array;

            ICriteria criteria2 = Session.CreateCriteria(targetType);
            foreach (ICriterion criterion in criteria)
            {
                criteria2.Add(criterion);
            }
            AddOrdersToCriteria(criteria2, orders);
            array = BuildArray(targetType, criteria2.List());

            return array;
        }

        protected internal Array FindAll(Type targetType, DetachedCriteria detachedCriteria, params Order[] orders)
        {
            Array array;


            ICriteria executableCriteria = detachedCriteria.GetExecutableCriteria(Session);
            AddOrdersToCriteria(executableCriteria, orders);
            array = BuildArray(targetType, executableCriteria.List());


            return array;
        }

        protected internal Array FindAllByProperty(Type targetType, string property, object value)
        {
            ICriterion criterion = (value == null) ? Restrictions.IsNull(property) : Restrictions.Eq(property, value);
            return FindAll(targetType, new ICriterion[] { criterion });
        }

        protected internal Array FindAllByProperty(Type targetType, string orderByColumn, string property, object value)
        {
            ICriterion criterion = (value == null) ? Restrictions.IsNull(property) : Restrictions.Eq(property, value);
            return FindAll(targetType, new Order[] { Order.Asc(orderByColumn) }, new ICriterion[] { criterion });
        }

        protected internal object FindByPrimaryKey(Type targetType, object id)
        {
            return FindByPrimaryKey(targetType, id, true);
        }

        protected internal object FindByPrimaryKey(Type targetType, object id, bool throwOnNotFound)
        {
            return throwOnNotFound ? Session.Load(targetType, id) : Session.Get(targetType, id);

        }

        protected internal object FindFirst(Type targetType, params ICriterion[] criteria)
        {
            return FindFirst(targetType, null, criteria);
        }

        protected internal object FindFirst(Type targetType, IDetachedQuery detachedQuery)
        {
            Array array = SlicedFindAll(targetType, 0, 1, detachedQuery);
            if ((array != null) && (array.Length > 0))
            {
                return array.GetValue(0);
            }
            return null;
        }

        protected internal object FindFirst(Type targetType, Order[] orders, params ICriterion[] criteria)
        {
            Array array = SlicedFindAll(targetType, 0, 1, orders, criteria);
            if ((array != null) && (array.Length > 0))
            {
                return array.GetValue(0);
            }
            return null;
        }

        protected internal object FindFirst(Type targetType, DetachedCriteria detachedCriteria, params Order[] orders)
        {
            Array array = SlicedFindAll(targetType, 0, 1, orders, detachedCriteria);
            if ((array != null) && (array.Length > 0))
            {
                return array.GetValue(0);
            }
            return null;
        }
        protected internal Array BuildArray(Type targetType, IList list)
        {
            Array array = Array.CreateInstance(targetType, list.Count);
            list.CopyTo(array, 0);
            return array;
        }


        protected internal T[] BuildArray<T>(IEnumerable list, bool distinct)
        {
            return (T[])BuildArray(typeof(T), list, distinct);
        }

        protected internal Array BuildArray(Type type, IEnumerable list, bool distinct)
        {
            return BuildArray(type, list, -1, distinct);
        }

        protected internal Array BuildArray(Type type, IEnumerable list, int entityIndex, bool distinct)
        {
            if (distinct || (entityIndex != -1))
            {
                Set set = distinct ? new ListSet() : null;
                ICollection is2 = list as ICollection;
                IList list2 = (is2 != null) ? new ArrayList(is2.Count) : new ArrayList();
                foreach (object obj2 in list)
                {
                    object o = (entityIndex == -1) ? obj2 : ((object[])obj2)[entityIndex];
                    if ((set == null) || set.Add(o))
                    {
                        list2.Add(o);
                    }
                }
                list = list2;
            }
            ICollection is3 = list as ICollection;
            if (is3 == null)
            {
                ArrayList list3 = new ArrayList();
                foreach (object obj4 in list)
                {
                    list3.Add(obj4);
                }
                is3 = list3;
            }
            Array array = Array.CreateInstance(type, is3.Count);
            is3.CopyTo(array, 0);
            return array;
        }

        protected internal object FindOne(Type targetType, params ICriterion[] criteria)
        {
            Array array = SlicedFindAll(targetType, 0, 2, criteria);
            if (array.Length > 1)
            {
                throw new Exception(string.Concat(new object[] { targetType.Name, ".FindOne returned ", array.Length, " rows. Expecting one or none" }));
            }
            if (array.Length != 0)
            {
                return array.GetValue(0);
            }
            return null;
        }

        protected internal object FindOne(Type targetType, DetachedCriteria criteria)
        {
            Array array = SlicedFindAll(targetType, 0, 2, criteria);
            if (array.Length > 1)
            {
                throw new Exception(string.Concat(new object[] { targetType.Name, ".FindOne returned ", array.Length, " rows. Expecting one or none" }));
            }
            if (array.Length != 0)
            {
                return array.GetValue(0);
            }
            return null;
        }

        protected internal object FindOne(Type targetType, IDetachedQuery detachedQuery)
        {
            Array array = SlicedFindAll(targetType, 0, 2, detachedQuery);
            if (array.Length > 1)
            {
                throw new Exception(string.Concat(new object[] { targetType.Name, ".FindOne returned ", array.Length, " rows. Expecting one or none" }));
            }
            if (array.Length != 0)
            {
                return array.GetValue(0);
            }
            return null;
        }



        private void InternalCreate(object instance, bool flush)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            Session.Save(instance);
            if (flush)
            {
                Session.Flush();
            }

        }

        private void InternalDelete(object instance, bool flush)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }


            Session.Delete(instance);
            if (flush)
            {
                Session.Flush();
            }

        }

        private void InternalSave(object instance, bool flush)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            Session.SaveOrUpdate(instance);
            if (flush)
            {
                Session.Flush();
            }

        }

        private object InternalSaveCopy(object instance, bool flush)
        {
            object obj3;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }


            object obj2 = Session.Merge(instance);
            if (flush)
            {
                Session.Flush();
            }
            obj3 = obj2;

            return obj3;
        }

        private void InternalUpdate(object instance, bool flush)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            Session.Update(instance);
            if (flush)
            {
                Session.Flush();
            }

        }

        internal Order[] PropertyNamesToOrderArray(bool asc, params string[] propertyNames)
        {
            Order[] orderArray = new Order[propertyNames.Length];
            for (int i = 0; i < propertyNames.Length; i++)
            {
                orderArray[i] = new Order(propertyNames[i], asc);
            }
            return orderArray;
        }

        public virtual void Refresh()
        {
            Refresh(this);
        }

        protected internal void Refresh(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            Session.Refresh(instance);

        }


        protected internal void Replicate(object instance, ReplicationMode replicationMode)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            Session.Replicate(instance, replicationMode);

        }

        public virtual void Save()
        {
            Save(this);
        }

        protected internal void Save(object instance)
        {
            InternalSave(instance, false);
        }

        public virtual void SaveAndFlush()
        {
            SaveAndFlush(this);
        }

        protected internal void SaveAndFlush(object instance)
        {
            InternalSave(instance, true);
        }

        public virtual object SaveCopy()
        {
            return SaveCopy(this);
        }

        protected internal object SaveCopy(object instance)
        {
            return InternalSaveCopy(instance, false);
        }

        public virtual object SaveCopyAndFlush()
        {
            return SaveCopyAndFlush(this);
        }

        protected internal object SaveCopyAndFlush(object instance)
        {
            return InternalSaveCopy(instance, true);
        }

        protected internal Array SlicedFindAll(Type targetType, int firstResult, int maxResults, params ICriterion[] criteria)
        {
            return SlicedFindAll(targetType, firstResult, maxResults, null, criteria);
        }

        protected internal Array SlicedFindAll(Type targetType, int firstResult, int maxResults, DetachedCriteria criteria)
        {
            return SlicedFindAll(targetType, firstResult, maxResults, null, criteria);
        }

        public Array SlicedFindAll(Type targetType, int firstResult, int maxResults, IDetachedQuery detachedQuery)
        {
            Array array;


            IQuery executableQuery = detachedQuery.GetExecutableQuery(Session);
            executableQuery.SetFirstResult(firstResult);
            executableQuery.SetMaxResults(maxResults);
            array = BuildArray(targetType, executableQuery.List());

            return array;
        }

        protected internal Array SlicedFindAll(Type targetType, int firstResult, int maxResults, Order[] orders, params ICriterion[] criteria)
        {
            Array array;

            ICriteria criteria2 = Session.CreateCriteria(targetType);
            foreach (ICriterion criterion in criteria)
            {
                criteria2.Add(criterion);
            }
            if (orders != null)
            {
                foreach (Order order in orders)
                {
                    criteria2.AddOrder(order);
                }
            }
            criteria2.SetFirstResult(firstResult);
            criteria2.SetMaxResults(maxResults);
            array = BuildArray(targetType, criteria2.List());

            return array;
        }

        protected internal Array SlicedFindAll(Type targetType, int firstResult, int maxResults, Order[] orders, DetachedCriteria criteria)
        {
            Array array;


            ICriteria executableCriteria = criteria.GetExecutableCriteria(Session);
            AddOrdersToCriteria(executableCriteria, orders);
            executableCriteria.SetFirstResult(firstResult);
            executableCriteria.SetMaxResults(maxResults);
            array = BuildArray(targetType, executableCriteria.List());

            return array;
        }


        public virtual void Update()
        {
            Update(this);
        }

        protected internal void Update(object instance)
        {
            InternalUpdate(instance, false);
        }

        public virtual void UpdateAndFlush()
        {
            UpdateAndFlush(this);
        }

        protected internal void UpdateAndFlush(object instance)
        {
            InternalUpdate(instance, true);
        }
    }
}
