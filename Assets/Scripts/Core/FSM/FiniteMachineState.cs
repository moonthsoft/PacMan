using UnityEngine;

namespace Moonthsoft.Core.FSM
{
    public class FiniteStateMachine<T> where T : class
    {
        public IState<T> CurrentState { get; set; }

        public FiniteStateMachine(IState<T> initialState)
        {
            CurrentState = initialState;

            CurrentState.Enter();
        }

        public void UpdateState()
        {
            CurrentState.Update();
        }

        public void ChangeState(IState<T> newState)
        {
            CurrentState?.Exit();

            CurrentState = newState;

            CurrentState.Enter();
        }
    }
}