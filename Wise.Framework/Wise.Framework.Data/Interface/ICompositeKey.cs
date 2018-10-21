using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Data.Interface
{
    public interface ICompositeKey<KeyPropertyOne, KeyPropertyTwo>
    {
         KeyPropertyOne Id { get; set; }
         KeyPropertyTwo Key2 { get; set; }

    }
}
