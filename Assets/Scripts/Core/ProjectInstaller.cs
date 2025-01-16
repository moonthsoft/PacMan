using UnityEngine;
using Zenject;
using Moonthsoft.Core.Managers;

namespace Moonthsoft.Core
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private LoadSceneManager _loadSceneManager;
        [SerializeField] private InputManager _inputManager;

        public override void InstallBindings()
        {
            var loadSceneManagerInstance = Instantiate(_loadSceneManager);
            var inputManager = Instantiate(_inputManager);

            Container.Bind<ILoadSceneManager>().FromInstance(loadSceneManagerInstance).AsSingle().NonLazy();
            Container.Bind<IInputManager>().FromInstance(inputManager).AsSingle().NonLazy();
        }
    }
}