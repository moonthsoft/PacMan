using Moonthsoft.Core.Definitions.Sounds;
using Moonthsoft.Core.Managers;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    public class MusicLevelManager
    {
        private readonly LevelManager _levelmanager;

        private AudioSource _ghostEatedAudioSource = null;
        private AudioSource _musicAudioSource = null;
        private int _numGhostEated = 0;

        private readonly IAudioManager _audioManager;

        public MusicLevelManager(LevelManager levelmanager, IAudioManager audioManager)
        {
            _levelmanager = levelmanager;

            _audioManager = audioManager;
        }

        public void ActiveMusic(bool ghost = false)
        {
            DeactiveMusic();

            var music = Fx.MusicGhost;

            if (!ghost)
            {
                music = Fx.MusicNormal;
            }

            _musicAudioSource = _audioManager.PlayFx(music, true);
        }

        public void DeactiveMusic()
        {
            if (_musicAudioSource != null)
            {
                _musicAudioSource.Stop();
            }
        }

        public void AddGhostEated()
        {
            _numGhostEated++;

            if (_ghostEatedAudioSource == null && _numGhostEated >= 0)
            {
                _ghostEatedAudioSource = _audioManager.PlayFx(Fx.GhostEatedToHome, true);
            }
        }

        public void RemoveGhostEated()
        {
            _numGhostEated--;

            if (_ghostEatedAudioSource != null && _numGhostEated <= 0)
            {
                _ghostEatedAudioSource.loop = false;
                _ghostEatedAudioSource = null;
            }
        }
    }
}