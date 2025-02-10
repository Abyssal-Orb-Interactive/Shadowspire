using FluentAssertions;
using NUnit.Framework;
using Unity.Mathematics;
using UseCases;

namespace UseCasesTests.MovementModelTests
{
    public class CalculateIncreasedYVelocityWithModiferTests
    {
        [Test]
        public void WhenCalculateIncreasedYVelocityWithModifer_AndModifierIsNegative_ThenShouldBeVelocity()
        {
            // Arrange.
            var velocity = new float2(0f, 0f);
            const float MODIFIER = -0.5f;

            // Act.
            var result = MovementCases.CalculateIncreasedYVelocityWithModifer(velocity, MODIFIER);

            // Assert.
            new { Velocity = result }.Should().Be(new { Velocity = velocity });
        }
        
        [Test]
        public void WhenCalculateIncreasedYVelocityWithModifer_AndModifierIsZero_ThenShouldBeVelocity()
        {
            // Arrange.
            var velocity = new float2(0f, 0f);
            const float MODIFIER = 0f;

            // Act.
            var result = MovementCases.CalculateIncreasedYVelocityWithModifer(velocity, MODIFIER);

            // Assert.
            new { Velocity = result }.Should().Be(new { Velocity = velocity });
        }
        
        [Test]
        public void WhenCalculateIncreasedYVelocityWithModifer_AndModifierIsZeroDotFiveAndVelocityIsZeroTwo_ThenShouldBeZeroThree()
        {
            // Arrange.
            var velocity = new float2(0f, 2f);
            const float MODIFIER = 0.5f;

            // Act.
            var result = MovementCases.CalculateIncreasedYVelocityWithModifer(velocity, MODIFIER);

            // Assert.
            new { Velocity = result }.Should().Be(new { Velocity = new float2(0f, 3f) });
        }
    }
}