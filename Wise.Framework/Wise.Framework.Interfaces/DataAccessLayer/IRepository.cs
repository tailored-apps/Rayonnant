using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wise.Framework.Interfaces.DataAccessLayer
{
    public interface IRepository: IDisposable
    {
        void Save(object obj);
    }
}
