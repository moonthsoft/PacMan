using System.Collections.Generic;
using UnityEngine;
using Moonthsoft.Core.FSM;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Chase status specific to Inky (the blue ghost), each ghost has its own Chase status to reflect a unique personality.
    /// In this state, Inky will head to Blinky's mirror position relative to Pac-Man, moving closer to Pac-Man as Blinky hits him, 
    /// and may even flank Pac-Man.
    /// </summary>
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