namespace Moonthsoft.Core.FSM
{
    /// <summary>
    /// Class in charge of managing the states of the FSM.
    /// </summary>
    /// <typeparam name="T">Class of the entity that the AI ​​will have, for example, the enemy of the game.</typeparam>
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