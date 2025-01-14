using GameplayConstructorElements.Expressions.Conditions;

namespace ConditionsFabricsTests.InvincibilityConditionsFabricsTests.TestsScriptLanguage
{
    public static class Create
    {
        public static IsInvincibleСonditionFabric IsInvincibleConditionFabric()
        {
            return new IsInvincibleСonditionFabric();
        }

        public static IsNotInvincibleСonditionFabric IsNotInvincibleConditionFabric()
        {
            return new IsNotInvincibleСonditionFabric();
        }
    }
}