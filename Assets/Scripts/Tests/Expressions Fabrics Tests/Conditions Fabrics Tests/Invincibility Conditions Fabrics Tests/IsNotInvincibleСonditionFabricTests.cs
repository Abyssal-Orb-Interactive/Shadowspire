using System;
using FluentAssertions;
using NUnit.Framework;
using Tests.EntityExtensionsTests.TestsScriptLanguage;
using SetUp = ConditionsFabricsTests.InvincibilityConditionsFabricsTests.TestsScriptLanguage.SetUp;

namespace ConditionsFabricsTests.InvincibilityConditionsFabricsTests
{
    public class IsNotInvincibleСonditionFabricTests
    {
        [Test]
        public void WhenInvokeConditionCreatedByIsNotInvincibleConditionFabric_AndEntityDoesNotHaveInvincibility_ThenShouldBeTrue()
        {
            // Arrange.
            var isNotInvincibleСondition = SetUp.IsNotInvincibleConditionForEntityWithoutInvincibility();

            // Act.
            var result = isNotInvincibleСondition.Invoke();

            // Assert.
            new {isNotInvincible = result}.Should().Be(new {isNotInvincible = true});

        }
        
        [Test]
        public void WhenInvokeConditionCreatedByIsNotInvincibleConditionFabric_AndEntityHaveFalseInvincibility_ThenShouldBeTrue()
        {
            // Arrange.
            var isNotInvincibleСondition = SetUp.IsNotInvincibleConditionForEntityWithInvincibility(false);

            // Act.
            var result = isNotInvincibleСondition.Invoke();

            // Assert.
            new {isNotInvincible = result}.Should().Be(new {isNotInvincible = true});

        }
        
        [Test]
        public void WhenInvokeConditionCreatedByIsNotInvincibleConditionFabric_AndEntityHaveTrueInvincibility_ThenShouldBeFalse()
        {
            // Arrange.
            var isNotInvincibleСondition = SetUp.IsNotInvincibleConditionForEntityWithInvincibility(true);

            // Act.
            var result = isNotInvincibleСondition.Invoke();

            // Assert.
            new {isNotInvincible = result}.Should().Be(new {isNotInvincible = false});

        }
    }
}