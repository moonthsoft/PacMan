using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Moonthsoft.Core.Definitions.Direction;
using Moonthsoft.Core.Utils.Direction;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Class in charge of pathfinding ghosts, FindPath, its only method, traverses  the graph to find the fastest route to the destination node.
    /// </summary>
    public class Pathfinding
    {
        public static List<NodeGraph> FindPath(NodeGraph start, NodeGraph target, Ghost ghost)
        {
            var nodesToExplore = new List<NodeGraph> { start };
            var exploredNodes = new HashSet<NodeGraph>();
            var nodeReachedFromDirection = new Dictionary<NodeGraph, Direction> { [start] = ghost.CurrentDir };
            var nodeCameFromNode = new Dictionary<NodeGraph, NodeGraph>();
            var sizePathAccumulated = new Dictionary<NodeGraph, float> { [start] = 0 };
            var estimatedSizePath = new Dictionary<NodeGraph, float> { [start] = start.GetSizeNode(target) };


            while (nodesToExplore.Count > 0)
            {
                //Find the node with the lowest estimated size in the open set
                NodeGraph current = nodesToExplore.OrderBy(node => estimatedSizePath.ContainsKey(node) ? estimatedSizePath[node] : float.MaxValue).First();

                //If we reach the destination node, we reconstruct and retourn the the path
                if (current == target)
                {
                    var path = new List<NodeGraph> { current };

                    while (nodeCameFromNode.ContainsKey(current))
                    {
                        current = nodeCameFromNode[current];
                        path.Add(current);
                    }

                    path.Reverse();

                    return path;
                }

                nodesToExplore.Remove(current);
                exploredNodes.Add(current);
                var currentDir = nodeReachedFromDirection[current];

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

                    float tentativeGScore = sizePathAccumulated[current] + current.GetSize(i);

                    if (!nodesToExplore.Contains(nodeAux))
                    {
                        nodesToExplore.Add(nodeAux);
                    }
                    else if (tentativeGScore >= sizePathAccumulated.GetValueOrDefault(nodeAux, float.MaxValue))
                    {
                        continue;
                    }

                    //We updated the route as it is the best
                    nodeCameFromNode[nodeAux] = current;
                    nodeReachedFromDirection[nodeAux] = (Direction)i;
                    sizePathAccumulated[nodeAux] = tentativeGScore;
                    estimatedSizePath[nodeAux] = sizePathAccumulated[nodeAux] + nodeAux.GetSizeNode(target);
                }
            }

            Debug.LogError("Can't make a Pathfinding from node " + start.name + " to node " + target.name + " with " + ghost.Type.ToString() 
                + " in state " + ghost.StateController.CurrentState.ToString() + " and direction " + ghost.CurrentDir.ToString());

            //If no path is found, return null
            return null;
        }
    }
}