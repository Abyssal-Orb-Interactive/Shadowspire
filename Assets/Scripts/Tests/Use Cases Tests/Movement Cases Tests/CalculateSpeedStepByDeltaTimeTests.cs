using FluentAssertions;
using NUnit.Framework;
using UseCases;

namespace UseCasesTests.MovementModelTests
{
    public sealed class CalculateSpeedStepByDeltaTimeTests
    {
        [Test]
        public void WhenCalculateSpeedStepByDeltaTime_AndDeltaTimeIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float SPEED = 100f;
            const float DELTA_TIME = -1f;

            // Act.
            var result = MovementCases.CalculateSpeedStepByDeltaTime(SPEED, DELTA_TIME);

            // Assert.
            new { Speed = result }.Should().Be(new { Speed = 0f });
        }
        
        [Test]
        public void WhenCalculateSpeedStepByDeltaTime_AndDeltaTimeIsZeroDotZeroOneAndSpeedIsHundred_ThenShouldBeOne()
        {
            // Arrange.
            const float SPEED = 100f;
            const float DELTA_TIME = 0.01f;

            // Act.
            var result = MovementCases.CalculateSpeedStepByDeltaTime(SPEED, DELTA_TIME);

            // Assert.
            new { Speed = result }.Should().Be(new { Speed = 1f });
        }
    }
}