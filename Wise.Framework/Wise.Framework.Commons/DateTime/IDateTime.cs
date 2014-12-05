using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Commons.DateTime
{
    public interface IDateTime
    {
        System.DateTime Now { get; }
        System.DateTime UtcNow { get; }
    }
}
