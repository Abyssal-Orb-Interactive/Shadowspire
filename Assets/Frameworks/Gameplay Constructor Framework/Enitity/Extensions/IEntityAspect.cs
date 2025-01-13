namespace GameplayConstructorFramework.Entity.Extensions
{
    public interface IEntityAspect
    {
        public void ApplyTo (IEntity entity);
        public void DiscardFrom (IEntity entity);
    }
}