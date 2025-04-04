using System;
using UnityEngine;
using Zenject;
using Moonthsoft.Core.Managers;
using Moonthsoft.PacMan.Config;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Class that handles the level logic, due to the complexity of this, it is divided into subclasses that handle specific logic, 
    /// for example, ScoreLevelManager handles the logic related to the game score.
    /// 
    /// It also has the references to the main elements of the game such as Pac-Man, the ghosts or the graph.Thus, 
    /// these elements can be accessed if you have the reference to the LevelManager.
    /// The level manager can be accessed by injecting its dependency into other classes.
    /// </summary>
    public class LevelManager : MonoInstaller
    {
        private GameLogicLevelManager _logicManager;
        private TimerLevelManager _timerManager;
        private ItemsLevelManager _itemsManager;
        private MusicLevelManager _musicLevelManager;
        private ScoreLevelManager _scoreLevelManager;

        private ILoadSceneManager _loadSceneManager;
        private IAudioManager _audioManager;
        private IDataManager _dataManager;

        [SerializeField] private Configuration _config;

        [SerializeField] private LevelUI _ui;
        [SerializeField] private Graph _graph;
        [SerializeField] private Player _player;

        [SerializeField] private Ghost _blinky;
        [SerializeField] private Ghost _pinky;
        [SerializeField] private Ghost _inky;
        [SerializeField] private Ghost _clyde;

        public event Action ChangeChaseModeEvent
        {
            add { _timerManager.ChangeChaseModeEvent += value; }
            remove { _timerManager.ChangeChaseModeEvent -= value; }
        }

        public event Action ActivePowerUpEvent
        {
            add { _itemsManager.ActivePowerUpEvent += value; }
            remove { _itemsManager.ActivePowerUpEvent -= value; }
        }

        public event Action FinishingPowerUpEvent
        {
            add { _itemsManager.FinishingPowerUpEvent += value; }
            remove { _itemsManager.FinishingPowerUpEvent -= value; }
        }

        public event Action ResetPowerUpEvent
        {
            add { _itemsManager.ResetPowerUpEvent += value; }
            remove { _itemsManager.ResetPowerUpEvent -= value; }
        }

        public event Action DeactivePowerUpEvent
        {
            add { _itemsManager.DeactivePowerUpEvent += value; }
            remove { _itemsManager.DeactivePowerUpEvent -= value; }
        }

        public bool IsChaseMode { get { return _timerManager.IsChaseMode; } }
        public Graph Graph { get { return _graph; } }
        public Player Player { get { return _player; } }


        [Inject] private void InjectLoadSceneManager(ILoadSceneManager loadSceneManager) { _loadSceneManager = loadSceneManager; }
        [Inject] private void InjectAudioManager(IAudioManager audioManager) { _audioManager = audioManager; }
        [Inject] private void InjectDataManager(IDataManager dataManager) { _dataManager = dataManager; }

        public override void InstallBindings()
        {
            Container.Bind<LevelManager>().FromInstance(this).AsSingle();
        }


        private void Awake()
        {
            _logicManager = new GameLogicLevelManager(this, _ui, _config, _loadSceneManager, _audioManager);
            _timerManager = new TimerLevelManager(this);
            _itemsManager = new ItemsLevelManager(this, _ui, _config, _audioManager);
            _musicLevelManager = new MusicLevelManager(_audioManager);
            _scoreLevelManager = new ScoreLevelManager(_ui, _dataManager);

            ResetSateGame(true);

            ResetScore();
        }

        public Ghost GetGhost(GhostType ghostType)
        {
            switch (ghostType)
            {
                case GhostType.Blinky: return _blinky;
                case GhostType.Pinky: return _pinky;
                case GhostType.Inky: return _inky;
                case GhostType.Clyde: return _clyde;
            }

            Debug.LogError("The Ghost Type is not configured.");

            return null;
        }

        public Ghost[] GetGhosts()
        {
            Ghost[] ghosts = new Ghost[4];

            ghosts[0] = _blinky;
            ghosts[1] = _pinky;
            ghosts[2] = _inky;
            ghosts[3] = _clyde;

            return ghosts;
        }

        public float GetSpeed(TypeSpeedPercentage type)
        {
            int indx = GetIndex(_config.SpeedPercentage.Matrix.Length);

            float[] speedAux = _config.SpeedPercentage.Matrix[indx].array;

            return speedAux[(int)type];
        }

        public int GetIndex(int length)
        {
            int indx = _logicManager.CurrentLevel;

            if (indx >= length)
            {
                indx = length - 1;
            }

            return indx;
        }


        //GameLogicLevelManager Methods
        public void PlayerDie() { _logicManager.PlayerDie(); }
        private void CompleteLevel() { _logicManager.CompleteLevel(); }
        private void ResetSateGame(bool playMusic) { _logicManager.ResetSateGame(playMusic); }


        //TimerLevelManager Methods
        public void ActiveTimer() { _timerManager.ActiveTimer(_config); }
        public bool CanGhostExitHome(GhostType type) { return _timerManager.CanGhostExitHome(_config, type); }
        public void FreezeGame(float duration, Action action = null) { _timerManager.FreezeGame(duration, action); }


        //ItemsLevelManager Methods
        public void AddDot(Dot dot) { _itemsManager.AddDot(dot); }
        public void RemoveDot() { _itemsManager.RemoveDot(CompleteLevel); }
        public void AddPowerUp(PowerUp powerUp) { _itemsManager.AddPowerUp(powerUp); }
        public void ActivePowerUp() { _itemsManager.ActivePowerUp(_config, _logicManager.CurrentLevel); }
        public void ResetItems() { _itemsManager.ResetItems(); }
        public void EatGhost(Vector3 pos) { _itemsManager.EatGhost(pos); }


        //MusicLevelManager Methods
        public void ActiveMusic(bool ghost = false) { _musicLevelManager.ActiveMusic(ghost); }
        public void DeactiveAllMusic() { _musicLevelManager.DeactiveAllMusic(); }
        public void AddGhostEated() { _musicLevelManager.AddGhostEated(); }
        public void RemoveGhostEated() { _musicLevelManager.RemoveGhostEated(); }


        //ScoreLevelManager Methods
        public void AddScore(int score) { _scoreLevelManager.AddScore(score); }
        public void ResetScore() { _scoreLevelManager.ResetScore(); }
    }
}