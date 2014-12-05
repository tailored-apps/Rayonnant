namespace Wise.Framework.Interface.Data
{
    public interface ISearchCriteria<T, TProvider>
    {
        TProvider Criteria { get; }
    }
}