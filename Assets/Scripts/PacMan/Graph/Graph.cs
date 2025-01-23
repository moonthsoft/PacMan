using System.Collections.Generic;
using UnityEngine;
using static Moonthsoft.PacMan.Ghost;

namespace Moonthsoft.PacMan
{
    public class Graph : MonoBehaviour
    {
        [SerializeField] private NodeGraph _playerInitialNode;

        [SerializeField] private NodeGraph _blinkyInitialNode;
        [SerializeField] private NodeGraph _pinkyInitialNode;
        [SerializeField] private NodeGraph _inkyInitialNode;
        [SerializeField] private NodeGraph _clydeInitialNode;

        [SerializeField] private NodeGraph _blinkyScatterNode;
        [SerializeField] private NodeGraph _pinkyScatterNode;
        [SerializeField] private NodeGraph _inkyScatterNode;
        [SerializeField] private NodeGraph _clydeScatterNode;

        [SerializeField] private NodeGraph _nodeTunnelA;
        [SerializeField] private NodeGraph _nodeTunnelB;

        public static bool ShowGizmos { get; set; }

        public List<NodeGraph> Nodes { get; } = new();

        public NodeGraph PlayerInitialNode { get { return _playerInitialNode; } }

        public NodeGraph GhostSpawnNode { get { return GetGhostInitialNode(GhostType.Blinky); } }
        public NodeGraph GhostHomeNode { get { return GetGhostInitialNode(GhostType.Pinky); } }


        public NodeGraph GetGhostInitialNode(GhostType ghostType)
        {
            switch (ghostType)
            {
                case GhostType.Blinky: return _blinkyInitialNode;
                case GhostType.Pinky: return _pinkyInitialNode;
                case GhostType.Inky: return _inkyInitialNode;
                case GhostType.Clyde: return _clydeInitialNode;
            }

            Debug.LogError("The Ghost Type is not configured.");

            return null;
        }
        
        public NodeGraph GetGhostScatterNode(GhostType ghostType)
        {
            switch (ghostType)
            {
                case GhostType.Blinky: return _blinkyScatterNode;
                case GhostType.Pinky: return _pinkyScatterNode;
                case GhostType.Inky: return _inkyScatterNode;
                case GhostType.Clyde: return _clydeScatterNode;
            }

            Debug.LogError("The Ghost Type is not configured.");

            return null;
        }

        public NodeGraph GetNearestNode(Vector2 pos)
        {
            NodeGraph nodeAux = null;
            float minSize = float.MaxValue;

            for (int i = 0; i < Nodes.Count; ++i)
            {
                float dist = Vector2.Distance(pos, Nodes[i].transform.position);

                if (dist < minSize)
                {
                    minSize = dist;

                    nodeAux = Nodes[i];
                }
            }

            return nodeAux;
        }

        public bool IsInTunnel(NodeGraph nodeTarget, NodeGraph nodeComesFrom)
        {
            if (nodeTarget == _nodeTunnelA || nodeTarget == _nodeTunnelB || nodeComesFrom == _nodeTunnelA || nodeComesFrom == _nodeTunnelB)
            {
                return true;
            }

            return false;
        }
    }
}