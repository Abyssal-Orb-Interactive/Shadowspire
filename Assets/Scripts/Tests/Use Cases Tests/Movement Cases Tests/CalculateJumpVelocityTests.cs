using FluentAssertions;
using NUnit.Framework;
using UseCases;

namespace UseCasesTests.MovementModelTests
{
    public class CalculateJumpVelocityTests
    {
        [Test]
        public void WhenCalculateJumpVelocity_AndGravityIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float GRAVITY = -1f;
            const float HEIGHT = 2f;

            // Act.
            var result = MovementCases.CalculateJumpVelocity(GRAVITY, HEIGHT);

            // Assert.
            new {JumpVelocity = result}.Should().Be(new {JumpVelocity = 0f});
        }
        
        [Test]
        public void WhenCalculateJumpVelocity_AndGravityIsZero_ThenShouldBeZero()
        {
            // Arrange.
            const float GRAVITY = 0f;
            const float HEIGHT = 2f;

            // Act.
            var result = MovementCases.CalculateJumpVelocity(GRAVITY, HEIGHT);

            // Assert.
            new {JumpVelocity = result}.Should().Be(new {JumpVelocity = 0f});
        }
        
        [Test]
        public void WhenCalculateJumpVelocity_AndHeightIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float GRAVITY = 10f;
            const float HEIGHT = -1f;

            // Act.
            var result = MovementCases.CalculateJumpVelocity(GRAVITY, HEIGHT);

            // Assert.
            new {JumpVelocity = result}.Should().Be(new {JumpVelocity = 0f});
        }
        
        [Test]
        public void WhenCalculateJumpVelocity_AndHeightIsZero_ThenShouldBeZero()
        {
            // Arrange.
            const float GRAVITY = 10f;
            const float HEIGHT = 0f;

            // Act.
            var result = MovementCases.CalculateJumpVelocity(GRAVITY, HEIGHT);

            // Assert.
            new {JumpVelocity = result}.Should().Be(new {JumpVelocity = 0f});
        }
        
        [Test]
        public void WhenCalculateJumpVelocity_AndHeightIsTwoAndGravityIsOne_ThenShouldBeTwo()
        {
            // Arrange.
            const float GRAVITY = 1f;
            const float HEIGHT = 2f;

            // Act.
            var result = MovementCases.CalculateJumpVelocity(GRAVITY, HEIGHT);

            // Assert.
            new {JumpVelocity = result}.Should().Be(new {JumpVelocity = 2f});
        }
    }
}