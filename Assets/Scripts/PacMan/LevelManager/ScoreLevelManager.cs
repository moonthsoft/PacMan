using Moonthsoft.Core.Managers;
using Moonthsoft.PacMan.Config;
using UnityEngine;
using static Zenject.CheatSheet;

namespace Moonthsoft.PacMan
{
    public class ScoreLevelManager
    {
        private readonly LevelUI _ui;
        private readonly IDataManager _dataManager;

        public ScoreLevelManager(LevelUI ui, IDataManager dataManager)
        {
            _ui = ui;

            _dataManager = dataManager;
        }

        public void AddScore(int score)
        {
            _dataManager.AddScore(score);

            UploadUI();
        }

        public void ResetScore()
        {
            _dataManager.ResetScore();

            UploadUI();
        }

        private void UploadUI()
        {
            _ui.Set1UPScore(_dataManager.Score);
            _ui.SetHighScore(_dataManager.HighScore);

            _ui.Active2Players(false);
        }
    }
}
