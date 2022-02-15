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
        public void GivenThreeFramesWithStrikeAndStrikeAndThreeAndTwoThenScoreShouldFourtyThree()
        {
            _bowlingGame.OpenFrame(10, 0); //X - 23
            _bowlingGame.OpenFrame(10, 0); //X - 15
            _bowlingGame.OpenFrame(3, 2);  //5 - 5

            var expectedScore = 43;
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

        [Fact]
        public void GivenThreeFramesTwoAndFiveAndStrikeAndOneThenScoreShouldBeNineTeen()
        {
            _bowlingGame.OpenFrame(2, 5);
            _bowlingGame.OpenFrame(10, 0);
            _bowlingGame.OpenFrame(1, 0);

            var expectedScore = 19;
            var actualScore = _bowlingGame.Score();
            Assert.Equal(expectedScore, actualScore);
        }
    }
}
