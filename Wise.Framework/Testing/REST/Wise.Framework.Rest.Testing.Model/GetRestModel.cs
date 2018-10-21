using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wise.Framework.Rest.Attributes;

namespace Wise.Framework.Rest.Testing.Model
{

    [Route(Url = "/Dummy/{Id}/{Message}", OperationType = OperationType.GET)]
    public class GetRestModel : IRestRequest<GetRestModelResponse>
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public Authorization Auth { get; set; }

        public GetRestModelResponse Result { get; set; }
    }


    
}
