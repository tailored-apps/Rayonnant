namespace Wise.Framework.Interface.Data
{
    public interface IRepository
    {
        TEntity Get<TEntity>(object id);
        TEntity GetById<TKey, TEntity>(TKey id);
        void Save<TEntity>(TEntity entity);
        void Delete<TEntity>(TEntity entity);

        void DeleteById<TKey, TEntity>(TKey id);

    }
}
