using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Moonthsoft.Core.Definitions.Scenes;
using Moonthsoft.Core.UI;

namespace Moonthsoft.Core.Managers
{
    /// <summary>
    /// LoadSceneManager handles switching between scenes.
    /// </summary>
    public class LoadSceneManager : MonoBehaviour, ILoadSceneManager
    {
        private const float TIME_IN_LOADING_SCENE = 0.2f;

        private WaitForSecondsRealtime _waitInLoadingScene;
        private IEnumerator _loadCoroutine = null;
        private Scenes _currentScene;

        [SerializeField] private BlackFade _blackFade;

        public bool IsLoading { get { return _loadCoroutine != null; } }
        public Scenes GetCurrentScene { get { return _currentScene; } }


        private void Awake()
        {
            _waitInLoadingScene = new WaitForSecondsRealtime(TIME_IN_LOADING_SCENE);

            _currentScene = (Scenes)SceneManager.GetActiveScene().buildIndex;
        }

        public void LoadScene(Scenes scene)
        {
            if (_loadCoroutine != null)
            {
                Debug.LogError("You're already trying to change the scene.");
                return;
            }

            _currentScene = scene;

            StartCoroutine(_loadCoroutine = LoadLevelCoroutine(scene));
        }

        private IEnumerator LoadLevelCoroutine(Scenes _scene)
        {
            yield return _blackFade.Active(true);

            //This is not necessary because Unity already resets the TimeScale to 1f when change the scene,
            //but I add it just in case they change it in the future (it wouldn't be the first time Unity does something like this)
            Time.timeScale = 1f;

            yield return LoadSceneAsync(Scenes.Loading);

            yield return _waitInLoadingScene;

            yield return LoadSceneAsync(_scene);

            yield return _blackFade.Active(false);

            _loadCoroutine = null;
        }

        private IEnumerator LoadSceneAsync(Scenes _scene)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync((int)_scene);

            yield return async;
        }
    }
}