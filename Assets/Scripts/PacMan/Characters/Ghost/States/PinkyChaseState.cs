using System.Collections.Generic;
using UnityEngine;
using Moonthsoft.Core.Definitions.Direction;
using Moonthsoft.Core.FSM;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Chase status specific to Pinky (the pink ghost), each ghost has its own Chase status to reflect a unique personality.
    /// In this state, Pinky will head to a node that is in front of the direction Pac-Man is facing, in order to flank him.
    /// </summary>
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