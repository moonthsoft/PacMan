using UnityEngine;

namespace Moonthsoft.Core.Managers
{
    /// <summary>
    /// DataManager is responsible for managing persistent information between scenes.
    /// In this case it is responsible for the player's score, but in other projects it will be other information.
    /// It should also be responsible for saving/loading savedata, but since this is a simple project it is not implemented.
    /// </summary>
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