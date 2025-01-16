using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Moonthsoft.Core.Managers
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        //[SerializeField] private EventSystem eventSystem = default;
        
        private Vector2 _movement = Vector2.zero;

        public Vector2 GetDirection()
        {
            return _movement;
        }

        private void Update()
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