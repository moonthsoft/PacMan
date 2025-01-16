using Moonthsoft.Core.Definitions.Scenes;
using Moonthsoft.Core.Managers;
using System;
using UnityEngine;
using Zenject;

namespace Moonthsoft.PacMan
{
    public class GameLogicLevelManager
    {
        private readonly LevelManager _levelmanager;

        private int _numPlayerLives = 0;
        private int _currentLevel = 0;

        private ILoadSceneManager _loadSceneManager;

        public int CurrentLevel { get { return _currentLevel; } }

        
        public GameLogicLevelManager(LevelManager levelmanager, int numPlayerLives, ILoadSceneManager loadSceneManager)
        {
            _levelmanager = levelmanager;

            _numPlayerLives = numPlayerLives;

            _loadSceneManager = loadSceneManager;
        }

        public void CompleteLevel()
        {
            _currentLevel++;

            ResetSateGame();

            _levelmanager.ResetItems();
        }

        public void PlayerDie()
        {
            if (_numPlayerLives > 0)
            {
                _numPlayerLives--;

                _levelmanager.FreezeGame(1.5f, ResetSateGame);
            }
            else
            {
                _levelmanager.FreezeGame(1.5f, GameOver);
            }
        }

        public void ResetSateGame()
        {
            _levelmanager.Player.ResetCharacter();

            var blinky = _levelmanager.GetGhost(GhostType.Blinky);
            var pinky = _levelmanager.GetGhost(GhostType.Pinky);
            var inky = _levelmanager.GetGhost(GhostType.Inky);
            var clyde = _levelmanager.GetGhost(GhostType.Clyde);

            blinky.ResetCharacter();
            pinky.ResetCharacter();
            inky.ResetCharacter();
            clyde.ResetCharacter();

            InitGhostStateController(blinky);
            InitGhostStateController(pinky);
            InitGhostStateController(inky);
            InitGhostStateController(clyde);

            _levelmanager.ActiveTimer();

            _levelmanager.FreezeGame(1.5f);
        }

        private void GameOver()
        {
            //TODO:
            //chango to load credits scene

            Time.timeScale = 0f;

            _loadSceneManager.LoadScene(Scenes.Game);
        }

        private void InitGhostStateController(Ghost ghost)
        {
            if (ghost != null)
            {
                ghost.InitStateController(new GhostStateController(ghost));
            }
        }
    }
}
