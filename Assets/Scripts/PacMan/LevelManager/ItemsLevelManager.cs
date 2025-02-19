using Moonthsoft.Core.Definitions.Sounds;
using Moonthsoft.Core.Managers;
using Moonthsoft.PacMan.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Zenject.CheatSheet;

namespace Moonthsoft.PacMan
{
    public class ItemsLevelManager
    {
        private readonly int SCORE_DOT;
        private readonly int SCORE_POWER_UP;
        private readonly int[] SCORE_EAT_GHOST;

        public event Action ActivePowerUpEvent;
        public event Action FinishingPowerUpEvent;
        public event Action ResetPowerUpEvent;
        public event Action DeactivePowerUpEvent;

        private readonly LevelManager _levelmanager;
        private readonly IAudioManager _audioManager;
        private LevelUI _ui;

        private int _numDots = 0;
        private readonly List<Dot> _dots = new();
        private readonly List<PowerUp> _powerUps = new();
        private bool _eatDotSound1 = true;
        private int _numGhostEated = 0;

        private IEnumerator _waitFinishPowerUpCoroutine = null;

        public ItemsLevelManager(LevelManager levelmanager, LevelUI ui, Configuration config, IAudioManager audioManager)
        {
            _levelmanager = levelmanager;

            _ui = ui;

            _audioManager = audioManager;

            SCORE_DOT = config.Score[(int)TypeScore.Dot];
            SCORE_POWER_UP = config.Score[(int)TypeScore.PowerUp];

            SCORE_EAT_GHOST = new int[4];
            SCORE_EAT_GHOST[0] = config.Score[(int)TypeScore.Ghost1];
            SCORE_EAT_GHOST[1] = config.Score[(int)TypeScore.Ghost2];
            SCORE_EAT_GHOST[2] = config.Score[(int)TypeScore.Ghost3];
            SCORE_EAT_GHOST[3] = config.Score[(int)TypeScore.Ghost4];
        }

        public void AddDot(Dot dot)
        {
            _numDots++;
            _dots.Add(dot);
        }

        public void RemoveDot(Action completeLevel)
        {
            EatDotSound();

            _numDots--;

            _levelmanager.AddScore(SCORE_DOT);

            //TODO:
            //Blinky angry

            if (_numDots <= 0)
            {
                _levelmanager.DeactiveAllMusic();

                _levelmanager.FreezeGame(2f, completeLevel);
            }
        }

        public void AddPowerUp(PowerUp powerUp)
        {
            _powerUps.Add(powerUp);
        }

        public void ActivePowerUp(Configuration config, int currentLevel)
        {
            EatDotSound();

            _levelmanager.AddScore(SCORE_POWER_UP);

            _numGhostEated = 0;

            if (currentLevel < config.LastLevel)
            {
                ActivePowerUpEvent?.Invoke();

                _levelmanager.ActiveMusic(true);

                if (_waitFinishPowerUpCoroutine != null)
                {
                    ResetPowerUpEvent?.Invoke();
                    _levelmanager.StopCoroutine(_waitFinishPowerUpCoroutine);
                }

                _levelmanager.StartCoroutine(_waitFinishPowerUpCoroutine = WaitFinishPowerUpCoroutine(config));
            }
        }

        public void ResetItems()
        {
            _numDots = _dots.Count;

            for (int i = 0; i < _dots.Count; ++i)
            {
                _dots[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < _powerUps.Count; ++i)
            {
                _powerUps[i].gameObject.SetActive(true);
            }
        }

        public void EatGhost(Vector3 pos)
        {
            _audioManager.PlayFx(Fx.EatGhost);

            if (_numGhostEated < SCORE_EAT_GHOST.Length)
            {
                int score = SCORE_EAT_GHOST[_numGhostEated];
                _numGhostEated++;

                _levelmanager.AddScore(score);
                _ui.ActiveScoreEatGhost(score, pos);
            }
            else
            {
                Debug.LogError("_numGhostEated is greater than " + SCORE_EAT_GHOST.Length);
            }

            _levelmanager.FreezeGame(0.5f);
        }

        private void EatDotSound()
        {
            var clip = _eatDotSound1 ? Fx.EatDot1 : Fx.EatDot2;

            _eatDotSound1 = !_eatDotSound1;

            _audioManager.PlayFx(clip);
        }

        private IEnumerator WaitFinishPowerUpCoroutine(Configuration config)
        {
            int indx = _levelmanager.GetIndex(config.DurationPowerUp.Length);
            float timeAux = config.DurationPowerUp[indx];

            timeAux -= config.TimeBlinkPowerUp;

            if (timeAux > 0f)
            {
                yield return new WaitForSeconds(timeAux);
            }

            FinishingPowerUpEvent?.Invoke();

            yield return new WaitForSeconds(config.TimeBlinkPowerUp);

            DeactivePowerUpEvent?.Invoke();

            _levelmanager.ActiveMusic(false);

            _numGhostEated = 0;

            _waitFinishPowerUpCoroutine = null;
        }
    }
}
