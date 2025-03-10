using UnityEngine;

namespace Moonthsoft.Core.Managers
{
    /// <summary>
    /// Interface for the InputManager.
    /// InputManager is responsible for managing the player's input.
    /// </summary>
    public interface IInputManager
    {
        /// <summary>
        /// Gets the direction of the player's input.
        /// </summary>
        /// <returns>The direction in values ​​from -1 to 1, X is horizontal, Y is vertical.</returns>
        public Vector2 GetDirection();
    }
}