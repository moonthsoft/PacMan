using UnityEngine;
using Moonthsoft.Core.FSM;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Ghost state manager, the SetState method changes the state, if the conditions necessary for the change to occur are valid.
    /// </summary>
    public class GhostStateController : BaseStateController<Ghost, GhostState>
    {
        private readonly GhostBaseState _chaseState;
        private readonly GhostBaseState _scatterState = ScriptableObject.CreateInstance<ScatterState>();
        private readonly GhostBaseState _frightenedState = ScriptableObject.CreateInstance<FrightenedState>();
        private readonly GhostBaseState _eatedState = ScriptableObject.CreateInstance<EatedState>();
        private readonly GhostBaseState _homeState = ScriptableObject.CreateInstance<HomeState>();


        public GhostStateController(Ghost ghost) : base(ghost) 
        {
            switch (ghost.Type)
            {
                case GhostType.Blinky: _chaseState = ScriptableObject.CreateInstance<BlinkyChaseState>(); break;
                case GhostType.Pinky: _chaseState = ScriptableObject.CreateInstance<PinkyChaseState>(); break;
                case GhostType.Inky: _chaseState = ScriptableObject.CreateInstance<InkyChaseState>(); break;
                case GhostType.Clyde: _chaseState = ScriptableObject.CreateInstance<ClydeChaseState>(); break;
            }

            _chaseState.Init(ghost);
            _scatterState.Init(ghost);
            _frightenedState.Init(ghost);
            _eatedState.Init(ghost);
            _homeState.Init(ghost);

            currentState = ghost.InitState;
            InitFSM(GetState(ghost.InitState));
        }

        public override void SetState(GhostState state)
        {
            if (state == GhostState.Chase || state == GhostState.Scatter)
            {
                if (GetEntity().IsFrightened)
                {
                    state = GhostState.Frightened;
                }
                else if (GetEntity().LevelManager.IsChaseMode)
                {
                    state = GhostState.Chase;
                }
                else
                {
                    state = GhostState.Scatter;
                }
            }

            if (state != currentState)
            {
                currentState = state;

                stateMachine.ChangeState(GetState(state));
            }
        }

        private GhostBaseState GetState(GhostState state)
        {
            switch (state)
            {
                case GhostState.Chase: return _chaseState;
                case GhostState.Scatter: return _scatterState;
                case GhostState.Frightened: return _frightenedState;
                case GhostState.Eated: return _eatedState;
                case GhostState.Home: return _homeState;
            }

            Debug.LogError("The state " + state.ToString() + " is not implemented.");

            return null;
        }
    }
}