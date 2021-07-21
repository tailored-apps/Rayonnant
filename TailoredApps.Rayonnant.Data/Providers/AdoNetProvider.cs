using System;
using System.Collections.Generic;
using System.Data;
using TailoredApps.Rayonnant.Data.Enum;
using TailoredApps.Rayonnant.Data.Interface;
using TailoredApps.Rayonnant.Interface.Data;

namespace TailoredApps.Rayonnant.Data.Providers
{
    public class AdoNetProvider : IDataProvider
    {
        private readonly AdoNetDbPersister persister;
        public AdoNetProvider(AdoNetDbPersister persister)
        {
            this.persister = persister;
        }
        public TEntity Get<TEntity>(object id) where TEntity : class
        {
            if (persister.HasRegisteredOperation<AdoNetProviderOperations, TEntity>(AdoNetProviderOperations.Get))
            {
                return persister.Get<TEntity>(id);
            }
            throw new Exception("Please register operation Get");
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            if (persister.HasRegisteredOperation<AdoNetProviderOperations, TEntity>(AdoNetProviderOperations.GetAll))
            {
                return persister.GetAll<TEntity>();
            }
            throw new Exception("Please register operation GetAll");
        }

        public TEntity GetById<TKey, TEntity>(TKey id) where TEntity : class
        {
            if (persister.HasRegisteredOperation<AdoNetProviderOperations, TEntity>(AdoNetProviderOperations.GetById))
            {
                return persister.GetById<TKey,TEntity>(id);
            }
            throw new Exception("Please register operation GetById");
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class
        {
            if (persister.HasRegisteredOperation<AdoNetProviderOperations, TEntity>(AdoNetProviderOperations.Save))
            {
                persister.Save<TEntity>(entity);
            }
            throw new Exception("Please register operation Save");
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (persister.HasRegisteredOperation<AdoNetProviderOperations, TEntity>(AdoNetProviderOperations.Delete))
            {
                persister.Delete<TEntity>(entity);
            }
            throw new Exception("Please register operation Delete");
        }

        public void DeleteById<TKey, TEntity>(TKey id) where TEntity : class
        {
            if (persister.HasRegisteredOperation<AdoNetProviderOperations, TEntity>(AdoNetProviderOperations.DeleteById))
            {
                persister.DeleteById<TEntity>(id);
            }
            throw new Exception("Please register operation DeleteById");
        }

        public IEnumerable<TEntity> GetBySearchCriteria<TEntity, TProvider>(
            ISearchCriteria<TEntity, TProvider> searchCriteria) where TEntity : class where TProvider : class
        {
            if (persister.HasRegisteredOperation<AdoNetProviderOperations, ISearchCriteria<TEntity, TProvider>>(AdoNetProviderOperations.GetBySearchCriteria))
            {
                return persister.GetBySearchCriteria<TEntity, TProvider>(searchCriteria);
            }
            throw new Exception("Please register operation GetBySearchCriteria");
        }
    }
}