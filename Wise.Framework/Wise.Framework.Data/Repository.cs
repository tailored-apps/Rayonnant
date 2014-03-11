using Wise.Framework.Data.Interface;
using Wise.Framework.Interface.Data;

namespace Wise.Framework.Data
{
    public class Repository : IRepository
    {

        private readonly IDataProvider dataProvider;
        public Repository(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        public void Delete<TEntity>(TEntity entity)
        {
            dataProvider.Delete(entity);
        }

        public void Save<TEntity>(TEntity entity)
        {
            dataProvider.Save(entity);
        }

        public TEntity Get<TEntity>(object id)
        {
            return dataProvider.Get<TEntity>(id);
        }

        public void DeleteById<TKey, TEntity>(TKey id)
        {
            dataProvider.DeleteById<TKey, TEntity>(id);
        }

        public TEntity GetById<TKey, TEntity>(TKey id)
        {
            return dataProvider.GetById<TKey, TEntity>(id);
        }
    }
}
