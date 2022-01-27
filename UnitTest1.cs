using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace bowling_kata_csharp1
{
    public class UnitTest1
    {
        private readonly BowlingGame _bowlingGame;

        public UnitTest1()
        {
            _bowlingGame = new BowlingGame();
        }

        [Fact]
        public void GivenFrameWithZeroZeroThenScoreShouldBeZero()
        {
            _bowlingGame.OpenFrame(0, 0);

            var expectedScore = 0;
            var actualScore = _bowlingGame.Score();

            Assert.Equal(expectedScore, actualScore);
        }

        [Fact]
        public void GivenFrameWithOneAndZeroThenScoreShouldBeOne()
        {
            _bowlingGame.OpenFrame(1, 0);

            var expectedScore = 1;
            var actualScore = _bowlingGame.Score();

            Assert.Equal(expectedScore, actualScore);
        }

        [Fact]
        public void GivenFrameWithZeroAndOneThenScoreShouldBeOne()
        {
            _bowlingGame.OpenFrame(0, 1);

            var expectedScore = 1;
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
    }

    internal class BowlingGame
    {
        private List<Tuple<int, int, int>> _score = new List<Tuple<int, int, int>>();
        internal void OpenFrame(int firstThrow, int secondThrow)
        {
            int totalFrame = firstThrow + secondThrow;

            if (totalFrame > 10)
            {
                throw new ArgumentOutOfRangeException();
            }
            _score.Add(new Tuple<int, int, int>(firstThrow, secondThrow, totalFrame));
        }

        internal int Score()
        {
            if (IsStrike(0))
            {
                if (IsStrike(1))
                {
                    var newScore = _score[0].Item3 + _score[1].Item3 + _score[2].Item1;
                    _score[0] = new Tuple<int, int, int>(_score[0].Item1, _score[0].Item2, newScore);

                    newScore = _score[1].Item3 + _score[2].Item3;
                    _score[1] = new Tuple<int, int, int>(_score[1].Item1, _score[1].Item2, newScore);
                }
                else
                {
                    var newScore = _score[0].Item3 + _score[1].Item3;
                    _score[0] = new Tuple<int, int, int>(_score[0].Item1, _score[0].Item2, newScore);
                }
            }

            return _score.Sum(p => p.Item3);
        }

        private bool IsStrike(int frameIndex)
        {
            return _score[frameIndex].Item1 == _score[frameIndex].Item3 && _score[frameIndex].Item3 == 10;
        }
    }
}
