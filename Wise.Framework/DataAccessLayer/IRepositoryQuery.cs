using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Wise.Framework.DataAccessLayer
{
    public interface IRepositoryQuery
    {
        // Methods
        IEnumerable Enumerate(ISession session);
        object Execute(ISession session);

        // Properties
        Type RootType { get; }

    }
}
