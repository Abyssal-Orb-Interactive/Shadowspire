using FluentAssertions;
using NUnit.Framework;
using Unity.Mathematics;
using UseCases;

namespace UseCasesTests.MovementModelTests
{
    public class CalculateOffsetTests
    {
        [Test]
        public void WhenCalculateOffset_AndSpeedIsNegativeAndDirectionIsOneOneAndDeltaTimeIsOne_ThenShouldBeOneOne()
        {
            // Arrange.
            const float SPEED = -1f;
            const float DELTA_TIME = 1f;
            var direction = new float2(1f, 1f);

            // Act.
            var result = MovementCases.CalculateOffset(direction, SPEED, DELTA_TIME);
            
            // Assert.
            new{Offset = result}.Should().Be(new{Offset = new float2(1f, 1f)});
        }
        
        [Test]
        public void WhenCalculateOffset_AndSpeedIsOneAndDirectionIsOneOneAndDimeDeltaIsOne_ThenShouldBeOneOne()
        {
            // Arrange.
            const float SPEED = 1f;
            const float DELTA_TIME = 1f;
            var direction = new float2(1f, 1f);

            // Act.
            var result = MovementCases.CalculateOffset(direction, SPEED, DELTA_TIME);
            
            // Assert.
            new{Offset = result}.Should().Be(new{Offset = new float2(1f, 1f)});
        }
    }
}