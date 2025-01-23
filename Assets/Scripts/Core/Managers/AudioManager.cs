using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
using Moonthsoft.Core.Definitions.Sounds;

namespace Moonthsoft.Core.Managers
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        #region Parameters

        private const int NUM_SOURCES_FX = 10;

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioMixerGroup _mixerMaster;
        [SerializeField] private FxData[] _soundsDataFx;

        private AudioSource[] _sourcesFx;

        [HideInInspector] public float masterVolume;

        public static object Fx { get; set; }

        #endregion


        #region Init

        private void Awake()
        {
            _sourcesFx = InitAudoSources(NUM_SOURCES_FX, false, _mixerMaster, "Fx");
        }

        private AudioSource[] InitAudoSources(int num, bool loop, AudioMixerGroup mixerGroup, string name)
        {
            AudioSource[] sourcesAux = new AudioSource[num];

            var parentAux = new GameObject();
            parentAux.name = "Container_AudioSources_" + name;
            parentAux.transform.parent = transform;
            parentAux.transform.localPosition = Vector3.zero;

            for (int i = 0; i < sourcesAux.Length; ++i)
            {
                var goAux = new GameObject();
                goAux.name = "AudioSource_" + name + "_" + (i + 1).ToString();
                goAux.transform.parent = parentAux.transform;
                goAux.transform.localPosition = Vector3.zero;

                sourcesAux[i] = goAux.AddComponent<AudioSource>();
                sourcesAux[i].outputAudioMixerGroup = mixerGroup;
                sourcesAux[i].loop = loop;
            }

            return sourcesAux;
        }

        #endregion


        #region Play Fx

        public AudioSource PlayFx(Fx sound, bool loop = false)
        {
            SoundData soundAux = Array.Find(_soundsDataFx, s => s.name == sound);

            if (soundAux == null)
            {
                Debug.LogError("Sound not found: " + sound);
                return null;
            }

            var sourceAux = GetSourceFree(_sourcesFx);

            if (sourceAux == null)
            {
                Debug.LogError("There are no AudioSources available.");
                return null;
            }

            PlaySound(sourceAux, soundAux, loop);

            return sourceAux;
        }

        public float GetFxLenght(Fx sound)
        {
            SoundData soundAux = Array.Find(_soundsDataFx, s => s.name == sound);

            if (soundAux == null)
            {
                Debug.LogError("Sound not found: " + sound);
                return 0f;
            }

            return soundAux.clip.length;
        }

        public void StopAllSourcesAndReset()
        {
            StopSources(_sourcesFx);
        }

        private void StopSources(AudioSource[] sources)
        {
            for (int i = 0; i < sources.Length; ++i)
            {
                sources[i].Stop();
            }
        }

        private void PlaySound(AudioSource source, SoundData data, bool loop)
        {
            source.loop = loop;
            source.clip = data.clip;
            source.volume = data.volume;

            source.Play();
        }

        private AudioSource GetSourceFree(AudioSource[] sources)
        {
            for (int i = 0; i < sources.Length; ++i)
            {
                if (!sources[i].isPlaying)
                {
                    return sources[i];
                }
            }

            return null;
        }

        #endregion


        #region Set Volume

        public void SetVolumeMaster(float volume)
        {
            masterVolume = volume;

            _mixer.SetFloat("volumeMaster", GetVolumeDb(volume));
        }

        private float GetVolumeDb(float volume)
        {
            float volumeAux;

            if (volume != 0)
            {
                volumeAux = Mathf.Log10(volume) * 20f;
            }
            else
            {
                volumeAux = -80.0f;
            }

            return volumeAux;
        }

        #endregion


        #region Audio Data Class

        public abstract class SoundData
        {
            public AudioClip clip;

            [Range(0f, 1f)]
            public float volume = 0.5f;
        }


        [System.Serializable]
        public class FxData : SoundData
        {
            public Fx name;
        }

        #endregion
    }
}