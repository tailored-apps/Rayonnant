using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TailoredApps.Rayonnant.Data.Enum;
using TailoredApps.Rayonnant.Data.Interface;
using TailoredApps.Rayonnant.Interface.Data;

namespace TailoredApps.Rayonnant.Data.Providers
{
    public class AdoNetDbPersister
    {
        Dictionary<Type, AdoNetEntitySettings> dict = new Dictionary<Type, AdoNetEntitySettings>();

        public abstract class Transformer<TEntity>
        {
            public abstract TEntity Transform(SqlDataReader reader);
        }

        public void SetOperation<TEntity>(AdoNetProviderOperations operation, string command, AdoNetCommandType commandType,AdoNetExecutionType execution, Transformer<TEntity> transformer)
        {
            if (dict.ContainsKey(typeof(TEntity)))
            {
                dict.Add(typeof(TEntity), new AdoNetEntitySettings());
            }
            dict[typeof(TEntity)].Operations.Add(operation);

        }

        internal bool HasRegisteredOperation<TOperation, TEntity>(AdoNetProviderOperations operation) where TEntity : class
        {
            return dict.ContainsKey(typeof(TEntity)) && dict[typeof(TEntity)].Operations.Contains(operation);
        }

        internal TEntity Get<TEntity>(object id) where TEntity : class
        {
            throw new NotImplementedException();

            //dict[typeof(TEntity)].
        }

        internal IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        internal TEntity GetById<TKey, TEntity>(TKey id) where TEntity : class
        {
            throw new NotImplementedException();
        }

        internal void Save<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        internal void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        internal void DeleteById<TEntity>(object id) where TEntity : class
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<TEntity> GetBySearchCriteria<TEntity, TProvider>(ISearchCriteria<TEntity, TProvider> searchCriteria)
            where TEntity : class
            where TProvider : class
        {
            throw new NotImplementedException();
        }
    }
}