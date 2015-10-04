using Wise.Framework.Data.Interface;
namespace Wise.Framework.Data.Entity
{
    public abstract class EntityBaseAbstract<TKey1, TKey2, TEntity> : EntityBaseAbstract<TKey1, TEntity> , ICompositeKey<TKey1, TKey2>
    {
        public virtual TKey2 Key2 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is TEntity)
            {
                var oth = (EntityBaseAbstract<TKey1, TKey2, TEntity>)obj;
                return Id.Equals(oth.Id);
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (!Id.Equals(default(TKey1)) && !Key2.Equals(default(TKey2)))
            {
                return ToString().GetHashCode();
            }
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("#{0}#{1}:{2}", typeof(TEntity), Id, Key2);
        }
    }

    public abstract class EntityBaseAbstract<TKey, TEntity>
    {
        public virtual TKey Id { get;  set; }


        public override bool Equals(object obj)
        {
            if (obj is TEntity)
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