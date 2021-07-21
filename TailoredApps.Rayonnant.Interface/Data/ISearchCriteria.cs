namespace TailoredApps.Rayonnant.Interface.Data
{
    public interface ISearchCriteria<T, TProvider>
    {
        TProvider Criteria { get; }
    }
}