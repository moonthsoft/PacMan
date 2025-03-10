using System;
using UnityEngine;

namespace Moonthsoft.Core.FSM
{
    /// <summary>
    /// Base class for the StateController. The StateController will be the class in charge of managing the FSM state changes based on the conditions of each entity.
    /// </summary>
    /// <typeparam name="T">Class of the entity that the AI ​​will have, for example, the enemy of the game.</typeparam>
    /// <typeparam name="S">Enum containing all the states of that FSM.</typeparam>
    public abstract class BaseStateController<T, S> 
        where T : class 
        where S : Enum
    {
        private readonly T _entity;

        protected FiniteStateMachine<T> stateMachine = null;
        protected S currentState;

        public S CurrentState { get { return currentState; } }


        public abstract void SetState(S state);


        public BaseStateController(T entity)
        {
            _entity = entity;
        }

        public T GetEntity()
        {
            return _entity;
        }

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