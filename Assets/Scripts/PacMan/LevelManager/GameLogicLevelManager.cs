using Moonthsoft.Core.Definitions.Scenes;
using Moonthsoft.Core.Definitions.Sounds;
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
        private readonly IAudioManager _audioManager;

        public int CurrentLevel { get { return _currentLevel; } }

        
        public GameLogicLevelManager(LevelManager levelmanager, int numPlayerLives, ILoadSceneManager loadSceneManager, IAudioManager audioManager)
        {
            _levelmanager = levelmanager;

            _numPlayerLives = numPlayerLives;

            _loadSceneManager = loadSceneManager;

            _audioManager = audioManager;
        }

        public void CompleteLevel()
        {
            _currentLevel++;

            ResetSateGame(true);

            _levelmanager.ResetItems();
        }

        public void PlayerDie()
        {
            _levelmanager.DeactiveMusic();

            _audioManager.PlayFx(Fx.PacManDeath);
            float durationSound = _audioManager.GetFxLenght(Fx.PacManDeath);
            Action action = GameOver;

            if (_numPlayerLives > 0)
            {
                _numPlayerLives--;
                action = () => ResetSateGame(false);
            }

            _levelmanager.FreezeGame(durationSound, action);
        }

        public void ResetSateGame(bool playMusic = false)
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

            float durationMusic = 1.5f;

            if (playMusic)
            {
                _audioManager.PlayFx(Fx.InitGame);
                durationMusic = _audioManager.GetFxLenght(Fx.InitGame);
            }

            Action activeMusic = () => _levelmanager.ActiveMusic();
            _levelmanager.FreezeGame(durationMusic, activeMusic);
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
