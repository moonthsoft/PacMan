using Moonthsoft.Core.Definitions.Direction;
using Moonthsoft.Core.Utils.Direction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    public class Pathfinding
    {
        public static List<NodeGraph> FindPath(NodeGraph start, NodeGraph target, Ghost ghost)
        {
            //List of nodes to explore
            var nodesToExplore = new List<NodeGraph> { start };

            //Set of already explored nodes
            var exploredNodes = new HashSet<NodeGraph>();

            //The direction from which the node was reached
            var fromDirection = new Dictionary<NodeGraph, Direction> { [start] = ghost.CurrentDir };

            //Dictionary to reconstruct the path
            var cameFrom = new Dictionary<NodeGraph, NodeGraph>();

            //Accumulated costs to reach each node
            var sizePath = new Dictionary<NodeGraph, float> { [start] = 0 };

            //Estimated total costs for each node
            var estimatedSizePath = new Dictionary<NodeGraph, float> { [start] = start.GetSizeNode(target) };


            while (nodesToExplore.Count > 0)
            {
                //Find the node with the lowest estimated size in the open set
                NodeGraph current = nodesToExplore.OrderBy(node => estimatedSizePath.ContainsKey(node) ? estimatedSizePath[node] : float.MaxValue).First();

                //If we reach the target node, reconstruct the path
                if (current == target)
                {
                    var path = new List<NodeGraph> { current };

                    while (cameFrom.ContainsKey(current))
                    {
                        current = cameFrom[current];
                        path.Add(current);
                    }

                    path.Reverse();

                    return path;
                }

                nodesToExplore.Remove(current);
                exploredNodes.Add(current);
                var currentDir = fromDirection[current];

                for (int i = 0; i < NodeGraph.NUM_NODES; ++i)
                {
                    //We can't turn 180º
                    if ((Direction)i == DirectionUtility.ReverseDirection(currentDir))
                    {
                        continue;
                    }

                    var nodeAux = current.GetNode(i);

                    if (nodeAux == null || exploredNodes.Contains(nodeAux))
                    {
                        continue;
                    }

                    float tentativeGScore = sizePath[current] + current.GetSize(i);

                    if (!nodesToExplore.Contains(nodeAux))
                    {
                        nodesToExplore.Add(nodeAux);
                    }
                    else if (tentativeGScore >= sizePath.GetValueOrDefault(nodeAux, float.MaxValue))
                    {
                        continue;
                    }

                    //This path is the best so far, update it
                    cameFrom[nodeAux] = current;
                    fromDirection[nodeAux] = (Direction)i;
                    sizePath[nodeAux] = tentativeGScore;
                    estimatedSizePath[nodeAux] = sizePath[nodeAux] + nodeAux.GetSizeNode(target);
                }
            }

            Debug.LogError("Can't make a Pathfinding from node " + start.name + " to node " + target.name + " with " + ghost.Type.ToString() 
                + " in state " + ghost.StateController.CurrentState.ToString() + " and direction " + ghost.CurrentDir.ToString());

            //If no path is found, return null
            return null;
        }
    }
}