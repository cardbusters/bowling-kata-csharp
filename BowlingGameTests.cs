using System;
using Xunit;

namespace bowling_kata_csharp1
{
    public class BowlingGameTests
    {
        private readonly BowlingGame _bowlingGame;

        public BowlingGameTests()
        {
            _bowlingGame = new BowlingGame();
        }

        [Fact]
        public void GivenFrameWithZeroZeroThenScoreShouldBeZero()
        {
            ExecutingThrow(firstThrow: 0, secondThrow: 0, expectedScore: 0);
        }

        [Fact]
        public void GivenFrameWithOneAndZeroThenScoreShouldBeOne()
        {
            ExecutingThrow(firstThrow: 1, secondThrow: 0, expectedScore: 1);
        }

        [Fact]
        public void GivenFrameWithZeroAndOneThenScoreShouldBeOne()
        {
            ExecutingThrow(firstThrow: 0, secondThrow: 1, expectedScore: 1);
        }

        private void ExecutingThrow(int firstThrow, int secondThrow, int expectedScore)
        {
            _bowlingGame.OpenFrame(firstThrow, secondThrow);

            var actualScore = _bowlingGame.Score();

            Assert.Equal(expectedScore, actualScore);
        }

        [Fact]
        public void GivenFrameTotalIsMoreThenTenThenArgumentOutOfRangeExceptionShouldBeThrown()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { _bowlingGame.OpenFrame(8, 9); });
        }

        [Fact]
        public void GivenTwoFramesWithoutStrikeAndSpareThenScoreShouldBeSumOfAllPins()
        {
            _bowlingGame.OpenFrame(6, 3);
            _bowlingGame.OpenFrame(3, 3);

            var expectedScore = 15;
            var actualScore = _bowlingGame.Score();
            Assert.Equal(expectedScore, actualScore);
        }

        [Fact]
        public void GivenTwoFramesWithStrikeAndThreeAndTwoThenScoreShouldTwenty()
        {
            _bowlingGame.OpenFrame(10, 0);
            _bowlingGame.OpenFrame(3, 2);

            var expectedScore = 20;
            var actualScore = _bowlingGame.Score();
            Assert.Equal(expectedScore, actualScore);
        }

        [Fact]
        public void GivenTwoFramesWithSpareAndFourAndOneThenScoreShouldBeNineTeen()
        {
            _bowlingGame.OpenFrame(5, 5);
            _bowlingGame.OpenFrame(4, 1);

            var expectedScore = 19;
            var actualScore = _bowlingGame.Score();
            Assert.Equal(expectedScore, actualScore);
        }

        [Theory]
        [InlineData(10, 0, 10, 0, 3, 2, 43)]
        [InlineData(2, 5, 10, 0, 1, 0, 19)]
        public void TestsWithThreeFrames(int frame11, int frame12, int frame21, int frame22, int frame31, int frame32, int expectedScore)
        {
            _bowlingGame.OpenFrame(frame11, frame12);
            _bowlingGame.OpenFrame(frame21, frame22);
            _bowlingGame.OpenFrame(frame31, frame32);

            var actualScore = _bowlingGame.Score();
            Assert.Equal(expectedScore, actualScore);
        }

        [Theory]
        [InlineData(1, 0, 1, 9, 10, 0, 1, 0, 33)]
        [InlineData(1, 0, 1, 0, 1, 0, 10, 0, 13)]
        public void TestsWithFourFrames(int frame11, int frame12, int frame21, int frame22, int frame31, int frame32, int frame41, int frame42, int expectedScore)
        {
            _bowlingGame.OpenFrame(frame11, frame12);
            _bowlingGame.OpenFrame(frame21, frame22);
            _bowlingGame.OpenFrame(frame31, frame32);
            _bowlingGame.OpenFrame(frame41, frame42);

            var actualScore = _bowlingGame.Score();
            Assert.Equal(expectedScore, actualScore);
        }
    }
}
