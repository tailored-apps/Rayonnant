using System.Collections.Generic;
using TailoredApps.Rayonnant.Data.Interface;
using TailoredApps.Rayonnant.Interface.Data;

namespace TailoredApps.Rayonnant.Data
{
    public class Repository : IRepository
    {
        private readonly IDataProvider dataProvider;

        public Repository(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            dataProvider.Delete(entity);
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class
        {
            dataProvider.Save(entity);
        }

        public TEntity Get<TEntity>(object id) where TEntity : class
        {
            return dataProvider.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
           return dataProvider.GetAll<TEntity>();
        }

        public void DeleteById<TKey, TEntity>(TKey id) where TEntity : class
        {
            dataProvider.DeleteById<TKey, TEntity>(id);
        }

        public TEntity GetById<TKey, TEntity>(TKey id) where TEntity : class
        {
            return dataProvider.GetById<TKey, TEntity>(id);
        }

        public IEnumerable<TEntity> GetBySearchCriteria<TEntity, TProvider>(ISearchCriteria<TEntity, TProvider> searchCriteria) 
            where TEntity : class 
            where TProvider : class
        {
            return dataProvider.GetBySearchCriteria(searchCriteria);
        }
    }
}