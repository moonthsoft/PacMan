using Moonthsoft.Core.Definitions.Direction;
using Moonthsoft.Core.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    [CreateAssetMenu(menuName = "FSM/PinkyChaseState")]
    public class PinkyChaseState : GhostBaseState, IState<Ghost>
    {
        private const float OFFSET = 2f;

        protected override List<NodeGraph> GetPath()
        {
            var posTarget = Player.transform.position;

            switch (Player.CurrentDir)
            {
                case Direction.Up: posTarget.y += OFFSET; break;
                case Direction.Down: posTarget.y += -OFFSET; break;
                case Direction.Left: posTarget.x += -OFFSET; break;
                case Direction.Right: posTarget.x += OFFSET; break;
            }

            var nodeTarget = Graph.GetNearestNode(posTarget);

            return GetPathToTarget(nodeTarget);
        }
    }
}