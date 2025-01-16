using Moonthsoft.Core.FSM;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    [CreateAssetMenu(menuName = "FSM/InkyChaseState")]
    public class InkyChaseState : GhostBaseState, IState<Ghost>
    {
        protected override List<NodeGraph> GetPath()
        {
            var posPlayer = Player.transform.position;
            var posBlinky = LevelManager.GetGhost(GhostType.Blinky).transform.position;

            var posTarget = (2f * posPlayer) - posBlinky;

            var nodeTarget = Graph.GetNearestNode(posTarget);

            return GetPathToTarget(nodeTarget);
        }
    }
}