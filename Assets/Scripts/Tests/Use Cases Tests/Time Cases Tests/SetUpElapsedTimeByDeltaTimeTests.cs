using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UseCases;

namespace UseCasesTests.TimeCasesTests
{
    public class SetUpElapsedTimeByDeltaTimeTests
    {
        [Test]
        public void WhenSetUpElapsedTimeByDeltaTime_AndTimerIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            var timer = -1f;

            // Act.
            TimeCases.SetUpElapsedTimeByDeltaTime(ref timer);

            // Assert.
            new { ElapsedTime = timer }.Should().Be(new { ElapsedTime = 0f });
        }

        [Test]
        public void WhenSetUpElapsedTimeByDeltaTime_AndTimerIsZero_ThenShouldBeDeltaTime()
        {
            // Arrange.
            var timer = 0f;

            // Act.
            TimeCases.SetUpElapsedTimeByDeltaTime(ref timer);

            // Assert.
            new { ElapsedTime = timer }.Should().Be(new { ElapsedTime = Time.deltaTime });
        }
    }
}