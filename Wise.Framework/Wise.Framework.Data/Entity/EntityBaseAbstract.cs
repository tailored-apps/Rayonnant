using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Data.Entity
{
    public abstract class EntityBaseAbstract<TKey, TEntity>
    {
        public virtual TKey Id { get; protected set; }


        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(TEntity))
            {
                var oth = (EntityBaseAbstract<TKey, TEntity>)obj;
                return Id.Equals(oth.Id);
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (!Id.Equals(default(TKey)))
            {
                return ToString().GetHashCode();
            }
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("#{0}#{1}", typeof(TEntity), Id);
        }
    }
}
