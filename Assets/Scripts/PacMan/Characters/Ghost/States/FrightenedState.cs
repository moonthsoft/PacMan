using Moonthsoft.Core.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    [CreateAssetMenu(menuName = "FSM/FrightenedState")]
    public class FrightenedState : GhostBaseState, IState<Ghost>
    {
        public void Enter()
        {
            ghost.TurnAround();
        }

        //public void Exit()
        //{
        //    ghost.TurnAround();
        //}

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