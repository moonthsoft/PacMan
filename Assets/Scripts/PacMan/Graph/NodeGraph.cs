using Moonthsoft.Core.Definitions.Direction;
using Moonthsoft.Core.Utils.Direction;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    public class NodeGraph : MonoBehaviour
    {
        public const int NUM_NODES = 4;

        [SerializeField] private bool _inTunnel = false;
        [SerializeField] private bool _toGraph = true;

        [SerializeField] private NodeGraph _upNode;
        [SerializeField] private NodeGraph _downNode;
        [SerializeField] private NodeGraph _leftNode;
        [SerializeField] private NodeGraph _rightNode;

        private readonly float[] _sizeNodes = new float[NUM_NODES];

        [SerializeField] private Graph _graph;

        public bool InTunnel { get { return _inTunnel; } }

        public float GetSize(int index)
        {
            return _sizeNodes[index];
        }

        public NodeGraph GetNode(int index)
        {
            return GetNode((Direction)index);
        }

        public NodeGraph GetNode(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return _upNode;
                case Direction.Down: return _downNode;
                case Direction.Left: return _leftNode;
                case Direction.Right: return _rightNode;
            }

            Debug.LogError("Incorrect direction");

            return null;
        }

        public void SetNode(NodeGraph node, int index)
        {
            SetNode(node, (Direction)index);
        }
        public void SetNode(NodeGraph node, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: _upNode = node; break;
                case Direction.Down: _downNode = node; break;
                case Direction.Left: _leftNode = node; break;
                case Direction.Right: _rightNode = node; break;
            }
        }

        public Direction GetDirNode(NodeGraph node)
        {
            if (node == _upNode)
            {
                return Direction.Up;
            }
            else if (node == _downNode)
            {
                return Direction.Down;
            }
            else if (node == _leftNode)
            {
                return Direction.Left;
            }
            else if (node == _rightNode)
            {
                return Direction.Right;
            }

            if (node == _graph.GhostSpawnNode)
            {
                return Direction.Up;
            }
            else if (node == _graph.GhostHomeNode)
            {
                return Direction.Down;
            }

            Debug.LogError("Node is not a neighbour");

            return Direction.Up;
        }

        public List<NodeGraph> GetNeighborNodes(Direction dir)
        {
            var neighborNode = new List<NodeGraph>();

            var neighborDir = DirectionUtility.GetNeighborDirections(dir);

            for (int i = 0; i < neighborDir.Count; ++i)
            {
                var nodeAux = GetNode(neighborDir[i]);

                if (nodeAux != null)
                {
                    neighborNode.Add(nodeAux);
                }
            }

            if (neighborNode.Count == 0)
            {
                Debug.LogError("Node " + name + " with direction " + dir.ToString() + " don't have a neighbor node.");
            }

            return neighborNode;
        }

        private void Awake()
        {
            if (_toGraph)
            {
                Graph.Nodes.Add(this);
            }

            for (int i = 0; i < _sizeNodes.Length; ++i)
            {
                _sizeNodes[i] = GetSizeNode(GetNode(i));
            }
        }

        public float GetSizeNode(NodeGraph node)
        {
            if (node != null)
            {
                if (node.InTunnel)
                {
                    return 100f;
                }
                else
                {
                    return Vector2.Distance(transform.position, node.transform.position);
                }
            }

            return -1;
        }
    }
}