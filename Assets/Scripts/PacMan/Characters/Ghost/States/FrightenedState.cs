using System.Collections.Generic;
using UnityEngine;
using Moonthsoft.Core.FSM;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// State that the ghost enters when Pac-Mac eats a power up, upon entering this state he will turn 180º, 
    /// and then choose the direction randomly.
    /// </summary>
    [CreateAssetMenu(menuName = "FSM/FrightenedState")]
    public class FrightenedState : GhostBaseState, IState<Ghost>
    {
        public void Enter()
        {
            ghost.TurnAround();
        }

        protected override List<NodeGraph> GetPath()
        {
            var neighborNodes = ghost.CurrentNode.GetNeighborNodes(ghost.CurrentDir);

            int rand = Random.Range(0, neighborNodes.Count);

            var path = new List<NodeGraph>
            {
                neighborNodes[rand]
            };

            return path;
        }
    }
}