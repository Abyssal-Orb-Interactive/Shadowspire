using FluentAssertions;
using NUnit.Framework;
using UseCases;

namespace UseCasesTests.HealthCasesTests
{
    public class CalculateHealthPercentTests
    {
        [Test]
        public void WhenCalculateHealthPercent_AndHealthIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float HEALTH = -1f;
            const float MAX_HEALTH = 100f;

            // Act.
            var result = HealthCases.CalculateHealthPercent(HEALTH, MAX_HEALTH);

            // Assert.
            new { HealthPercent = result }.Should().Be(new { HealthPercent = 0f });
        }
        
        
        [Test]
        public void WhenCalculateHealthPercent_AndHealthIsZero_ThenShouldBeZero()
        {
            // Arrange.
            const float HEALTH = 0f;
            const float MAX_HEALTH = 100f;

            // Act.
            var result = HealthCases.CalculateHealthPercent(HEALTH, MAX_HEALTH);

            // Assert.
            new { HealthPercent = result }.Should().Be(new { HealthPercent = 0f });
        }
        
        
        [Test]
        public void WhenCalculateHealthPercent_AndMaxHealthIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float HEALTH = 1f;
            const float MAX_HEALTH = -1f;

            // Act.
            var result = HealthCases.CalculateHealthPercent(HEALTH, MAX_HEALTH);

            // Assert.
            new { HealthPercent = result }.Should().Be(new { HealthPercent = 0f });
        }
        
        [Test]
        public void WhenCalculateHealthPercent_AndMaxHealthIsZero_ThenShouldBeZero()
        {
            // Arrange.
            const float HEALTH = 1f;
            const float MAX_HEALTH = 0f;

            // Act.
            var result = HealthCases.CalculateHealthPercent(HEALTH, MAX_HEALTH);

            // Assert.
            new { HealthPercent = result }.Should().Be(new { HealthPercent = 0f });
        }
        
        [Test]
        public void WhenCalculateHealthPercent_AndHealthBiggerThenMaxHealth_ThenShouldBeOne()
        {
            // Arrange.
            const float HEALTH = 200f;
            const float MAX_HEALTH = 100f;

            // Act.
            var result = HealthCases.CalculateHealthPercent(HEALTH, MAX_HEALTH);

            // Assert.
            new { HealthPercent = result }.Should().Be(new { HealthPercent = 1f });
        }
        
        [Test]
        public void WhenCalculateHealthPercent_AndHealthIsTenAndMaxHealthIsHundred_ThenShouldBeZeroPointOne()
        {
            // Arrange.
            const float HEALTH = 10f;
            const float MAX_HEALTH = 100f;

            // Act.
            var result = HealthCases.CalculateHealthPercent(HEALTH, MAX_HEALTH);

            // Assert.
            new { HealthPercent = result }.Should().Be(new { HealthPercent = 0.1f });
        }
    }
}