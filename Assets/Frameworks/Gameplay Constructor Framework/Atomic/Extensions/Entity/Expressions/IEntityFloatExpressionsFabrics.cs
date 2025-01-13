namespace AtomicFramework.AtomicStructures
{
    public interface IEntityFloatExpressionElementFabric : IEntityFunctionFabric<float> {}
   
    public interface IEntityFloatExpressionElementFabric<in T> : IEntityFunctionFabric<T, float> {}
   
    public interface IEntityFloatExpressionElementFabric<in T1, in  T2> : IEntityFunctionFabric<T1, T2, float> {}
   
    public interface IEntityFloatExpressionElementFabric<in T1, in T2, in T3> : IEntityFunctionFabric<T1, T2, T3, float> {}
}