using FloatExtensions;
using FluentAssertions;
using NUnit.Framework;

namespace FloatExtensionsTests
{
    public class CalculateElapsedTimeByTests
    {
        [Test]
        public void WhenTimerCalculateElapsedTimeBy_AndTimerIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float TIMER = -1f;
            const float TIME_DELTA = 0.1f;

            // Act.
            var result = TIMER.CalculateElapsedTimeBy(TIME_DELTA);

            // Assert.
            new {ElapsedTime = result}.Should().Be(new {ElapsedTime = 0f});
        }
        
        [Test]
        public void WhenTimerCalculateElapsedTimeBy_AndTimeDeltaIsNegative_ThenShouldBeTimer()
        {
            // Arrange.
            const float TIMER = 0f;
            const float TIME_DELTA = -0.1f;

            // Act.
            var result = TIMER.CalculateElapsedTimeBy(TIME_DELTA);

            // Assert.
            new {ElapsedTime = result}.Should().Be(new {ElapsedTime = TIMER});
        }
        
        [Test]
        public void WhenTimerCalculateElapsedTimeBy_AndTimeDeltaIsZero_ThenShouldBeTimer()
        {
            // Arrange.
            const float TIMER = 0f;
            const float TIME_DELTA = 0f;

            // Act.
            var result = TIMER.CalculateElapsedTimeBy(TIME_DELTA);

            // Assert.
            new {ElapsedTime = result}.Should().Be(new {ElapsedTime = TIMER});
        }
        
        [Test]
        public void WhenTimerCalculateElapsedTimeBy_AndTimeDeltaIsOneAndTimerIsZero_ThenShouldBeOne()
        {
            // Arrange.
            const float TIMER = 0f;
            const float TIME_DELTA = 1f;

            // Act.
            var result = TIMER.CalculateElapsedTimeBy(TIME_DELTA);

            // Assert.
            new {ElapsedTime = result}.Should().Be(new {ElapsedTime = 1f});
        }
    }
}