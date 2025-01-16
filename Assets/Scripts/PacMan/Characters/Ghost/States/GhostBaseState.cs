using Moonthsoft.Core.FSM;
using Moonthsoft.Core.Utils.Direction;
using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    public abstract class GhostBaseState : ScriptableObject, IState<Ghost>
    {
        protected Ghost ghost;

        protected LevelManager LevelManager { get { return ghost.LevelManager; } }
        protected Player Player { get { return LevelManager.Player; } }
        protected Graph Graph { get { return LevelManager.Graph; } }


        public virtual void Init(Ghost entity)
        {
            ghost = entity;
        }

        public void Update()
        {
            ghost.Path = GetPath();

            ghost.CurrentNode = ghost.Path[0];

            if (ghost.CurrentNode == null)
            {
                Debug.LogError("Node is null.");
            }
        }

        protected abstract List<NodeGraph> GetPath();

        protected List<NodeGraph> GetPathToTarget(NodeGraph nodeTarget)
        {
            if (ghost.CurrentNode == nodeTarget)
            {
                var nextNode = ghost.CurrentNode.GetNeighborNodes(ghost.CurrentDir)[0];

                var pathfinding = Pathfinding.FindPath(nextNode, nodeTarget, ghost);

                return pathfinding;
            }
            else
            {
                var pathfinding = Pathfinding.FindPath(ghost.CurrentNode, nodeTarget, ghost);

                if (pathfinding == null || pathfinding.Count == 0)
                {
                    Debug.LogError("Pathfinding is void.");
                    return null;
                }
                else if (pathfinding.Count < 2)
                {
                    Debug.LogError("Pathfinding is not long enough.");
                    return null;
                }

                pathfinding.RemoveAt(0);

                return pathfinding;
            }
        }
    }
}