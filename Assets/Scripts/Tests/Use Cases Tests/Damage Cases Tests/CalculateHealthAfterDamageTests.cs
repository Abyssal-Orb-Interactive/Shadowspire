using FluentAssertions;
using NUnit.Framework;
using UseCases;

namespace Tests.UseCasesTests.DamageModelTests
{
    public class CalculateHealthAfterDamageTests
    {
        [Test]
        public void WhenCalculateHealthAfterDamageAndHealthIsNegativeThenResultShouldBeZero()
        {
            // Arrange
            const float HEALTH = -1f;
            const float DAMAGE = 1f;
            
            // Act
            var result = DamageCases.CalculateHealthAfterDamage(HEALTH, DAMAGE);
            
            // Assert
            new {Health = result}.Should().Be(new {Health = 0f});
        }
        
        [Test]
        public void WhenCalculateHealthAfterDamageAndHealthIsZeroThenResultShouldBeZero()
        {
            // Arrange
            const float HEALTH = 0f;
            const float DAMAGE = 1f;
            
            // Act
            var result = DamageCases.CalculateHealthAfterDamage(HEALTH, DAMAGE);
            
            // Assert
            new {Health = result}.Should().Be(new {Health = 0f});
        }
        
        [Test]
        public void WhenCalculateHealthAfterDamageAndDamageIsNegativeThenResultShouldBeHealth()
        {
            // Arrange
            const float HEALTH = 10f;
            const float DAMAGE = -1f;
            
            // Act
            var result = DamageCases.CalculateHealthAfterDamage(HEALTH, DAMAGE);
            
            // Assert
            new {Health = result}.Should().Be(new {Health = HEALTH});
        }
        
        [Test]
        public void WhenCalculateHealthAfterDamageAndDamageIsZeroThenResultShouldBeHealth()
        {
            // Arrange
            const float HEALTH = 10f;
            const float DAMAGE = 0f;
            
            // Act
            var result = DamageCases.CalculateHealthAfterDamage(HEALTH, DAMAGE);
            
            // Assert
            new {Health = result}.Should().Be(new {Health = HEALTH});
        }
        
        [Test]
        public void WhenCalculateHealthAfterDamageAndDamageIsTwoAndHealthIsOneThenResultShouldBeZero()
        {
            // Arrange
            const float HEALTH = 1f;
            const float DAMAGE = 2f;

            // Act
            var result = DamageCases.CalculateHealthAfterDamage(HEALTH, DAMAGE);

            // Assert
            new { Health = result }.Should().Be(new { Health = 0f });
        }

        [Test]
        public void WhenCalculateHealthAfterDamageAndDamageIsOneAndHealthIsTenThenResultShouldBeNine()
        {
            // Arrange
            const float HEALTH = 10f;
            const float DAMAGE = 1f;

            // Act
            var result = DamageCases.CalculateHealthAfterDamage(HEALTH, DAMAGE);

            // Assert
            new { Health = result }.Should().Be(new { Health = 9f });
        }
    }
}
