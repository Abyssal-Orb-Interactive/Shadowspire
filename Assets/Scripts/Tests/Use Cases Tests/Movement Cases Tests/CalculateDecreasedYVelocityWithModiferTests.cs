using FluentAssertions;
using NUnit.Framework;
using Unity.Mathematics;
using UseCases;

namespace UseCasesTests.MovementModelTests
{
    public class CalculateDecreasedYVelocityWithModiferTests
    {
        [Test]
        public void WhenCalculateDecreasedYVelocityWithModifer_AndModifierIsNegative_ThenShouldBeVelocity()
        {
            // Arrange.
            var velocity = new float2(0f, 0f);
            const float MODIFIER = -0.5f;

            // Act.
            var result = MovementCases.CalculateDecreasedYVelocityWithModifer(velocity, MODIFIER);

            // Assert.
            new { Velocity = result }.Should().Be(new { Velocity = velocity });
        }
        
        [Test]
        public void WhenCalculateDecreasedYVelocityWithModifer_AndModifierIsZero_ThenShouldBeVelocity()
        {
            // Arrange.
            var velocity = new float2(0f, 0f);
            const float MODIFIER = 0f;

            // Act.
            var result = MovementCases.CalculateDecreasedYVelocityWithModifer(velocity, MODIFIER);

            // Assert.
            new { Velocity = result }.Should().Be(new { Velocity = velocity });
        }
        
        [Test]
        public void WhenCalculateDecreasedYVelocityWithModifer_AndModifierIsZeroDotFiveAndVelocityIsZeroTwo_ThenShouldBeZeroOne()
        {
            // Arrange.
            var velocity = new float2(0f, 2f);
            const float MODIFIER = 0.5f;

            // Act.
            var result = MovementCases.CalculateDecreasedYVelocityWithModifer(velocity, MODIFIER);

            // Assert.
            new { Velocity = result }.Should().Be(new { Velocity = new float2(0f, 1f) });
        }
    }
}