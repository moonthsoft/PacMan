using Moonthsoft.Core.Definitions.Sounds;
using UnityEngine;

namespace Moonthsoft.Core.Managers
{
    public interface IDataManager
    {
        public int Score { get; }
        public int HighScore { get; }

        public void AddScore(int score);
        public void ResetScore();
    }
}