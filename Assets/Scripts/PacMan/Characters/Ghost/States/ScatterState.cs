using System.Collections.Generic;
using UnityEngine;
using Moonthsoft.Core.FSM;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// In this state the ghost goes to the associated corner, and once there it will spin around the corner. 
    /// The ghost switches between the Chase and Scatter state every so often depending on the current level.
    /// </summary>
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