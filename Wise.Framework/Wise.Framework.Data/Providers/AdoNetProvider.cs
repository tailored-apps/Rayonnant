using System;
using System.Collections.Generic;
using System.Data;
using Wise.Framework.Data.Interface;
using Wise.Framework.Interface.Data;

namespace Wise.Framework.Data.Providers
{
    public class AdoNetProvider : IDataProvider
    {
        public TEntity Get<TEntity>(object id) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TEntity GetById<TKey, TEntity>(TKey id) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void DeleteById<TKey, TEntity>(TKey id) where TEntity : class
        {
            throw new NotImplementedException();
        }


        public IEnumerable<TEntity> GetBySearchCriteria<TEntity, TProvider>(
            ISearchCriteria<TEntity, TProvider> searchCriteria) where TEntity : class where TProvider : class
        {
            throw new NotImplementedException();
        }
    }
}