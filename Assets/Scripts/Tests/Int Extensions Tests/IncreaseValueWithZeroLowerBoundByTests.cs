using FluentAssertions;
using IntExtensions;
using NUnit.Framework;

namespace IntExtensionsTests
{
    public class IncreaseValueWithZeroLowerBoundByTests
    {
        [Test]
        public void WhenIncreaseValueWithZeroLowerBoundBy_AndValueIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            const int VALUE = -1;
            const int AMOUNT = 1;

            // Act.
            var result = VALUE.IncreaseValueWithZeroLowerBoundBy(AMOUNT);

            // Assert.
            new {Value = result}.Should().Be(new {Value = 0});
        }
        
        [Test]
        public void WhenIncreaseValueWithZeroLowerBoundBy_AndAmountIsNegative_ThenShouldBeValue()
        {
            // Arrange.
            const int VALUE = 1;
            const int AMOUNT = -1;

            // Act.
            var result = VALUE.IncreaseValueWithZeroLowerBoundBy(AMOUNT);

            // Assert.
            new {Value = result}.Should().Be(new {Value = VALUE});
        }
        
        [Test]
        public void WhenIncreaseValueWithZeroLowerBoundBy_AndAmountIsOneAndValueIsOne_ThenShouldBeTwo()
        {
            // Arrange.
            const int VALUE = 1;
            const int AMOUNT = 1;

            // Act.
            var result = VALUE.IncreaseValueWithZeroLowerBoundBy(AMOUNT);

            // Assert.
            new {Value = result}.Should().Be(new {Value = 2});
        }
    }
}