using FluentAssertions;
using NUnit.Framework;
using Tests.ExpressionsFabricsTests.ConditionsFabricsTests.LifeConditionsFabricsTests.TestsScriptLanguage;

namespace Tests.ExpressionsFabricsTests.ConditionsFabricsTests.LifeConditionsFabricsTests
{
    public class IsDeadConditionTests
    {
        [Test]
        public void WhenInvokeConditionCreatedByIsDeadConditionFabric_AndEntityDoesNotHaveHealth_ThenShouldBeTrue()
        {
            // Arrange.
            var isDeadCondition = SetUp.IsDeadConditionForEntityWithoutHealth();

            // Act.
            var result = isDeadCondition.Invoke();

            // Assert.
            new { IsDead = result }.Should().Be(new { IsDead = true });
        }
        
        [Test]
        public void WhenInvokeConditionCreatedByIsDeadConditionFabric_AndEntityHaveZeroHealth_ThenShouldBeTrue()
        {
            // Arrange.
            var isDeadCondition = SetUp.IsDeadConditionForEntityWithHealth(0f);

            // Act.
            var result = isDeadCondition.Invoke();

            // Assert.
            new { IsDead = result }.Should().Be(new { IsDead = true });
        }
        
        [Test]
        public void WhenInvokeConditionCreatedByIsDeadConditionFabric_AndEntityHaveNegativeHealth_ThenShouldBeTrue()
        {
            // Arrange.
            var isDeadCondition = SetUp.IsDeadConditionForEntityWithHealth(-1f);

            // Act.
            var result = isDeadCondition.Invoke();

            // Assert.
            new { IsDead = result }.Should().Be(new { IsDead = true });
        }
        
        [Test]
        public void WhenInvokeConditionCreatedByIsDeadConditionFabric_AndEntityHaveHealth_ThenShouldBeFalse()
        {
            // Arrange.
            var isDeadCondition = SetUp.IsDeadConditionForEntityWithHealth(1f);

            // Act.
            var result = isDeadCondition.Invoke();

            // Assert.
            new { IsDead = result }.Should().Be(new { IsDead = false });
        }
    }
}