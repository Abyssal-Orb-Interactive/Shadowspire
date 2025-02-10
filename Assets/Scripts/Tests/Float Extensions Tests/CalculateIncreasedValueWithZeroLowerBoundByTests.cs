using FloatExtensions;
using FluentAssertions;
using NUnit.Framework;

namespace FloatExtensionsTests
{
    public class CalculateIncreasedValueWithZeroLowerBoundByTests
    {
        [Test]
        public void WhenValueCalculateIncreasedValueWithZeroLowerBoundBy_AndValueIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const float VALUE = -1f;
            const float OPERAND = 0.1f;

            // Act.
            var result = VALUE.CalculateIncreasedValueWithZeroLowerBoundBy(OPERAND);

            // Assert.
            new {ElapsedTime = result}.Should().Be(new {ElapsedTime = 0f});
        }
        
        [Test]
        public void WhenValueCalculateIncreasedValueWithZeroLowerBoundBy_AndOperandIsNegative_ThenShouldBeTimer()
        {
            // Arrange.
            const float VALUE = 0f;
            const float OPERAND = -0.1f;

            // Act.
            var result = VALUE.CalculateIncreasedValueWithZeroLowerBoundBy(OPERAND);

            // Assert.
            new {ElapsedTime = result}.Should().Be(new {ElapsedTime = VALUE});
        }
        
        [Test]
        public void WhenValueCalculateIncreasedValueWithZeroLowerBoundBy_AndOperandIsZero_ThenShouldBeTimer()
        {
            // Arrange.
            const float VALUE = 0f;
            const float OPERAND = 0f;

            // Act.
            var result = VALUE.CalculateIncreasedValueWithZeroLowerBoundBy(OPERAND);

            // Assert.
            new {ElapsedTime = result}.Should().Be(new {ElapsedTime = VALUE});
        }
        
        [Test]
        public void WhenValueCalculateIncreasedValueWithZeroLowerBoundBy_AndOperandIsOneAndTimerIsZero_ThenShouldBeOne()
        {
            // Arrange.
            const float VALUE = 0f;
            const float OPERAND = 1f;

            // Act.
            var result = VALUE.CalculateIncreasedValueWithZeroLowerBoundBy(OPERAND);

            // Assert.
            new {ElapsedTime = result}.Should().Be(new {ElapsedTime = 1f});
        }
    }
}