using System;

namespace Wise.Framework.Rest.Testing.Model
{
    public class GetRestModelResponse : IRestResponse
    {
        public int ErrorCode { get; set;}
        public bool Status { get; set; }

        public RestModel RestModel { get; set; }
    }
}