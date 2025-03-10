using UnityEngine;
using Moonthsoft.Core.Definitions.Sounds;

namespace Moonthsoft.Core.Managers
{
    /// <summary>
    /// Interface for the AudioManager.
    /// AudioManager is responsible for managing the playback and volume of the game's sounds and music.
    /// </summary>
    public interface IAudioManager
    {
        /// <summary>
        /// Activate an Fx sound.
        /// </summary>
        /// <param name="sound">The sound to be activated.</param>
        /// <param name="loop">If it's only going to play once or is it a loop.</param>
        /// <returns>The AudioSource that will play the sound.</returns>
        public AudioSource PlayFx(Fx sound, bool loop = false);

        /// <summary>
        /// Returns the clip duration of a Fx sound.
        /// </summary>
        /// <param name="sound">Sound whose duration you want to obtain.</param>
        /// <returns>The duration of the sound clip.</returns>
        public float GetFxLenght(Fx sound);

        /// <summary>
        /// Stops all playing sounds and resets their AudioSources.
        /// </summary>
        public void StopAllSourcesAndReset();

        /// <summary>
        /// Changes the master volume (affects all sound types).
        /// </summary>
        /// <param name="volume">New volume that the sound should have (between 0 and 1).</param>
        public void SetVolumeMaster(float volume);
    }
}
