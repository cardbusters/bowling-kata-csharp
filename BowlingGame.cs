using System;
using System.Collections.Generic;
using System.Linq;

namespace bowling_kata_csharp1
{
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
