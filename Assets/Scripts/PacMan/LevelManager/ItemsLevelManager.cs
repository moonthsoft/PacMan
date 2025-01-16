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

        private int _numDots = 0;
        private readonly List<Dot> _dots = new();
        private readonly List<PowerUp> _powerUps = new();

        private IEnumerator _waitFinishPowerUpCoroutine = null;

        public ItemsLevelManager(LevelManager levelmanager)
        {
            _levelmanager = levelmanager;
        }

        public void AddDot(Dot dot)
        {
            _numDots++;
            _dots.Add(dot);
        }

        public void RemoveDot(Action completeLevel)
        {
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
            if (currentLevel < config.LastLevel)
            {
                ActivePowerUpEvent?.Invoke();

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

            _waitFinishPowerUpCoroutine = null;
        }
    }
}
