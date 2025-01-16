using Moonthsoft.Core.Definitions.Direction;
using Moonthsoft.Core.FSM;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.XR;

namespace Moonthsoft.PacMan
{
    [CreateAssetMenu(menuName = "FSM/HomeState")]
    public class HomeState : GhostBaseState, IState<Ghost>
    {
        private bool _toUp = true;

        public override void Init(Ghost entity)
        {
            base.Init(entity);

            if (entity.Type == GhostType.Pinky)
            {
                _toUp = false;
            }
        }

        protected override List<NodeGraph> GetPath()
        {
            NodeGraph nodeTarget = null;

            //We have reached the spawn node
            if (ghost.CurrentNode == Graph.GhostSpawnNode)
            {
                ghost.StateController.SetState(GhostState.Scatter);

                nodeTarget = Graph.GhostSpawnNode;
            }
            else
            {
                if (IsCenterNode(ghost.CurrentNode) && ghost.LevelManager.CanGhostExitHome(ghost.Type))
                {
                    //Leaving home
                    if (ghost.CurrentNode == Graph.GhostHomeNode)
                    {
                        nodeTarget = Graph.GhostSpawnNode;
                    }
                    else if (ghost.CurrentNode.GetNode(Direction.Left) != null)
                    {
                        nodeTarget = ghost.CurrentNode.GetNode(Direction.Left);
                    }
                    else if (ghost.CurrentNode.GetNode(Direction.Right) != null)
                    {
                        nodeTarget = ghost.CurrentNode.GetNode(Direction.Right);
                    }
                }
                else
                {
                    //Moving up and down inside home
                    nodeTarget = GetNextNode();

                    if (nodeTarget == null)
                    {
                        _toUp = !_toUp;
                        nodeTarget = GetNextNode();
                    }
                }
            }

            var path = new List<NodeGraph>
            {
                nodeTarget
            };

            return path;
        }

        private NodeGraph GetNextNode()
        {
            var dirAux = _toUp ? Direction.Up : Direction.Down;

            return ghost.CurrentNode.GetNode(dirAux);
        }

        private bool IsCenterNode(NodeGraph node)
        {
            if (node.GetNode(Direction.Up) != null && node.GetNode(Direction.Down) != null)
            {
                return true;
            }

            return false;
        }
    }
}