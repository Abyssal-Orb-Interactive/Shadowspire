using FluentAssertions;
using NUnit.Framework;
using UseCases;

namespace UseCasesTests.LerpCasesTests
{
    public class CalculateStepRatioTests
    {
        [Test]
        public void WhenCalculateStepRatio_AndDistanceIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float DISTANCE = -1f;
            const float MAX_STEP = 100f;

            // Act.
            var result = LerpCases.CalculateStepRatio(DISTANCE, MAX_STEP);

            // Assert.
            new { Step = result }.Should().Be(new { Step = 0f });
        }
        
        [Test]
        public void WhenCalculateStepRatio_AndDistanceIsZero_ThenShouldBeZero()
        {
            // Arrange.
            const float DISTANCE = 0f;
            const float MAX_STEP = 100f;

            // Act.
            var result = LerpCases.CalculateStepRatio(DISTANCE, MAX_STEP);

            // Assert.
            new { Step = result }.Should().Be(new { Step = 0f });
        }
        
        [Test]
        public void WhenCalculateStepRatio_AndMaxStepIsZero_ThenShouldBeZero()
        {
            // Arrange.
            const float DISTANCE = 1f;
            const float MAX_STEP = 0f;

            // Act.
            var result = LerpCases.CalculateStepRatio(DISTANCE, MAX_STEP);

            // Assert.
            new { Step = result }.Should().Be(new { Step = 0f });
        }
        
        [Test]
        public void WhenCalculateStepRatio_AndMaxStepIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float DISTANCE = 1f;
            const float MAX_STEP = -1f;

            // Act.
            var result = LerpCases.CalculateStepRatio(DISTANCE, MAX_STEP);

            // Assert.
            new { Step = result }.Should().Be(new { Step = 0f });
        }
        
        
        [Test]
        public void WhenCalculateStepRatio_AndMaxStepIsOneAndDistanceIsTen_ThenShouldBeZeroDotOne()
        {
            // Arrange.
            const float DISTANCE = 10f;
            const float MAX_STEP = 1f;

            // Act.
            var result = LerpCases.CalculateStepRatio(DISTANCE, MAX_STEP);

            // Assert.
            new { Step = result }.Should().Be(new { Step = 0.1f });
        }
    }
}