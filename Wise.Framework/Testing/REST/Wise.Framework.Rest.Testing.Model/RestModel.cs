using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Rest.Testing.Model
{
    public class RestModel
    {
        public int MyIntProperty { get; set; }

        public string MyStringProperty { get; set; }

        public int? MyNullableIntProperty { get; set; }

        public decimal MyDecimalProperty { get; set; }

        public decimal? MyNullableDec { get; set; }

        public DateTime MyDateTime { get; set; }

        public DateTime? NullableDateTime { get; set; }
    }
}
