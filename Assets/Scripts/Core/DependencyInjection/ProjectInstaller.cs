using UnityEngine;
using Zenject;
using Moonthsoft.Core.Managers;

namespace Moonthsoft.Core
{
    /// <summary>
    /// Class responsible for instantiating and injecting the dependencies of the game's general managers.
    /// </summary>
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private LoadSceneManager _loadSceneManager;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private DataManager _dataManager;

        public override void InstallBindings()
        {
            var loadSceneManagerInstance = Instantiate(_loadSceneManager);
            var inputManager = Instantiate(_inputManager);
            var audioManager = Instantiate(_audioManager);
            var dataManager = Instantiate(_dataManager);

            Container.Bind<ILoadSceneManager>().FromInstance(loadSceneManagerInstance).AsSingle().NonLazy();
            Container.Bind<IInputManager>().FromInstance(inputManager).AsSingle().NonLazy();
            Container.Bind<IAudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
            Container.Bind<IDataManager>().FromInstance(dataManager).AsSingle().NonLazy();
        }
    }
}