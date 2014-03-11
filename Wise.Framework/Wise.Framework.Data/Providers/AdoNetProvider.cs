using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wise.Framework.Data.Interface;

namespace Wise.Framework.Data.Providers
{
    public class AdoNetProvider : IDataProvider
    {
        public TEntity Get<TEntity>(object id)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById<TKey, TEntity>(TKey id)
        {
            throw new NotImplementedException();
        }

        public void Save<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById<TKey, TEntity>(TKey id)
        {
            throw new NotImplementedException();
        }
    }
}
