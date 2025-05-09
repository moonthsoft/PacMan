using System;
using UnityEngine;
using Moonthsoft.Core.Definitions.Scenes;
using Moonthsoft.Core.Definitions.Sounds;
using Moonthsoft.Core.Managers;
using Moonthsoft.PacMan.Config;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Level manager subclass in charge of the logic of the game, such as restarting the level when Pac-Man dies, 
    /// or completing it when all the points are collected.
    /// </summary>
    public class GameLogicLevelManager
    {
        private readonly LevelManager _levelmanager;
        private readonly IAudioManager _audioManager;
        private readonly ILoadSceneManager _loadSceneManager;
        private readonly LevelUI _ui;

        private int _numPlayerLives = 0;
        private int _currentLevel = 0;

        public int CurrentLevel { get { return _currentLevel; } }

        
        public GameLogicLevelManager(LevelManager levelmanager, LevelUI ui, Configuration config, ILoadSceneManager loadSceneManager, IAudioManager audioManager)
        {
            _levelmanager = levelmanager;

            _ui = ui;

            _numPlayerLives = config.NumPlayerLives;

            _currentLevel = config.FirstLevel;

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
            _levelmanager.DeactiveAllMusic();

            _audioManager.PlayFx(Fx.PacManDeath);

            float durationSound = _audioManager.GetFxLenght(Fx.PacManDeath);
            Action action = GameOver;

            if (_numPlayerLives > 0)
            {
                _numPlayerLives--;
                action = () => ResetSateGame(false);
            }

            _levelmanager.Player.ActiveAnimationDie();

            var ghosts = _levelmanager.GetGhosts();
            for (int i = 0; i < ghosts.Length; ++i)
            {
                ghosts[i].ActiveSprite(false);
            }

            _levelmanager.FreezeGame(durationSound, action);
        }

        public void ResetSateGame(bool playMusic = false)
        {
            _levelmanager.Player.ResetCharacter();

            var ghosts = _levelmanager.GetGhosts();
            for (int i = 0; i < ghosts.Length; ++i)
            {
                ghosts[i].ResetCharacter();

                InitGhostStateController(ghosts[i]);
            }

            _levelmanager.ActiveTimer();

            float durationMusic = 1.5f;

            if (playMusic)
            {
                _audioManager.PlayFx(Fx.InitGame);
                durationMusic = _audioManager.GetFxLenght(Fx.InitGame);
            }

            _ui.SetLevelUI(_currentLevel, _numPlayerLives);
            _ui.ActiveReadyText(true);

            Action action = () => _levelmanager.ActiveMusic();
            action += () => _ui.ActiveReadyText(false);
            _levelmanager.FreezeGame(durationMusic, action);
        }

        private void GameOver()
        {
            Time.timeScale = 0f;

            //TODO:
            //change to load credits scene
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
