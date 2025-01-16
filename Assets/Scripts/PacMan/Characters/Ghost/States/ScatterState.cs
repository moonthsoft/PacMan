using Moonthsoft.Core.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    [CreateAssetMenu(menuName = "FSM/ScatterState")]
    public class ScatterState : GhostBaseState, IState<Ghost>
    {
        protected override List<NodeGraph> GetPath()
        {
            var nodeTarget = Graph.GetGhostScatterNode(ghost.Type);

            return GetPathToTarget(nodeTarget);
        }
    }
}