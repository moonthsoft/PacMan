using System.Collections;
using UnityEngine;
using Zenject;
using Moonthsoft.Core.Managers;
using Moonthsoft.Core.Definitions.Scenes;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Init scene logic. Just change the scene to Game, where the game is.
    /// </summary>
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
