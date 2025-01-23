using Moonthsoft.Core.Definitions.Sounds;
using Moonthsoft.Core.Managers;
using Moonthsoft.PacMan.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    public class ItemsLevelManager
    {
        public event Action ActivePowerUpEvent;
        public event Action FinishingPowerUpEvent;
        public event Action ResetPowerUpEvent;
        public event Action DeactivePowerUpEvent;

        private readonly LevelManager _levelmanager;
        private readonly IAudioManager _audioManager;

        private int _numDots = 0;
        private readonly List<Dot> _dots = new();
        private readonly List<PowerUp> _powerUps = new();
        private bool _eatDotSound1 = true;

        private IEnumerator _waitFinishPowerUpCoroutine = null;

        public ItemsLevelManager(LevelManager levelmanager, IAudioManager audioManager)
        {
            _levelmanager = levelmanager;

            _audioManager = audioManager;
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

            //TODO:
            //Blinky angry

            if (_numDots <= 0)
            {
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

            _waitFinishPowerUpCoroutine = null;
        }
    }
}
