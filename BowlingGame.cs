using System;
using System.Collections.Generic;
using System.Linq;

namespace bowling_kata_csharp1
{
    internal class BowlingGame
    {

        private List<IFrame> _frameHistory = new List<IFrame>();
        internal void OpenFrame(int firstThrow, int secondThrow)
        {
            int totalFrame = firstThrow + secondThrow;

            if (totalFrame > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            IFrame frame;
            if (firstThrow == 10)
            {
                frame = new StrikeFrame(firstThrow, secondThrow, totalFrame);
                _frameHistory.Add(frame);
            }
            else if (firstThrow != 10 && totalFrame == 10){
                frame = new SpareFrame(firstThrow, secondThrow, totalFrame);
                _frameHistory.Add(frame);
            }
            else
            {
                frame = new Frame(firstThrow, secondThrow, totalFrame);
                _frameHistory.Add(frame);
            }
        }

        internal int Score()
        {
            for (int i = 0; i < _frameHistory.Count; i++)
            {
                RecalculateFrames(i);
            }

            return _frameHistory.Sum(p => p.Score);
        }

        private void RecalculateFrames(int frameIndex)
        {
            if (_frameHistory[frameIndex] is StrikeFrame) //first throw is strike
            {
                if (_frameHistory.Count > frameIndex + 1)
                {
                    if (_frameHistory[frameIndex + 1] is StrikeFrame) //second throw is also strike
                    {

                        var newScore = _frameHistory[frameIndex].Score + _frameHistory[frameIndex + 1].Score + _frameHistory[frameIndex + 2].ThrowOne;
                        _frameHistory[frameIndex] = new Frame(_frameHistory[frameIndex].ThrowOne, _frameHistory[frameIndex].ThrowTwo, newScore);

                        if (_frameHistory.Count > frameIndex + 2)
                        {
                            newScore = _frameHistory[frameIndex + 1].Score + _frameHistory[frameIndex + 2].Score;
                            _frameHistory[frameIndex + 1] = new Frame(_frameHistory[frameIndex + 1].ThrowOne, _frameHistory[frameIndex + 1].ThrowTwo, newScore);
                        }
                    }
                    else //second throw is not a strike
                    {
                        var newScore = _frameHistory[frameIndex].Score + _frameHistory[frameIndex + 1].Score;
                        _frameHistory[frameIndex] = new Frame(_frameHistory[frameIndex].ThrowOne, _frameHistory[frameIndex].ThrowTwo, newScore);
                    }
                }
            }

            if (_frameHistory[frameIndex] is SpareFrame && _frameHistory.Count > frameIndex + 1)
            {
                var newScore = _frameHistory[frameIndex].Score + _frameHistory[frameIndex + 1].ThrowOne;
                _frameHistory[frameIndex] = new Frame(_frameHistory[frameIndex].ThrowOne, _frameHistory[frameIndex].ThrowTwo, newScore);
            }
        }

        public abstract class IFrame
        {
            public int ThrowOne { get; }
            public int ThrowTwo { get; }
            public int Score { get; }
        }

        class StrikeFrame : IFrame
        {
            public int ThrowOne { get; }
            public int ThrowTwo { get; }
            public int Score { get; }

            public StrikeFrame(int firstThrow, int secondThrow, int totalFrame)
            {
                this.ThrowOne = firstThrow;
                this.ThrowTwo = secondThrow;
                this.Score = totalFrame;
            }
        }

        class SpareFrame : IFrame
        {
            public int ThrowOne { get; }
            public int ThrowTwo { get; }
            public int Score { get; }

            public SpareFrame(int firstThrow, int secondThrow, int totalFrame)
            {
                this.ThrowOne = firstThrow;
                this.ThrowTwo = secondThrow;
                this.Score = totalFrame;
            }
        }

        private class Frame : IFrame
        {
            public int ThrowOne { get; }
            public int ThrowTwo { get; }
            public int Score { get; }

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

        }
    }
}
