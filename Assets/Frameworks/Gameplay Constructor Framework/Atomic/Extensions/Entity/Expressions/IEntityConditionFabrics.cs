namespace AtomicFramework.AtomicStructures
{
   public interface IEntityConditionFabric : IEntityFunctionFabric<bool> {}
   
   public interface IEntityConditionFabric<in T> : IEntityFunctionFabric<T, bool> {}
   
   public interface IEntityConditionFabric<in T1, in  T2> : IEntityFunctionFabric<T1, T2, bool> {}
   
   public interface IEntityConditionFabric<in T1, in T2, in T3> : IEntityFunctionFabric<T1, T2, T3, bool> {}
}