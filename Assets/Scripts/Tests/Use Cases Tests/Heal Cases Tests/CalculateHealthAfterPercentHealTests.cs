using FluentAssertions;
using NUnit.Framework;
using UseCases;

namespace UseCasesTests.HealCasesTests
{
    public class CalculateHealthAfterPercentHealTests
    {
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndMaxHealthIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float MAX_HEALTH = -1f;
            const float CURRENT_HEALTH = 90f;
            const float HEAL_PERCENT = 0.2f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = 0f });
        }
        
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndMaxHealthIsZero_ThenShouldBeZero()
        {
            // Arrange.
            const float MAX_HEALTH = 0f;
            const float CURRENT_HEALTH = 90f;
            const float HEAL_PERCENT = 0.2f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = 0f });
        }
        
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndCurrentHealthIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float MAX_HEALTH = 100f;
            const float CURRENT_HEALTH = -1f;
            const float HEAL_PERCENT = 0.2f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = 0f });
        }
        
        
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndCurrentHealthIsZero_ThenShouldBeZero()
        {
            // Arrange.
            const float MAX_HEALTH = 100f;
            const float CURRENT_HEALTH = 0f;
            const float HEAL_PERCENT = 0.2f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = 0f });
        }
        
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndCurrentHealthIsBiggerThenMaxHealth_ThenShouldBeMaxHealth()
        {
            // Arrange.
            const float MAX_HEALTH = 100f;
            const float CURRENT_HEALTH = 120f;
            const float HEAL_PERCENT = 0.2f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = MAX_HEALTH });
        }
        
        
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndCurrentHealthIsEqualMaxHealth_ThenShouldBeMaxHealth()
        {
            // Arrange.
            const float MAX_HEALTH = 100f;
            const float CURRENT_HEALTH = 100f;
            const float HEAL_PERCENT = 0.2f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = MAX_HEALTH });
        }
        
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndHealPercentIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float MAX_HEALTH = 100f;
            const float CURRENT_HEALTH = 90f;
            const float HEAL_PERCENT = -0.2f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = 0f });
        }
        
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndHealPercentIsZero_ThenShouldBeZero()
        {
            // Arrange.
            const float MAX_HEALTH = 100f;
            const float CURRENT_HEALTH = 90f;
            const float HEAL_PERCENT = 0f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = 0f });
        }
        
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndHealPercentIsOne_ThenShouldBeMaxHealth()
        {
            // Arrange.
            const float MAX_HEALTH = 100f;
            const float CURRENT_HEALTH = 90f;
            const float HEAL_PERCENT = 1f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = MAX_HEALTH });
        }
        
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndHealPercentIsBiggerThenOne_ThenShouldBeMaxHealth()
        {
            // Arrange.
            const float MAX_HEALTH = 100f;
            const float CURRENT_HEALTH = 90f;
            const float HEAL_PERCENT = 2f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = MAX_HEALTH });
        }
        
        [Test]
        public void WhenCalculateHealthAfterPercentHeal_AndMaxHealthIsHundredAndCurrentHealthIsNinetyAndHealthPercentIsZeroPointTwoHundredths_ThenShouldBeNinetyTwo()
        {
            // Arrange.
            const float MAX_HEALTH = 100f;
            const float CURRENT_HEALTH = 90f;
            const float HEAL_PERCENT = 0.02f;

            // Act.
            var result = HealCases.CalculateHealthAfterPercentHeal(MAX_HEALTH, CURRENT_HEALTH, HEAL_PERCENT);

            // Assert.
            new { Health = result }.Should().Be(new { Health = 92f });
        }
    }
}