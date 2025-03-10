using System.Collections.Generic;
using UnityEngine;
using Moonthsoft.Core.FSM;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Chase status specific to Clyde (the orange ghost), each ghost has its own Chase status to reflect a unique personality.
    /// In this state, Clyde will behave like Blinky, heading to the last node Pac-Man was on to chase him, but when he is close enough, 
    /// he will behave like the Scatter state, heading to the bottom left corner, thus keeping his distance from Pac-Man.
    /// </summary>
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