using Moonthsoft.Core.Definitions.Direction;
using Moonthsoft.Core.Managers;
using Moonthsoft.Core.Utils.Direction;
using UnityEngine;
using Zenject;

namespace Moonthsoft.PacMan
{
    public class Player : Character
    {
        private Direction _NextDir;

        private IInputManager _inputManager;
        [Inject] private void InjectInputManager(IInputManager inputManager) { _inputManager = inputManager; }


        protected override NodeGraph GetInitialNode()
        {
            return LevelManager.Graph.PlayerInitialNode;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Time.timeScale > 0f)
            {
                CheckInput();
            }
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
    }
}