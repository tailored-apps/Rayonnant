using System;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;

namespace Wise.Framework.Interface.Data
{
    public interface ISearchCriteria<T, TProvider>
    {
        TProvider Criteria { get; }
    }
}
