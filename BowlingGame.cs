using System;
using System.Collections.Generic;
using System.Linq;

namespace bowling_kata_csharp1
{
    internal class BowlingGame
    {

        private List<Frame> _frameHistory = new List<Frame>();
        internal void OpenFrame(int firstThrow, int secondThrow)
        {
            var frame = Frame.initializeFrame(firstThrow, secondThrow);
            _frameHistory.Add(frame);
        }

        internal int Score()
        {
            //TODO: move this logic to the OpenFrame method
            //===========>>
            const int frameIndex = 0;

            if (_frameHistory[frameIndex].IsStrike()) //first throw is strike
            {
                if (_frameHistory[frameIndex + 1].IsStrike()) //second throw is also strike
                {
                    var newScore = _frameHistory[frameIndex].Score + _frameHistory[frameIndex + 1].Score + _frameHistory[frameIndex + 2].ThrowOne;
                    _frameHistory[frameIndex] = new Frame(_frameHistory[frameIndex].ThrowOne, _frameHistory[frameIndex].ThrowTwo, newScore);

                    newScore = _frameHistory[frameIndex + 1].Score + _frameHistory[frameIndex + 2].Score;
                    _frameHistory[frameIndex + 1] = new Frame(_frameHistory[frameIndex + 1].ThrowOne, _frameHistory[frameIndex + 1].ThrowTwo, newScore);
                }
                else //second throw is not a strike
                {
                    var newScore = _frameHistory[frameIndex].Score + _frameHistory[frameIndex + 1].Score;
                    _frameHistory[frameIndex] = new Frame(_frameHistory[frameIndex].ThrowOne, _frameHistory[frameIndex].ThrowTwo, newScore);
                }
            }

            if (_frameHistory[frameIndex].IsSpare())
            {
                var newScore = _frameHistory[frameIndex].Score + _frameHistory[frameIndex + 1].ThrowOne;
                _frameHistory[frameIndex] = new Frame(_frameHistory[frameIndex].ThrowOne, _frameHistory[frameIndex].ThrowTwo, newScore);
            }
            //<<=============

            return _frameHistory.Sum(p => p.Score);
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

            internal bool IsSpare()
            {
                return ThrowTwo > 0 && Score == 10;
            }

            public static Frame initializeFrame(int firstThrow, int secondThrow)
            {
                int totalFrame = firstThrow + secondThrow;

                if (totalFrame > 10)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return new Frame(firstThrow, secondThrow, totalFrame);
            }
        }
    }
}
