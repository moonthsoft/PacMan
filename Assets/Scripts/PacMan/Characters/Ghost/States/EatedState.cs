using System.Collections.Generic;
using UnityEngine;
using Moonthsoft.Core.FSM;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// State of the ghost when Pac-Man eats it, it will go for the fastest route to the spawn area.
    /// </summary>
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

            ghost.LevelManager.AddGhostEated();
        }

        public void Exit()
        {
            ghost.Animator.SetBool("eated", false);

            ghost.LevelManager.RemoveGhostEated();
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