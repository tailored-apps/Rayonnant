using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Rest
{
    public interface IRestRequest<T> : IRestRequest 
        where T : IRestResponse
    {
        T Result { get; set; }
    }


    public interface IRestRequest
    {
        Authorization Auth { get; set; }
    }
}
