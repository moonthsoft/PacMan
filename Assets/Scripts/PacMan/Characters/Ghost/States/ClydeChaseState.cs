using Moonthsoft.Core.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    [CreateAssetMenu(menuName = "FSM/ClydeChaseState")]
    public class ClydeChaseState : GhostBaseState, IState<Ghost>
    {
        public const float DIST_SCAPE = 4f;

        protected override List<NodeGraph> GetPath()
        {
            NodeGraph nodeTarget;

            float dist = Vector2.Distance(ghost.transform.position, Player.transform.position);

            if (dist > DIST_SCAPE)
            {
                nodeTarget = Player.CurrentNode;
            }
            else
            {
                nodeTarget = Graph.GetGhostScatterNode(ghost.Type);
            }

            return GetPathToTarget(nodeTarget);
        }
    }
}