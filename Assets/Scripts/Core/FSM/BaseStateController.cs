using UnityEngine;
using System;

namespace Moonthsoft.Core.FSM
{
    public abstract class BaseStateController<T, S> 
        where T : class 
        where S : Enum
    {
        protected FiniteStateMachine<T> stateMachine = null;

        private readonly T _entity;
        protected S currentState;

        public S CurrentState { get { return currentState; } }

        public BaseStateController(T entity)
        {
            _entity = entity;
        }

        public T GetEntity()
        {
            return _entity;
        }

        public abstract void SetState(S state);

        public void UpdateState()
        {
            if (stateMachine == null)
            {
                Debug.LogError("The finite state machine is not initialized.");

                return;
            }

            stateMachine.UpdateState();
        }

        protected void InitFSM(IState<T> initialState) 
        {
            if (stateMachine != null)
            {
                Debug.LogError("The finite state machine is already initialized.");

                return;
            }

            stateMachine = new FiniteStateMachine<T>(initialState);
        }
    }
}