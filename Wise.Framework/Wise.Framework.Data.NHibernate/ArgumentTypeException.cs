using System;
using System.Runtime.Serialization;

namespace Wise.Framework.Data.NHibernate
{
    [Serializable]
    internal class ArgumentTypeException : Exception
    {
        private string v;
        private Type type1;
        private Type type2;

        public ArgumentTypeException()
        {
        }

        public ArgumentTypeException(string message) : base(message)
        {
        }

        public ArgumentTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ArgumentTypeException(string v, Type type1, Type type2)
        {
            this.v = v;
            this.type1 = type1;
            this.type2 = type2;
        }

        protected ArgumentTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}