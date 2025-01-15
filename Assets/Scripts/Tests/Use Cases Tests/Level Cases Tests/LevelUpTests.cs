using AtomicFramework.AtomicStructures;
using FluentAssertions;
using NUnit.Framework;
using UseCases;

namespace UseCasesTests.LevelCasesTests
{
    public class LevelUpTests
    {
        [Test]
        public void WhenLevelUp_AndLevelIsNegative_ThenShouldBeZero()
        {
            // Arrange.
            var level = new AtomicVariable<int>(-10);
            const int LEVELS_AMOUNT = 1;

            // Act.
            LevelCases.LevelUp(level, LEVELS_AMOUNT);

            // Assert.
            new { Level = level.CurrentValue }.Should().Be(new { Level = 0 });
        }
        
        [Test]
        public void WhenLevelUp_AndLevelsAmountIsNegative_ThenShouldBeLevel()
        {
            // Arrange.
            const int START_LEVEL = 1;
            var level = new AtomicVariable<int>(START_LEVEL);
            const int LEVELS_AMOUNT = -1;

            // Act.
            LevelCases.LevelUp(level, LEVELS_AMOUNT);

            // Assert.
            new { Level = level.CurrentValue }.Should().Be(new { Level = START_LEVEL});
        }
        
        [Test]
        public void WhenLevelUp_AndLevelsAmountIsOneANdLevelIsOne_ThenShouldBeTwo()
        {
            // Arrange.
            var level = new AtomicVariable<int>(1);
            const int LEVELS_AMOUNT = 1;

            // Act.
            LevelCases.LevelUp(level, LEVELS_AMOUNT);

            // Assert.
            new { Level = level.CurrentValue }.Should().Be(new { Level = 2 });
        }
    }
}