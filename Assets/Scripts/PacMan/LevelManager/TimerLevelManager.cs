using Moonthsoft.PacMan.Config;
using System;
using System.Collections;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    public class TimerLevelManager
    {
        public event Action ChangeChaseModeEvent;

        private readonly LevelManager _levelmanager;

        private float _totalTime = 0f;
        private int _iterationMode = 0;
        private bool _isChaseMode = false;

        private IEnumerator _iterationModeGhostCoroutine = null;
        private IEnumerator _increasesTotalTime = null;

        public bool IsChaseMode { get { return _isChaseMode; } }

        public TimerLevelManager(LevelManager levelmanager)
        {
            _levelmanager = levelmanager;
        }

        public void ActiveTimer(Configuration config)
        {
            if (_iterationModeGhostCoroutine != null)
            {
                _levelmanager.StopCoroutine(_iterationModeGhostCoroutine);
            }

            _levelmanager.StartCoroutine(_iterationModeGhostCoroutine = IterationModeGhostCoroutine(config));


            if (_increasesTotalTime != null)
            {
                _levelmanager.StopCoroutine(_increasesTotalTime);
            }

            _levelmanager.StartCoroutine(_increasesTotalTime = IncreasesTotalTime(config));
        }

        public bool CanGhostExitHome(Configuration config, GhostType type)
        {
            if (_totalTime >= config.TimeSpawnGhost[(int)type])
            {
                return true;
            }

            return false;
        }

        public void FreezeGame(float duration, Action action)
        {
            _levelmanager.StartCoroutine(FreezeGameCoroutine(duration, action));
        }

        private IEnumerator FreezeGameCoroutine(float duration, Action action)
        {
            Time.timeScale = 0f;

            yield return new WaitForSecondsRealtime(duration);

            Time.timeScale = 1f;

            action?.Invoke();
        }

        private IEnumerator IterationModeGhostCoroutine(Configuration config)
        {
            _iterationMode = 0;
            _isChaseMode = false;

            int indx = _levelmanager.GetIndex(config.TimeGhostMode.Matrix.Length);
            float[] timeMode = config.TimeGhostMode.Matrix[indx].array;

            while (_iterationMode < timeMode.Length)
            {
                yield return new WaitForSeconds(timeMode[_iterationMode]);

                _iterationMode++;

                _isChaseMode = !_isChaseMode;

                ChangeChaseModeEvent?.Invoke();
            }

            _iterationModeGhostCoroutine = null;
        }

        private IEnumerator IncreasesTotalTime(Configuration config)
        {
            _totalTime = 0f;

            //Total level timer, stops when all ghosts have already spawned
            while (_totalTime < config.TimeSpawnGhost[^1])
            {
                _totalTime += Time.deltaTime;

                yield return null;
            }

            _increasesTotalTime = null;
        }
    }
}
