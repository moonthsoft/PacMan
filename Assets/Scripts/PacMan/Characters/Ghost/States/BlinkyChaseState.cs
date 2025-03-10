using System.Collections.Generic;
using UnityEngine;
using Moonthsoft.Core.FSM;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Chase state specific to Blinky (the red ghost), each ghost has its own Chase status to reflect a unique personality.
    /// In this state, Blinky heads to the last node Pac-Man was on, to chase him.
    /// </summary>
    [CreateAssetMenu(menuName = "FSM/BlinkyChaseState")]
    public class BlinkyChaseState : GhostBaseState, IState<Ghost>
    {
        protected override List<NodeGraph> GetPath()
        {
            var nodeTarget = Player.CurrentNode;

            return GetPathToTarget(nodeTarget);
        }
    }
}