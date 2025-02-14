using Moonthsoft.Core.Definitions.Sounds;
using UnityEngine;

namespace Moonthsoft.Core.Managers
{
    public interface IAudioManager
    {
        public AudioSource PlayFx(Fx sound, bool loop = false);

        public float GetFxLenght(Fx sound);

        public void StopAllSourcesAndReset();

        public void SetVolumeMaster(float volume);
    }
}
