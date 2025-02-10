using FluentAssertions;
using NUnit.Framework;
using Tests.ExpressionsFabricsTests.ConditionsFabricsTests.LifeConditionsFabricsTests.TestsScriptLanguage;

namespace Tests.ExpressionsFabricsTests.ConditionsFabricsTests.LifeConditionsFabricsTests
{
    public class IsAliveConditionFabricTests
    {
        [Test]
        public void WhenInvokeConditionCreatedByIsAliveConditionFabric_AndEntityDoesNotHaveHealth_ThenShouldBeFalse()
        {
            // Arrange.
            var isAliveCondition = SetUp.IsAliveConditionForEntityWithoutHealth();

            // Act.
            var result = isAliveCondition.Invoke();

            // Assert.
            new {IsAlive = result}.Should().Be(new {IsAlive = false});
        }
        
        [Test]
        public void WhenInvokeConditionCreatedByIsAliveConditionFabric_AndEntityHaveZeroHealth_ThenShouldBeFalse()
        {
            // Arrange.
            var isAliveCondition = SetUp.IsAliveConditionForEntityWithHealth(0f);

            // Act.
            var result = isAliveCondition.Invoke();

            // Assert.
            new {IsAlive = result}.Should().Be(new {IsAlive = false});
        }
        
        [Test]
        public void WhenInvokeConditionCreatedByIsAliveConditionFabric_AndEntityHaveNegativeHealth_ThenShouldBeFalse()
        {
            // Arrange.
            var isAliveCondition = SetUp.IsAliveConditionForEntityWithHealth(-1f);

            // Act.
            var result = isAliveCondition.Invoke();

            // Assert.
            new {IsAlive = result}.Should().Be(new {IsAlive = false});
        }
        
        [Test]
        public void WhenInvokeConditionCreatedByIsAliveConditionFabric_AndEntityHaveHealth_ThenShouldBeTrue()
        {
            // Arrange.
            var isAliveCondition = SetUp.IsAliveConditionForEntityWithHealth(1f);

            // Act.
            var result = isAliveCondition.Invoke();

            // Assert.
            new {IsAlive = result}.Should().Be(new {IsAlive = true});
        }
    }
}