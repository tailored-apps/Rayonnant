using System.Collections.Generic;

namespace Wise.Framework.Interface.Data
{
    public interface IRepository
    {
        TEntity Get<TEntity>(object id) where TEntity : class;
        TEntity GetById<TKey, TEntity>(TKey id) where TEntity : class;
        void Save<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;

        void DeleteById<TKey, TEntity>(TKey id) where TEntity : class;

        IEnumerable<TEntity> GetBySearchCriteria<TEntity, TProvider>(ISearchCriteria<TEntity, TProvider> searchCriteria)
            where TEntity : class
            where TProvider : class;
    }
}