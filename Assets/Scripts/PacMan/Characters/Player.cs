using UnityEngine;
using Zenject;
using Moonthsoft.Core.Definitions.Direction;
using Moonthsoft.Core.Managers;
using Moonthsoft.Core.Utils.Direction;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// It controls Pac-Man, mainly his movement in the graph based on the player's input. 
    /// And also his animator. 
    /// Collisions are managed from the Items and the GhostCollider.
    /// </summary>
    public class Player : Character
    {
        private Direction _NextDir;
        private IInputManager _inputManager;


        [Inject] private void InjectInputManager(IInputManager inputManager) { _inputManager = inputManager; }

        private void Update()
        {
            if (Time.timeScale > 0f)
            {
                CheckInput();
            }
        }

        public void ActiveAnimationDie()
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;

            animator.SetTrigger("die");
        }

        protected override NodeGraph GetInitialNode()
        {
            return LevelManager.Graph.PlayerInitialNode;
        }

        protected override void GetNextNode()
        {
            var nodeAux = CurrentNode.GetNode(_NextDir);
            var nextDirAux = _NextDir;

            if (nodeAux == null)
            {
                nodeAux = CurrentNode.GetNode(CurrentDir);
                nextDirAux = CurrentDir;
            }

            if (nodeAux != null)
            {
                CurrentNode = nodeAux;

                SetDirection(nextDirAux);

                SetMove(true);
            }
        }

        protected override float GetSpeedPercentage()
        {
            return LevelManager.GetSpeed(TypeSpeedPercentage.Player);
        }

        private void CheckInput()
        {
            var movement = _inputManager.GetDirection();

            var dirAux = _NextDir;

            if (movement.y == 1f)
            {
                _NextDir = Direction.Up;
            }
            else if (movement.y == -1f)
            {
                _NextDir = Direction.Down;
            }
            else if (movement.x == 1f)
            {
                _NextDir = Direction.Right;
            }
            else if (movement.x == -1f)
            {
                _NextDir = Direction.Left;
            }

            if (dirAux != _NextDir)
            {
                CheckTurnBack();
            }
        }

        private void CheckTurnBack()
        {
            if (isMoving && _NextDir == DirectionUtility.ReverseDirection(CurrentDir))
            {
                SetDirection(_NextDir);
                
                CurrentNode = CurrentNode.GetNode(_NextDir);
            }
        }
    }
}