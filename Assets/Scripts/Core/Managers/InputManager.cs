using UnityEngine;

namespace Moonthsoft.Core.Managers
{
    /// <summary>
    /// InputManager is responsible for managing the player's input.
    /// It is also responsible for managing the current input device.
    /// 
    /// Since this is a small and simple project that serves as a portfolio sample, 
    /// this is an extremely simple InputManager that only checks the WASD and arrow keys for Pac-Man movement. 
    /// More complex projects should have a more elaborate InputManager.
    /// </summary>
    public class InputManager : MonoBehaviour, IInputManager
    {
        private Vector2 _movement = Vector2.zero;


        private void Update()
        {
            CheckDirection();
        }

        public Vector2 GetDirection()
        {
            return _movement;
        }

        private void CheckDirection()
        {
            _movement = Vector2.zero;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                _movement.y = 1f;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                _movement.y = -1f;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _movement.x = 1f;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _movement.x = -1f;
            }
        }
    }
}