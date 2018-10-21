using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Rest.Attributes
{
    public class Route : Attribute
    {
        public string Url { get; set; }
        public OperationType OperationType { get; set; }
    }
}

namespace Wise.Framework.Rest.Attributes
{
   public enum OperationType
    {
        GET,
        POST,
        PUT,
        DELETE,
        PATCH
    }
}