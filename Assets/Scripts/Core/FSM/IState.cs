namespace Moonthsoft.Core.FSM
{
    /// <summary>
    /// Interface for AI FSM states.
    /// </summary>
    /// <typeparam name="T">Class of the entity that the AI ​​will have, for example, the enemy of the game.</typeparam>
    public interface IState<T> where T : class
    {
        /// <summary>
        /// Not to be confused with the monobehaviour Update, this Update has to be called manually.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// It is called when entering the state.
        /// </summary>
        public virtual void Enter() { }

        /// <summary>
        /// It is called when exit the state.
        /// </summary>
        public virtual void Exit() { }

        /// <summary>
        /// It is called only once when instantiating the entity, and is used to create the references. 
        /// It is not called again when entering the state.
        /// </summary>
        /// <param name="entity">Entity that possesses the AI. For example, an enemy in the game.</param>
        public abstract void Init(T entity);
    }
}