using ConditionsFabricsTests.InvincibilityConditionsFabricsTests.TestsScriptLanguage;
using FluentAssertions;
using NUnit.Framework;

namespace ConditionsFabricsTests.InvincibilityConditionsFabricsTests
{
    public class IsInvincibleСonditionFabricTests
    {
        [Test]
        public void WhenInvokeConditionCreatedByIsInvincibleConditionFabric_AndEntityDoesNotHaveInvincibility_ThenShouldBeFalse()
        {
            // Arrange.
            var isInvincibleCondition = SetUp.IsInvincibleConditionForEntityWithoutInvincibility();

            // Act.
            var result = isInvincibleCondition.Invoke();

            // Assert.
            new {IsInvincible = result}.Should().Be(new {IsInvincible = false});
        }
        
        [Test]
        public void WhenInvokeConditionCreatedByIsInvincibleConditionFabric_AndEntityDoesHaveFalseInvincibility_ThenShouldBeFalse()
        {
            // Arrange.
            var isInvincibleCondition = SetUp.IsInvincibleConditionForEntityWithInvincibility(false);

            // Act.
            var result = isInvincibleCondition.Invoke();

            // Assert.
            new {IsInvincible = result}.Should().Be(new {IsInvincible = false});
        }
        
        [Test]
        public void WhenInvokeConditionCreatedByIsInvincibleConditionFabric_AndEntityDoesHaveTrueInvincibility_ThenShouldBeTrue()
        {
            // Arrange.
            var isInvincibleCondition = SetUp.IsInvincibleConditionForEntityWithInvincibility(true);

            // Act.
            var result = isInvincibleCondition.Invoke();

            // Assert.
            new {IsInvincible = result}.Should().Be(new {IsInvincible = true});
        }
    }
}