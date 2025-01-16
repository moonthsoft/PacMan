using Moonthsoft.Core.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.PacMan
{
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