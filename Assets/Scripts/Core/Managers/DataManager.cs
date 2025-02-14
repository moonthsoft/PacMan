using UnityEngine;
using static Zenject.CheatSheet;


namespace Moonthsoft.Core.Managers
{
    public class DataManager : MonoBehaviour, IDataManager
    {
        private int _score = 0;
        private int _highScore = 0;

        public int Score { get { return _score; } }

        public int HighScore { get { return _highScore; } }

        public void AddScore(int score)
        {
            _score += score;

            if (_score > _highScore)
            {
                _highScore = _score;
            }
        }

        public void ResetScore()
        {
            _score = 0;
        }
    }
}