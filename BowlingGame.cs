using System;
using System.Collections.Generic;
using System.Linq;

namespace bowling_kata_csharp1
{
    internal class BowlingGame
    {

        private List<Frame> _score = new List<Frame>();
        internal void OpenFrame(int firstThrow, int secondThrow)
        {
            int totalFrame = firstThrow + secondThrow;

            if (totalFrame > 10)
            {
                throw new ArgumentOutOfRangeException();
            }
            _score.Add(new Frame(firstThrow, secondThrow, totalFrame));
        }

        internal int Score()
        {
            const int frameIndex = 0;
            if (_score[frameIndex].IsStrike())
            {
                if (_score[frameIndex + 1].IsStrike())
                {
                    int newScore;
                    CalculateScoreForStrikeFrame(frameIndex);

                    newScore = _score[frameIndex + 1].Score + _score[frameIndex + 2].Score;
                    _score[frameIndex + 1] = new Frame(_score[frameIndex + 1].ThrowOne, _score[frameIndex + 1].ThrowTwo, newScore);
                }
                else
                {
                    var newScore = _score[frameIndex].Score + _score[frameIndex + 1].Score;
                    _score[frameIndex] = new Frame(_score[frameIndex].ThrowOne, _score[frameIndex].ThrowTwo, newScore);
                }
            }

            return _score.Sum(p => p.Score);
        }

        private void CalculateScoreForStrikeFrame(int frameIndex)
        {
            var newScore = _score[frameIndex].Score + _score[frameIndex + 1].Score + _score[frameIndex + 2].ThrowOne;
            _score[frameIndex] = new Frame(_score[frameIndex].ThrowOne, _score[frameIndex].ThrowTwo, newScore);
        }

        private class Frame
        {
            public int ThrowOne;
            public int ThrowTwo;
            public int Score;

            public Frame(int firstThrow, int secondThrow, int totalFrame)
            {
                this.ThrowOne = firstThrow;
                this.ThrowTwo = secondThrow;
                this.Score = totalFrame;
            }

            internal bool IsStrike()
            {
                return ThrowOne == Score && Score == 10;
            }
        }
    }
}
