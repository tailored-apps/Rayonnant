namespace TailoredApps.Rayonnant.Data.Entity
{
    public abstract class EntityBaseAbstract<TKey, TEntity>
    {
        public virtual TKey Id { get; protected set; }

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

    public abstract class EntityBaseAbstract<TKey, TkeyTwo, TEntity> : EntityBaseAbstract<TKey, TEntity>
    {
        public virtual TkeyTwo IdTwo { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj is TEntity)
            {
                var oth = (EntityBaseAbstract<TKey, TkeyTwo, TEntity>)obj;
                return Id.Equals(oth.Id) && IdTwo.Equals(oth.IdTwo);
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (!Id.Equals(default(TKey)) && !IdTwo.Equals(default(TkeyTwo)))
            {
                return ToString().GetHashCode();
            }
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("#{0}#{1}:{2}", typeof(TEntity), Id, IdTwo);
        }
    }
}