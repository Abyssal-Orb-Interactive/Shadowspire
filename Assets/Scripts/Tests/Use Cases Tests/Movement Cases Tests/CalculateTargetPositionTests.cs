using FluentAssertions;
using NUnit.Framework;
using Unity.Mathematics;
using UseCases;
using SetUp = UseCasesTests.MovementModelTests.TestsScriptLanguage.SetUp;

namespace UseCasesTests.MovementModelTests
{
    public class CalculateTargetPositionTests
    {
        [Test]
        public void WhenCalculateTargetPosition_AndPositionIsZeroZeroAndOffsetIsOneOne_ThenShouldBeOneOne()
        {
            // Arrange.
            var rigidbody2D = SetUp.Rigidbody2DWithPosition();
            var offset = new float2(1f, 1f);

            // Act.
            var result = MovementCases.CalculateTargetPosition(rigidbody2D, offset);

            // Assert.
            new { TargetPosition = result }.Should().Be(new { TargetPosition = new float2(1f, 1f) });
        }
    }
}