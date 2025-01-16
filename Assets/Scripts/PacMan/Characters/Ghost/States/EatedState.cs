using Moonthsoft.Core.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    [CreateAssetMenu(menuName = "FSM/EatedState")]
    public class EatedState : GhostBaseState, IState<Ghost>
    {
        private bool _isInHome = false;

        public void Enter()
        {
            _isInHome = false;

            ghost.Animator.SetBool("frightened", false);
            ghost.Animator.SetBool("frightenedFinishing", false);
            ghost.Animator.SetBool("eated", true);
        }

        public void Exit()
        {
            ghost.Animator.SetBool("eated", false);
        }

        protected override List<NodeGraph> GetPath()
        {
            var spawnNode = Graph.GhostSpawnNode;

            if (_isInHome)
            {
                ghost.StateController.SetState(GhostState.Home);

                return new List<NodeGraph> { spawnNode };
            }
            else
            {
                if (spawnNode == ghost.CurrentNode)
                {
                    _isInHome = true;

                    return new List<NodeGraph> { Graph.GhostHomeNode };
                }
                else
                {
                    return GetPathToTarget(spawnNode);
                }
            }
        }
    }
}