using GameplayConstructorElements.Expressions.Conditions;

namespace Tests.ExpressionsFabricsTests.ConditionsFabricsTests.LifeConditionsFabricsTests.TestsScriptLanguage
{
    public static class Create
    {
        public static IsAliveConditionFabric IsAliveConditionFabric()
        {
            return new IsAliveConditionFabric();
        }

        public static IsDeadConditionFabric IsDeadConditionFabric()
        {
            return new IsDeadConditionFabric();
        }
    }
}