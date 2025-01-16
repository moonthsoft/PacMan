using System.Collections;
using UnityEngine;
using Moonthsoft.Core.Managers;
using Moonthsoft.Core.Definitions.Scenes;
using Zenject;

namespace Moonthsoft.PacMan
{
    public class InitScene : MonoBehaviour
    {
        private ILoadSceneManager _loadSceneManager;
        
        [Inject] private void InjectLoadSceneManager(ILoadSceneManager loadSceneManager) { _loadSceneManager = loadSceneManager; }


        private void Start()
        {
            StartCoroutine(StartCoroutine());
        }

        private IEnumerator StartCoroutine()
        {
            yield return new WaitForSeconds(0.5f);

            _loadSceneManager.LoadScene(Scenes.Game);
        }
    }
}
